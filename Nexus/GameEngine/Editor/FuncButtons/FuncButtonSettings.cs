
namespace Nexus.GameEngine {

	public class FuncButtonSettings : FuncButton {

		public FuncButtonSettings() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Settings";
			this.title = "Settings";
			this.description = "No behavior at this time.";
		}

		public override void ActivateFuncButton() {
			System.Console.WriteLine("Activated Function Button: Settings");
		}
	}
}
