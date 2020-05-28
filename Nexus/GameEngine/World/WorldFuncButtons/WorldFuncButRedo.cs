
namespace Nexus.GameEngine {

	public class WorldFuncButRedo : WEFuncBut {

		public WorldFuncButRedo() : base() {
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
