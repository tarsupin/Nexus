
namespace Nexus.GameEngine {

	public class WEFuncButRedo : WEFuncBut {

		public WEFuncButRedo() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Small/Redo";
			this.title = "Redo";
			this.description = "No behavior at this time.";
		}

		public override void ActivateWorldFuncButton() {
			System.Console.WriteLine("Activated Function Button: Redo");
		}
	}
}
