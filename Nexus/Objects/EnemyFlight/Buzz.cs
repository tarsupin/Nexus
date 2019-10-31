using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public enum BuzzSubType : byte {
		Buzz
	}

	public class Buzz : EnemyFlight {

		public Buzz(RoomScene room, byte subType, FVector pos, JObject paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyFly];

			// Physics, Collisions, etc.
			this.physics = new Physics(this);

			// Assign Flight Behavior
			this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 2, -2, -2);
		}

		private void AssignSubType( byte subType ) {
			this.SpriteName = "Buzz/Left2";
		}

		public override bool DamageByTNT() { return false; }
	}
}
