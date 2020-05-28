
namespace Nexus.GameEngine {

	public class WorldFuncButUndo : WEFuncBut {

		public WorldFuncButUndo() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Small/Undo";
			this.title = "Undo";
			this.description = "No behavior at this time.";
		}

		public override void ActivateWorldFuncButton() {
			System.Console.WriteLine("Activated Function Button: Undo");
		}
	}
}
