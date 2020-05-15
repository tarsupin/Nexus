﻿using Nexus.Engine;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class EnemyFlight : Enemy {

		public EnemyFlight(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {

		}
	}
}
