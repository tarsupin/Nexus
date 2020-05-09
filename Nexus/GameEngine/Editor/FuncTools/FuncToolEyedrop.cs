using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncToolEyedrop : FuncTool {

		public FuncToolEyedrop() : base() {

		}

		public override void RunTick(EditorScene scene) {

			// Left or Right Mouse Click
			if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked || Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {

				// TODO:
				//this.scene.CloneTile();
			}
		}
	}
}
