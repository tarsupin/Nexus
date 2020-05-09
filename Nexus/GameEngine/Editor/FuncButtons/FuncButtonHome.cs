
namespace Nexus.GameEngine {

	public class FuncButtonHome : FuncButton {

		public FuncButtonHome() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Home";
			this.title = "Home";
			this.description = "Switches to the 'home' room, where the level begins.";
		}

		public override void ActivateFuncButton() {
			System.Console.WriteLine("Activated Function Button: Home");
		}
	}
}
