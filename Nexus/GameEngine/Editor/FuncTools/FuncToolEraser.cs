using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncToolEraser : FuncTool {

		public FuncToolEraser() : base() {

		}

		public override void RunTick(EditorScene scene) {

			// Left Mouse Button Down (Delete Current Tile)
			if(Cursor.mouseState.LeftButton == ButtonState.Pressed) {

				// TODO:
				//this.scene.DeleteTile();
			}

			// Right Mouse Button Clicked (Clone Current Tile)
			else if(Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {

				// TODO:
				//this.scene.CloneTile();
			}
		}
	}
}
