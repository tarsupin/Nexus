
using static Nexus.GameEngine.WEFuncTool;

namespace Nexus.GameEngine {

	public class WEFuncButEraser : WEFuncBut {

		public WEFuncButEraser() : base() {
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
