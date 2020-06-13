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
			this.SetActivity(Activity.NoTileCollide);

			// Assign Flight Behavior
			this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);
		}

		public override bool RunProjectileImpact(Projectile projectile) {
			return true;
		}

		public override bool RunCharacterImpact(Character character) {
			character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
			return true;
		}
	}
}
