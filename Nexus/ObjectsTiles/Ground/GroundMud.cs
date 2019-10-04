using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundMud : Ground {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.GroundMud)) {
				new GroundMud(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.GroundMud, subTypeId, true, true, false);
		}

		public GroundMud(LevelScene scene) : base(scene, ClassGameObjectId.GroundMud) {
			this.BuildTextures("Mud/");
		}
	}
}
