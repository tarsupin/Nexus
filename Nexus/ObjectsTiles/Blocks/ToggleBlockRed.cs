using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockRed : ToggleBlock {

		protected new bool Toggled {
			get { return this.scene.flags.toggleBR; }
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.ToggleBlockRed)) {
				new ToggleBlockRed(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.ToggleBlockRed, subTypeId, true, true, false, true, false);
		}

		public ToggleBlockRed(LevelScene scene) : base(scene, ClassGameObjectId.ToggleBlockRed) {
			this.Texture = "/Red/Block";
		}
	}
}
