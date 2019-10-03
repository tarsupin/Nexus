using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Wall : Ground {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte)ClassGameObjectId.Wall)) {
				new Wall(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte)ClassGameObjectId.Wall, subTypeId, true, true, false);
		}

		public Wall(LevelScene scene) : base(scene, ClassGameObjectId.Wall) {
			this.BuildGroundTextures("Slab/Gray/");	// TODO: Change to Wall
		}
	}
}
