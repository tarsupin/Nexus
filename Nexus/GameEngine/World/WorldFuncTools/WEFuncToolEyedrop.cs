using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WEFuncToolEyedrop : WEFuncTool {

		public WEFuncToolEyedrop() : base() {
			this.spriteName = "Icons/Small/Eyedrop";
			this.title = "Eyedrop";
			this.description = "Clones a tile - same behavior as right clicking a tile.";
		}

		public override void RunTick(WEScene scene) {
			if(UIComponent.ComponentWithFocus != null) { return; }

			// Left or Right Mouse Click
			if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked || Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {
				scene.CloneTile((byte) Cursor.MiniGridX, (byte) Cursor.MiniGridY);
			}
		}
	}
}
