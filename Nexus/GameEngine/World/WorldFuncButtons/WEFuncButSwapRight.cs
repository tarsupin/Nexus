using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WEFuncButSwapRight : WEFuncBut {

		public WEFuncButSwapRight() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Small/MoveRight";
			this.title = "Swap Room Position";
			this.description = "Swaps the current room with the room to the right (+1 ID higher).";
		}

		public override void ActivateWorldFuncButton() {
			WEScene scene = (WEScene)Systems.scene;
			if(scene.campaign.zoneId < 8) {
				scene.SwapZoneOrder();
			}
		}
	}
}
