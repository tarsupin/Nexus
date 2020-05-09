
namespace Nexus.GameEngine {

	public class FuncButtonSwapRight : FuncButton {

		public FuncButtonSwapRight() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/MoveRight";
			this.title = "Swap Room Position";
			this.description = "Swaps the current room with the room to the right (+1 ID higher).";
		}
	}
}
