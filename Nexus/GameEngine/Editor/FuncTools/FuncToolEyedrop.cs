﻿using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncToolEyedrop : FuncTool {

		public FuncToolEyedrop() : base() {
			this.spriteName = "Eyedrop";
			this.title = "Eyedrop";
			this.description = "Clones a tile - same behavior as right clicking a tile.";
		}

		public override void RunTick(EditorRoomScene scene) {
			if(UIComponent.ComponentWithFocus != null) { return; }

			// Left or Right Mouse Click
			if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked || Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {
				scene.CloneTile(Cursor.TileGridX, Cursor.TileGridY);
			}
		}
	}
}
