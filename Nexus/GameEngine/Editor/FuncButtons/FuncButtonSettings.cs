using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncButtonSettings : FuncButton {

		public FuncButtonSettings() : base() {
			this.keyChar = "";
			this.spriteName = "Settings";
			this.title = "Settings";
			this.description = "Opens the level console to edit important settings.";
		}

		public override void ActivateFuncButton() {
			UIHandler.editorConsole.Open();
			GameValues.LastAction = "EditorConsoleButton";
		}
	}
}
