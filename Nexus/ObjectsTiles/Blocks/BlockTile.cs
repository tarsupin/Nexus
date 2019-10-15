using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class BlockTile : TileGameObject {

		public BlockTile(LevelScene scene, TileGameObjectId classId) : base(scene, classId, AtlasGroup.Tiles) {
			this.collides = true;
		}
	}
}
