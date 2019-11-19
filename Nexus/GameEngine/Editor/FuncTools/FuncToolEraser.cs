using Microsoft.Xna.Framework.Input;

namespace Nexus.GameEngine {

	public class FuncToolEraser : FuncTool {

		public FuncToolEraser( EditorScene scene ) : base(scene) {

		}

		public override void RunTick() {

			// Left Mouse Button (Delete Current Tile)
			if(this.scene.mouseState.LeftButton == ButtonState.Pressed) {

				// TODO:
				//this.scene.DeleteTile();
			}

			// Right Mouse Button (Clone Current Tile)
			else if(this.scene.mouseState.RightButton == ButtonState.Pressed) {

				// TODO:
				//this.scene.CloneTile();
			}
		}
	}
}
