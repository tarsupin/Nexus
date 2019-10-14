using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundSnow : Ground {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.GroundSnow)) {
				new GroundSnow(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.GroundSnow, subTypeId);
		}

		public GroundSnow(LevelScene scene) : base(scene, TileGameObjectId.GroundSnow) {
			this.BuildTextures("Snow/");
		}
	}
}
