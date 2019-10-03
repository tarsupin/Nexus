using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundSnow : Ground {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.GroundSnow)) {
				new GroundSnow(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.GroundSnow, subTypeId, true, true, false);
		}

		public GroundSnow(LevelScene scene) : base(scene, ClassGameObjectId.GroundSnow) {
			this.BuildGroundTextures("Snow/");
		}
	}
}
