using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WEFuncButSwapRight : WEFuncBut {

		public WEFuncButSwapRight() : base() {
			this.keyChar = "";
			this.spriteName = "Small/MoveRight";
			this.title = "Swap Zone Position";
			this.description = "Swaps the current zone with the zone to the right (+1 ID higher).";
		}

		public override void ActivateWorldFuncButton() {
			WEScene scene = (WEScene)Systems.scene;
			if(scene.campaign.zoneId < 8) {
				scene.SwapZoneOrder();
			}
		}
	}
}
