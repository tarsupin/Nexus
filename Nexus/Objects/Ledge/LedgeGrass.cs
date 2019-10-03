using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class LedgeGrass : Ledge {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.LedgeGrass)) {
				new LedgeGrass(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.LedgeGrass, subTypeId, true, false, false);
		}

		public LedgeGrass(LevelScene scene) : base(scene, ClassGameObjectId.LedgeGrass) {
			this.BuildLedgeTextures("GrassLedge/");
		}
	}
}
