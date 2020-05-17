﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum DireSubType : byte {
		Dire
	}

	public class Dire : EnemyFlight {

		public Dire(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyFly];

			// Physics, Collisions, etc.
			this.physics = new Physics(this);

			// Assign Flight Behavior
			this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(6, 28, -28, -6);
		}

		private void AssignSubType( byte subType ) {
			this.SpriteName = "Dire/Left2";
		}
	}
}
