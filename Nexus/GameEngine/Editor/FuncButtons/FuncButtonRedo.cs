
namespace Nexus.GameEngine {

	public class FuncButtonRedo : FuncButton {

		public FuncButtonRedo() : base() {
			this.keyChar = "";
			this.spriteName = "Redo";
			this.title = "Redo";
			this.description = "No behavior at this time.";
		}

		public override void ActivateFuncButton() {
			System.Console.WriteLine("Activated Function Button: Redo");
		}
	}
}
