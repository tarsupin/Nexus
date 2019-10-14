using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBoxBR : ToggleBlock {

		protected new bool Toggled {
			get { return this.scene.flags.toggleBR; }
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.ToggleBoxBR)) {
				new ToggleBoxBR(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.ToggleBoxBR, subTypeId);
		}

		public ToggleBoxBR(LevelScene scene) : base(scene, TileGameObjectId.ToggleBoxBR) {
			this.Texture = "/BoxBR";
		}
	}
}
