using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundMud : Ground {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.GroundMud)) {
				new GroundMud(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.GroundMud, subTypeId);
		}

		public GroundMud(LevelScene scene) : base(scene, TileGameObjectId.GroundMud) {
			this.BuildTextures("Mud/");
		}
	}
}
