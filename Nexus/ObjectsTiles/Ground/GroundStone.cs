using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundStone : Ground {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.GroundStone)) {
				new GroundStone(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.GroundStone, subTypeId);
		}

		public GroundStone(LevelScene scene) : base(scene, TileGameObjectId.GroundStone) {
			this.BuildTextures("Stone/");
		}
	}
}
