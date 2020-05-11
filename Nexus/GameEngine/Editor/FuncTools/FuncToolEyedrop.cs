using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncToolEyedrop : FuncTool {

		public FuncToolEyedrop() : base() {
			this.spriteName = "Icons/Eyedrop";
			this.title = "Eyedrop";
			this.description = "Clones a tile - same behavior as right clicking a tile.";
		}

		public override void RunTick(EditorRoomScene scene) {

			// Left or Right Mouse Click
			if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked || Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {
				scene.CloneTile(Cursor.MouseGridX, Cursor.MouseGridY);
			}
		}
	}
}
