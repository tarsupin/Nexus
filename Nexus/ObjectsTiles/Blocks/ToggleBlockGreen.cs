using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockGreen : ToggleBlock {

		protected new bool Toggled {
			get { return this.scene.flags.toggleGY; }
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.ToggleBlockGreen)) {
				new ToggleBlockGreen(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.ToggleBlockGreen, subTypeId);
		}

		public ToggleBlockGreen(LevelScene scene) : base(scene, TileGameObjectId.ToggleBlockGreen) {
			this.Texture = "/Green/Block";

		}
	}
}
