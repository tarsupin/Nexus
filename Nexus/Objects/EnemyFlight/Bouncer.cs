using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum BouncerSubType : byte { Normal }

	public class Bouncer : EnemyFlight {

		public Bouncer(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Bouncer].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);

			// TODO: Need Wall Bounce Behavior
			// Assign Flight Behavior
			//this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(6, 6, -6, -6);
		}

		private void AssignSubType( byte subType ) {
			this.SpriteName = "Elemental/MiniAir";
		}
	}
}
