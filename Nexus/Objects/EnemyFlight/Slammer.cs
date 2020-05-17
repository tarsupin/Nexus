using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum SlammerSubType : byte {
		Slammer
	}

	public class Slammer : EnemyFlight {

		public Slammer(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Slammer].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);

			// TODO: Slammer Behavior
			// Assign Flight Behavior
			//this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(6, 28, -28, -6);
		}

		private void AssignSubType( byte subType ) {
			this.SpriteName = "Slammer/Standard";
		}
	}
}
