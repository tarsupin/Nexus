
namespace Nexus.GameEngine {

	public class FuncButtonEraser : FuncButton {

		public FuncButtonEraser() : base() {
			this.keyChar = "x";
			this.spriteName = "Icons/Eraser";
			this.title = "Eraser";
			this.description = "Erases tiles.";
		}

		public override void ActivateFuncButton() {
			System.Console.WriteLine("Activated Function Button: Eraser");
		}
	}
}
