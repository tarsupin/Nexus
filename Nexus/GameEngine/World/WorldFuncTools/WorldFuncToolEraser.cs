using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WorldFuncToolEraser : WorldFuncTool {

		public WorldFuncToolEraser() : base() {
			this.spriteName = "Icons/Small/Eraser";
			this.title = "Eraser";
			this.description = "Erases tiles.";
		}

		public override void RunTick(WorldEditorScene scene) {
			if(UIComponent.ComponentWithFocus != null) { return; }

			// Left Mouse Button Down (Delete Current Tile)
			if(Cursor.mouseState.LeftButton == ButtonState.Pressed) {
				scene.worldContent.DeleteTile(scene.currentZone, (byte) Cursor.MiniGridX, (byte) Cursor.MiniGridY);
			}

			// Right Mouse Button Clicked (Clone Current Tile)
			else if(Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {
				scene.CloneTile((byte) Cursor.MiniGridX, (byte) Cursor.MiniGridY);
			}
		}
	}
}
