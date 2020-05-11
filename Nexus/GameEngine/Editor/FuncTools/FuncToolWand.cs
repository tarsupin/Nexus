using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncToolWand : FuncTool {

		public FuncToolWand() : base() {
			this.spriteName = "Icons/Wand";
			this.title = "Wand";
			this.description = "An advanced tool that allows you to modify properties on game objects.";
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
