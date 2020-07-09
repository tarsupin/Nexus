
using static Nexus.GameEngine.FuncTool;

namespace Nexus.GameEngine {

	public class FuncButtonEraser : FuncButton {

		public FuncButtonEraser() : base() {
			this.keyChar = "x";
			this.spriteName = "Eraser";
			this.title = "Eraser";
			this.description = "Erases tiles.";
		}

		public override void ActivateFuncButton() {
			EditorTools.SetFuncTool(FuncTool.funcToolMap[(byte)FuncToolEnum.Eraser]);
		}
	}
}
