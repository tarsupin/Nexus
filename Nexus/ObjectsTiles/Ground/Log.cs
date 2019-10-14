using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Log : Ground {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.Log)) {
				new Log(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.Log, subTypeId);
		}

		public Log(LevelScene scene) : base(scene, TileGameObjectId.Log) {
			this.BuildTextures("Log/");
		}
	}
}
