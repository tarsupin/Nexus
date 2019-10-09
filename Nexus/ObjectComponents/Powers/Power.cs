﻿using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Power {

		// References
		protected readonly Character character;

		public readonly string pool;						// The pool that the projectile will get sent to for reuse.
		public string IconTexture { get; protected set; }	// The texture path for the Power Icon (e.g. "Power/" + this.pool)

		protected byte cooldown;
		protected byte numberOfUses;
		protected byte delayBetweenUses;
		protected uint lastActivation;
		protected uint[] lastUseTracker;

		public Power( Character character, string pool ) {
			this.character = character;
			this.pool = pool;
		}

		public virtual void Activate() {}
		public virtual void UpdateAbilities() {}

		public void SetActivationSettings( byte cooldown, byte numberOfUses = 2, byte delayBetweenUses = 15 ) {
			this.cooldown = cooldown;
			this.numberOfUses = numberOfUses;
			this.delayBetweenUses = delayBetweenUses;
			this.lastActivation = 0;
			this.lastUseTracker = new uint[this.numberOfUses];
		}

		public bool CanActivate() {

			// Cannot activate this power while holding an item:
			// TODO HIGH PRIORITY: When we have items, uncomment:
			//if(this.character.item != null) { return false; }

			TimerGlobal timer = this.character.scene.timer;

			// Delay if last activation was too recent.
			if(timer.frame < this.lastActivation) { return false; }

			// If this character is a fast caster, run special activation behaviors:
			byte fastCastMult = this.character.stats.CanFastCast ? (byte) 2 : (byte) 1;

			// Loop through available power uses:
			foreach( uint i in this.lastUseTracker ) {

				// If we found an available activation slot:
				if(timer.frame > this.lastUseTracker[i]) {

					// Consume this activation for now:
					this.lastUseTracker[i] = (uint)(timer.frame + this.cooldown / fastCastMult);

					// Set most recent activation:
					this.lastActivation = (uint)(timer.frame + this.delayBetweenUses / fastCastMult);

					return true;
				}
			}

			return false;
		}
	}
}
