using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class BlockTile : TileGameObject {

		public BlockTile(RoomScene room, TileGameObjectId classId) : base(room, classId, AtlasGroup.Tiles) {
			this.collides = true;
		}
	}
}
