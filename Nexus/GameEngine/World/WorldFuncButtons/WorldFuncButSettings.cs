
namespace Nexus.GameEngine {

	public class WorldFuncButSettings : WorldFuncBut {

		public WorldFuncButSettings() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Settings";
			this.title = "Settings";
			this.description = "No behavior at this time.";
		}

		public override void ActivateWorldFuncButton() {
			System.Console.WriteLine("Activated Function Button: Settings");
		}
	}
}
