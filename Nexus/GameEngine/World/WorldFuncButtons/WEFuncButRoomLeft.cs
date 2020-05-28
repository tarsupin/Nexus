using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WEFuncButRoomLeft : WEFuncBut {

		public WEFuncButRoomLeft() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Small/Left";
			this.title = "Switch Room Left";
			this.description = "Switches the active editing room to one room ID lower.";
		}

		public override void ActivateWorldFuncButton() {
			WEScene scene = (WEScene) Systems.scene;
			if(scene.campaign.zoneId > 0) {
				scene.SwitchZone((byte)(scene.campaign.zoneId - 1));
			}
		}
	}
}
