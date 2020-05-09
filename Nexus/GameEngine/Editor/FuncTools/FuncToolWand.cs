using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncToolWand : FuncTool {

		public FuncToolWand() : base() {

		}

		public override void RunTick(EditorScene scene) {

			// Left Mouse Button (Delete Current Tile)
			if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {

				// TODO:
				//this.CreateWandObject();
			}

			// Right Mouse Button (Clone Current Tile)
			else if(Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {

				// TODO:
				//this.scene.CloneTile();
			}
		}
	}
}
