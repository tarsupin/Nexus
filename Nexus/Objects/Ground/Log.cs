using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Log : Ground {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.Log)) {
				new Log(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.Log, subTypeId, true, true, false);
		}

		public Log(LevelScene scene) : base(scene, ClassGameObjectId.Log) {
			this.BuildGroundTextures("Log/");
		}
	}
}
