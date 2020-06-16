﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class BGTile : TileObject {

		public BGTile() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.BGTile];
		}

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {
			return false;
		}
	}
}
