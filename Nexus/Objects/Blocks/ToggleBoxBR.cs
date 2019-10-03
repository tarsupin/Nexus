using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBoxBR : ToggleBlock {
		 
		// TODO HIGH PRIORITY: Point this to Global Scene Toggles.
		protected new bool Toggled {
			get {
				return true;
			}
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.ToggleBoxBR)) {
				new ToggleBoxBR(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.ToggleBoxBR, subTypeId, true, false, false);
		}

		public ToggleBoxBR(LevelScene scene) : base(scene, ClassGameObjectId.ToggleBoxBR) {
			this.Texture = "/BoxBR";
		}
	}
}
