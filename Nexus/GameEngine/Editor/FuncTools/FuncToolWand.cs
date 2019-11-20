using Microsoft.Xna.Framework.Input;

namespace Nexus.GameEngine {

	public class FuncToolWand : FuncTool {

		public FuncToolWand( EditorScene scene ) : base(scene) {

		}

		public override void RunTick() {

			// Left Mouse Button (Delete Current Tile)
			if(this.scene.mouseState.LeftButton == ButtonState.Pressed) {

				// TODO:
				//this.CreateWandObject();
			}

			// Right Mouse Button (Clone Current Tile)
			else if(this.scene.mouseState.RightButton == ButtonState.Pressed) {

				// TODO:
				//this.scene.CloneTile();
			}
		}
	}
}
