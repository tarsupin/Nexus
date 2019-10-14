using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockYellow : ToggleBlock {

		protected new bool Toggled {
			get { return this.scene.flags.toggleGY; }
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.ToggleBlockYellow)) {
				new ToggleBlockYellow(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.ToggleBlockYellow, subTypeId);
		}

		public ToggleBlockYellow(LevelScene scene) : base(scene, TileGameObjectId.ToggleBlockYellow) {
			this.Texture = "/Yellow/Block";
		}
	}
}
