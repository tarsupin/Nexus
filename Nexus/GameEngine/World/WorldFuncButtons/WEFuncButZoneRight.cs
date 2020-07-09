
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WEFuncButZoneRight : WEFuncBut {

		public WEFuncButZoneRight() : base() {
			this.keyChar = "";
			this.spriteName = "Small/Right";
			this.title = "Switch Zone Right";
			this.description = "Switches the active editing zone to one zone ID higher.";
		}

		public override void ActivateWorldFuncButton() {
			WEScene scene = (WEScene)Systems.scene;
			if(scene.campaign.zoneId < 9) {
				scene.SwitchZone((byte)(scene.campaign.zoneId + 1));
			}
		}
	}
}
