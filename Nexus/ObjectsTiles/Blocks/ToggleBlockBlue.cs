using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockBlue : ToggleBlock {

		protected new bool Toggled {
			get { return this.scene.flags.toggleBR; }
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.ToggleBlockBlue)) {
				new ToggleBlockBlue(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.ToggleBlockBlue, subTypeId, true, true, false, true, false);
		}

		public ToggleBlockBlue(LevelScene scene) : base(scene, ClassGameObjectId.ToggleBlockBlue) {
			this.Texture = "/Blue/Block";
		}
	}
}
