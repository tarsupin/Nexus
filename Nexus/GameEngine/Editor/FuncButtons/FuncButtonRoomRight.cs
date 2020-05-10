
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncButtonRoomRight : FuncButton {

		public FuncButtonRoomRight() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Right";
			this.title = "Switch Room Right";
			this.description = "Switches the active editing room to one room ID higher.";
		}

		public override void ActivateFuncButton() {
			EditorScene scene = (EditorScene)Systems.scene;
			if(scene.roomNum < 9) {
				scene.SwitchRoom((byte)(scene.roomNum + 1));
			}
		}
	}
}
