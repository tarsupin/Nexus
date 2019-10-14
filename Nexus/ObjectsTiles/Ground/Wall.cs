using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Wall : Ground {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte)TileGameObjectId.Wall)) {
				new Wall(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte)TileGameObjectId.Wall, subTypeId);
		}

		public Wall(LevelScene scene) : base(scene, TileGameObjectId.Wall) {
			this.BuildTextures("Slab/Gray/");	// TODO: Change to Wall
		}
	}
}
