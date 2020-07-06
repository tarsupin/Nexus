using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WEFuncButZoneLeft : WEFuncBut {

		public WEFuncButZoneLeft() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Small/Left";
			this.title = "Switch Zone Left";
			this.description = "Switches the active editing zone to one zone ID lower.";
		}

		public override void ActivateWorldFuncButton() {
			WEScene scene = (WEScene) Systems.scene;
			if(scene.campaign.zoneId > 0) {
				scene.SwitchZone((byte)(scene.campaign.zoneId - 1));
			}
		}
	}
}
