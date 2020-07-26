
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WEFuncButSettings : WEFuncBut {

		public WEFuncButSettings() : base() {
			this.keyChar = "";
			this.spriteName = "Small/Settings";
			this.title = "Settings";
			this.description = "Opens console to allow editing World Functions.";
		}

		public override void ActivateWorldFuncButton() {
			UIHandler.worldEditConsole.Open();
		}
	}
}
