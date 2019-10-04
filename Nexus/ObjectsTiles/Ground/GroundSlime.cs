using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundSlime : Ground {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.GroundSlime)) {
				new GroundSlime(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.GroundSlime, subTypeId, true, true, false);
		}

		public GroundSlime(LevelScene scene) : base(scene, ClassGameObjectId.GroundSlime) {
			this.BuildTextures("Slime/");
		}
	}
}
