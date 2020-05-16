using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class BGTile : TileGameObject {

		public BGTile() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.BGTile];
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			return false;
		}
	}
}
