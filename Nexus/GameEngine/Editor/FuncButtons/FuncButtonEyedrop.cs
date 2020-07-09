
using static Nexus.GameEngine.FuncTool;

namespace Nexus.GameEngine {

	public class FuncButtonEyedrop : FuncButton {

		public FuncButtonEyedrop() : base() {
			this.keyChar = "c";
			this.spriteName = "Eyedrop";
			this.title = "Eyedrop";
			this.description = "Clones a tile - same behavior as right clicking a tile.";
		}

		public override void ActivateFuncButton() {
			EditorTools.SetFuncTool(FuncTool.funcToolMap[(byte) FuncToolEnum.Eyedrop]);
		}
	}
}
