
namespace Nexus.GameEngine {

	public class FuncButtonUndo : FuncButton {

		public FuncButtonUndo() : base() {
			this.keyChar = "";
			this.spriteName = "Undo";
			this.title = "Undo";
			this.description = "No behavior at this time.";
		}

		public override void ActivateFuncButton() {
			System.Console.WriteLine("Activated Function Button: Undo");
		}
	}
}
