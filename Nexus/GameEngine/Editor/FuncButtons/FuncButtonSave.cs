using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncButtonSave : FuncButton {

		public FuncButtonSave() : base() {
			this.keyChar = "";
			this.spriteName = "Save";
			this.title = "Save";
			this.description = "Saves the level.";
		}

		public override void ActivateFuncButton() {
			Systems.handler.levelContent.SaveLevel();
			UIHandler.AddNotification(UIAlertType.Success, "Level Saved", "Level has been saved.", 240);
			GameValues.LastAction = "EditorSaveButton";
		}
	}
}
