using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Elemental : EnemyFlight {

		public Elemental(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {

			// Physics, Collisions, etc.
			this.physics = new Physics(this);

			// Assign Flight Behavior
			this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);
		}

		// TODO: Collide With Character (see Elemental)
		// TODO: Collide With Character (see Elemental)
		
		//update( time: Timer ): void {
		//	if(this.physics.update) { this.physics.update( time ); }
		//	if(this.att) { this.attack( time ); } // Run Attack, if applicable
		//}
		
		//attack( time: Timer ): boolean {
		//	const lastFire = Math.floor((time.elapsed + this.att.offset) / this.att.cycle);
		
		//	if(this.trackProjectile >= lastFire) { return false; }
		
		//	// Prevent repeat firing.
		//	if(this.trackProjectile < lastFire - 1) { this.trackProjectile = lastFire; return false; }
		
		//	this.trackProjectile = lastFire;

		//	return true;
		//};

		//collideWithCharacter( character: Character, dir: DirCardinal ): void {
		//	character.wound();
		//}
	}
}
