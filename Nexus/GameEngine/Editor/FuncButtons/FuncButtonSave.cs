
namespace Nexus.GameEngine {

	public class FuncButtonSave : FuncButton {

		public FuncButtonSave() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Save";
			this.title = "Save";
			this.description = "Saves the level.";
		}

		public override void ActivateFuncButton() {
			System.Console.WriteLine("Activated Function Button: Save");
		}
	}
}
