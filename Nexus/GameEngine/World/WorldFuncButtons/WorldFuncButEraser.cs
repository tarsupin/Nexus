
using static Nexus.GameEngine.WorldFuncTool;

namespace Nexus.GameEngine {

	public class WorldFuncButEraser : WorldFuncBut {

		public WorldFuncButEraser() : base() {
			this.keyChar = "x";
			this.spriteName = "Icons/Eraser";
			this.title = "Eraser";
			this.description = "Erases tiles.";
		}

		public override void ActivateWorldFuncButton() {
			WorldEditorTools.SetWorldFuncTool(WorldFuncTool.WorldFuncToolMap[(byte)WorldFuncToolEnum.Eraser]);
		}
	}
}
