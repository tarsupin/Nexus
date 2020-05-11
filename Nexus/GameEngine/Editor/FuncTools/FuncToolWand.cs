using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncToolWand : FuncTool {

		public FuncToolWand() : base() {
			this.spriteName = "Icons/Wand";
		}

		public override void RunTick(EditorRoomScene scene) {

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
