using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatYellow : TogglePlat {

		protected new bool Toggled {
			get { return this.scene.flags.toggleGY; }
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.TogglePlatYellow)) {
				new TogglePlatYellow(scene, subTypeId);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.TogglePlatYellow, subTypeId, true, false, false, true, false);
		}

		public TogglePlatYellow(LevelScene scene, byte subTypeId) : base(scene, subTypeId, ClassGameObjectId.TogglePlatYellow) {
			this.Texture = "/Yellow/Plat";
		}
	}
}
