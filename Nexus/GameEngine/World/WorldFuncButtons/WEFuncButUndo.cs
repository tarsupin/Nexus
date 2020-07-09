
namespace Nexus.GameEngine {

	public class WEFuncButUndo : WEFuncBut {

		public WEFuncButUndo() : base() {
			this.keyChar = "";
			this.spriteName = "Small/Undo";
			this.title = "Undo";
			this.description = "No behavior at this time.";
		}

		public override void ActivateWorldFuncButton() {
			System.Console.WriteLine("Activated Function Button: Undo");
		}
	}
}
