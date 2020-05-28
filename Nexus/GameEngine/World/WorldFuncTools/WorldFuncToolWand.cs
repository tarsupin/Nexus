using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WorldFuncToolWand : WorldFuncTool {

		public WorldFuncToolWand() : base() {
			this.spriteName = "Icons/Small/Wand";
			this.title = "Wand";
			this.description = "An advanced tool that allows you to modify properties on game objects.";
		}

		public override void RunTick(WorldEditorScene scene) {
			if(UIComponent.ComponentWithFocus != null) { return; }

			// Left Mouse Button
			if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				
			}

			// Right Mouse Button (Clone Current Tile)
			else if(Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {
				scene.CloneTile((byte) Cursor.MiniGridX, (byte) Cursor.MiniGridY);
			}
		}
	}

	// Stores data related to a selected object and the wand param menu currently open.
	public static class WorldWandData {

	}
}
