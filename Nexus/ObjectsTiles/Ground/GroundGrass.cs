using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundGrass : Ground {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.GroundGrass)) {
				new GroundGrass(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.GroundGrass, subTypeId);
		}

		public GroundGrass(LevelScene scene) : base(scene, TileGameObjectId.GroundGrass) {
			this.BuildTextures("Grass/");
		}
	}
}
