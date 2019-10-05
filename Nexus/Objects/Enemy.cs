using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Enemy : DynamicGameObject {

		public DamageEnum ProjectileResist { get; protected set; }

		public Enemy(LevelScene scene, byte subType, FVector pos, object[] paramList) : base(scene, subType, pos, paramList) {

		}

		public void RunTick() {
			base.RunTick();
			// if this.animation, then this.animation.runTick();
		}

		public bool CanResistDamage( DamageEnum damage ) {
			return this.ProjectileResist >= damage;
		}

		// TODO: Set to "Character" class, not DynamicGameObject
		public void WoundCharacter( DynamicGameObject character ) {
			// if action is Deathaction, return
			// character.ReceiveWound();
		}

		// TODO: GetJumpedOn( Character char, byte bounceStrength )
		public bool GetJumpedOn( byte character, byte bounceStrength = 0 ) {
			// TODO: if action is DeathAction, return
			// TODO: character.bounceUp( this, bounceStrength );
			return this.ReceiveWound();
		}

		public bool ReceiveWound() {
			// TODO: Play Sound (splat1)
			return this.Die(DeathResult.Knockout);
		}

		public bool Die( DeathResult deathType ) {
			// TODO: this.action = new DeathAction( this, DeathActionType.Standard );
				// TODO inside of DeathAction: if(this.animation) { this.animation.null; }
				// TODO inside: this.physics.setGravity(0.5) if it's a Knockout
			return true;
		}

		// Land Enemies typically die to TNT
		public bool DamageByTNT() {
			return this.Die(DeathResult.Knockout);
		}

	}
}
