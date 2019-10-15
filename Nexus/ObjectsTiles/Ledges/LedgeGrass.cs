using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class LedgeGrass : Ledge {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.LedgeGrass)) {
				new LedgeGrass(scene);
			}

			// Add to Tilemap
			// TODO HIGH PRIORITY: Ledge won't actually face up in every case. Must assign based on subtype.
			// TODO HIGH PRIORITY: NOTE: Some of these will just be DECOR tiles.
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.LedgeGrass, subTypeId);
		}

		public LedgeGrass(LevelScene scene) : base(scene, TileGameObjectId.LedgeGrass) {
			this.BuildTextures("GrassLedge/");
		}
	}
}
