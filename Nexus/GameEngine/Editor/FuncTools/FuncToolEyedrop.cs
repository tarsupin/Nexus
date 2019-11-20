
using Microsoft.Xna.Framework.Input;

namespace Nexus.GameEngine {

	public class FuncToolEyedrop : FuncTool {

		public FuncToolEyedrop( EditorScene scene ) : base(scene) {

		}

		public override void RunTick() {

			// Left or Right Mouse Click
			if(EditorCursor.mouseState.LeftButton == ButtonState.Pressed || EditorCursor.mouseState.RightButton == ButtonState.Pressed) {

				// TODO:
				//this.scene.CloneTile();
			}
		}
	}
}
