using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundGrass : Ground {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.GroundGrass)) {
				new GroundGrass(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.GroundGrass, subTypeId, true, true, false);
		}

		public GroundGrass(LevelScene scene) : base(scene, ClassGameObjectId.GroundGrass) {
			this.BuildTextures("Grass/");
		}
	}
}
