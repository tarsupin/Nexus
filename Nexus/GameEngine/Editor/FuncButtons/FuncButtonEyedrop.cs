
namespace Nexus.GameEngine {

	public class FuncButtonEyedrop : FuncButton {

		public FuncButtonEyedrop() : base() {
			this.keyChar = "c";
			this.spriteName = "Icons/Eyedrop";
			this.title = "Eyedrop";
			this.description = "Clones a tile - same behavior as right clicking a tile.";
		}

		public override void ActivateFuncButton() {
			System.Console.WriteLine("Activated Function Button: Eyedrop");
		}
	}
}
