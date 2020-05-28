using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WorldFuncToolEyedrop : WorldFuncTool {

		public WorldFuncToolEyedrop() : base() {
			this.spriteName = "Icons/Eyedrop";
			this.title = "Eyedrop";
			this.description = "Clones a tile - same behavior as right clicking a tile.";
		}

		public override void RunTick(WorldEditorScene scene) {
			if(UIComponent.ComponentWithFocus != null) { return; }

			// Left or Right Mouse Click
			if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked || Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {
				scene.CloneTile((byte) Cursor.MouseGridX, (byte) Cursor.MouseGridY);
			}
		}
	}
}
