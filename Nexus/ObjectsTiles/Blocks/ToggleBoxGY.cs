using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBoxGY : ToggleBlock {
		 
		protected new bool Toggled {
			get { return this.scene.flags.toggleGY; }
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.ToggleBoxGY)) {
				new ToggleBoxGY(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.ToggleBoxGY, subTypeId);
		}

		public ToggleBoxGY(LevelScene scene) : base(scene, TileGameObjectId.ToggleBoxGY) {
			this.Texture = "/BoxGY";
		}
	}
}
