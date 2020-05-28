
using static Nexus.GameEngine.WorldFuncTool;

namespace Nexus.GameEngine {

	public class WorldFuncButSelect : WorldFuncBut {

		public WorldFuncButSelect() : base() {
			this.keyChar = "v";
			this.spriteName = "Icons/Move";
			this.title = "Selection Tool";
			this.description = "Drag and move selections. Ctrl+C will copy, Ctrl+X will cut, Delete will end.";
		}

		public override void ActivateWorldFuncButton() {
			WorldEditorTools.SetWorldFuncTool(WorldFuncTool.WorldFuncToolMap[(byte) WorldFuncToolEnum.Select]);
		}
	}
}
