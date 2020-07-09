
using static Nexus.GameEngine.FuncTool;

namespace Nexus.GameEngine {

	public class FuncButtonSelect : FuncButton {

		public FuncButtonSelect() : base() {
			this.keyChar = "v";
			this.spriteName = "Move";
			this.title = "Selection Tool";
			this.description = "Drag and move selections. Ctrl+C will copy, Ctrl+X will cut, Delete will end.";
		}

		public override void ActivateFuncButton() {
			EditorTools.SetFuncTool(FuncTool.funcToolMap[(byte) FuncToolEnum.Select]);
		}
	}
}
