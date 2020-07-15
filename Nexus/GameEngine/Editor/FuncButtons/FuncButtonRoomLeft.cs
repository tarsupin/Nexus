using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncButtonRoomLeft : FuncButton {

		public FuncButtonRoomLeft() : base() {
			this.keyChar = "";
			this.spriteName = "Left";
			this.title = "Switch Room Left";
			this.description = "Switches the active editing room to one room ID lower.";
		}

		public override void ActivateFuncButton() {
			EditorScene scene = (EditorScene) Systems.scene;
			if(scene.curRoomID > 0) {
				scene.SwitchRoom((byte)(scene.curRoomID - 1));
				UIHandler.AddNotification(UIAlertType.Normal, "Switched Room", "Now Viewing Room #" + (scene.curRoomID + 1) + ".", 180);
			}
		}
	}
}
