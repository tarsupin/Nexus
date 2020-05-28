
using static Nexus.GameEngine.WEFuncTool;

namespace Nexus.GameEngine {

	public class WorldFuncButEraser : WEFuncBut {

		public WorldFuncButEraser() : base() {
			this.keyChar = "x";
			this.spriteName = "Icons/Small/Eraser";
			this.title = "Eraser";
			this.description = "Erases tiles.";
		}

		public override void ActivateWorldFuncButton() {
			WETools.SetWorldFuncTool(WEFuncTool.WEFuncToolMap[(byte)WEFuncToolEnum.Eraser]);
		}
	}
}
