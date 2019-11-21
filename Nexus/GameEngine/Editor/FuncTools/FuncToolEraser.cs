using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncToolEraser : FuncTool {

		public FuncToolEraser() : base() {

		}

		public override void RunTick(EditorScene scene) {

			// Left Mouse Button (Delete Current Tile)
			if(Cursor.mouseState.LeftButton == ButtonState.Pressed) {

				// TODO:
				//this.scene.DeleteTile();
			}

			// Right Mouse Button (Clone Current Tile)
			else if(Cursor.mouseState.RightButton == ButtonState.Pressed) {

				// TODO:
				//this.scene.CloneTile();
			}
		}
	}
}
