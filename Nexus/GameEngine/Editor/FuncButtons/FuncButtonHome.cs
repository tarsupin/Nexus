
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncButtonHome : FuncButton {

		public FuncButtonHome() : base() {
			this.keyChar = "";
			this.spriteName = "Home";
			this.title = "Home";
			this.description = "Switches to the 'home' room, where the level begins.";
		}

		public override void ActivateFuncButton() {
			EditorScene scene = (EditorScene)Systems.scene;
			scene.SwitchRoom(0);
			UIHandler.AddNotification(UIAlertType.Normal, "Switched Room", "Switched to Home Room.", 180);
		}
	}
}
