
using static Nexus.GameEngine.WEFuncTool;

namespace Nexus.GameEngine {

	public class WEFuncButMove : WEFuncBut {

		public WEFuncButMove() : base() {
			this.keyChar = "v";
			this.spriteName = "Icons/Small/Move";
			this.title = "Selection Tool";
			this.description = "Drag and move selections. Ctrl+C will copy, Ctrl+X will cut, Delete will end.";
		}

		public override void ActivateWorldFuncButton() {
			WETools.SetWorldFuncTool(WEFuncTool.WEFuncToolMap[(byte) WEFuncToolEnum.Move]);
		}
	}
}
