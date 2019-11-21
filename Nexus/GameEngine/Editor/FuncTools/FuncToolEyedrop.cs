using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncToolEyedrop : FuncTool {

		public FuncToolEyedrop() : base() {

		}

		public override void RunTick(EditorScene scene) {

			// Left or Right Mouse Click
			if(Cursor.mouseState.LeftButton == ButtonState.Pressed || Cursor.mouseState.RightButton == ButtonState.Pressed) {

				// TODO:
				//this.scene.CloneTile();
			}
		}
	}
}
