
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WorldFuncButRoomRight : WorldFuncBut {

		public WorldFuncButRoomRight() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Small/Right";
			this.title = "Switch Room Right";
			this.description = "Switches the active editing room to one room ID higher.";
		}

		public override void ActivateWorldFuncButton() {
			WorldEditorScene scene = (WorldEditorScene)Systems.scene;
			if(scene.campaign.zoneId < 9) {
				scene.SwitchZone((byte)(scene.campaign.zoneId + 1));
			}
		}
	}
}
