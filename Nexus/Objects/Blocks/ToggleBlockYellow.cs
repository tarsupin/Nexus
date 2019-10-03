using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockYellow : ToggleBlock {

		// TODO HIGH PRIORITY: Point this to Global Scene Toggles.
		protected new bool Toggled {
			get {
				return true;
			}
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.ToggleBlockYellow)) {
				new ToggleBlockYellow(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.ToggleBlockYellow, subTypeId, true, false, false, true);
		}

		public ToggleBlockYellow(LevelScene scene) : base(scene, ClassGameObjectId.ToggleBlockYellow) {
			this.Texture = "/Yellow/Block";
		}
	}
}
