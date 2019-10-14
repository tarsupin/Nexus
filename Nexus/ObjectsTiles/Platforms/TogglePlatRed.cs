using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatRed : TogglePlat {

		protected new bool Toggled {
			get { return this.scene.flags.toggleBR; }
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.TogglePlatRed)) {
				new TogglePlatRed(scene, subTypeId);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.TogglePlatRed, subTypeId);
		}

		public TogglePlatRed(LevelScene scene, byte subTypeId) : base(scene, subTypeId, TileGameObjectId.TogglePlatRed) {
			this.Texture = "/Red/Plat";
		}
	}
}
