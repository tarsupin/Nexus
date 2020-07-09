
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WEFuncButSave : WEFuncBut {

		public WEFuncButSave() : base() {
			this.keyChar = "";
			this.spriteName = "Small/Save";
			this.title = "Save";
			this.description = "Saves the world.";
		}

		public override void ActivateWorldFuncButton() {
			Systems.handler.worldContent.SaveWorld();
			((WEScene)Systems.scene).weUI.noticeText.SetNotice("World Saved", "World has been saved.");
		}
	}
}
