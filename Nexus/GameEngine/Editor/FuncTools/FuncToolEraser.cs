using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncToolEraser : FuncTool {

		public FuncToolEraser() : base() {
			this.spriteName = "Icons/Eraser";
			this.title = "Eraser";
			this.description = "Erases tiles.";
		}

		public override void RunTick(EditorRoomScene scene) {

			// Left Mouse Button Down (Delete Current Tile)
			if(Cursor.mouseState.LeftButton == ButtonState.Pressed) {
				scene.DeleteTile(Cursor.MouseGridX, Cursor.MouseGridY);
			}

			// Right Mouse Button Clicked (Clone Current Tile)
			else if(Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {
				scene.CloneTile(Cursor.MouseGridX, Cursor.MouseGridY);
			}
		}
	}
}
