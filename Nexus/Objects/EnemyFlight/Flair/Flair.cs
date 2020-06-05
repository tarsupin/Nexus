using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Flair : EnemyFlight {

		public Flair(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {

			// Physics, Collisions, etc.
			this.physics = new Physics(this);

			// Assign Flight Behavior
			this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);
		}

		//attack( time: Timer ): boolean {
		//	const lastFire = Math.floor((time.elapsed + this.att.offset) / this.att.cycle);

		//	if(this.trackProjectile >= lastFire) { return false; }

		//	// Prevent repeat firing.
		//	if(this.trackProjectile < lastFire - 1) { this.trackProjectile = lastFire; return false; }

		//	this.trackProjectile = lastFire;

		//	return true;
		//};

		public override bool RunCharacterImpact(Character character) {
			character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
			return true;
		}
	}
}
