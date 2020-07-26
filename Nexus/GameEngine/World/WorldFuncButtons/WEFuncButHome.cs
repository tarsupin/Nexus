
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WEFuncButHome : WEFuncBut {

		public WEFuncButHome() : base() {
			this.keyChar = "";
			this.spriteName = "Small/Home";
			this.title = "Home";
			this.description = "Switches to the 'home' zone, where the world begins.";
		}

		public override void ActivateWorldFuncButton() {
			WEScene scene = (WEScene)Systems.scene;
			scene.SwitchZone(0);
			GameValues.LastAction = "WEHomeButton";
		}
	}
}
