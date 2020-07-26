
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncButtonInfo : FuncButton {

		public FuncButtonInfo() : base() {
			this.keyChar = "";
			this.spriteName = "Info";
			this.title = "Editor Help";
			this.description = "Provides help for using the editor.";
		}

		public override void ActivateFuncButton() {
			UIHandler.SetMenu(UIHandler.guideMenu, false);
			GameValues.LastAction = "EditorInfoButton";
		}
	}
}
