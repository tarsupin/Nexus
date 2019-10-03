using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundStone : Ground {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.GroundStone)) {
				new GroundStone(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.GroundStone, subTypeId, true, true, false);
		}

		public GroundStone(LevelScene scene) : base(scene, ClassGameObjectId.GroundStone) {
			this.BuildTextures("Stone/");
		}
	}
}
