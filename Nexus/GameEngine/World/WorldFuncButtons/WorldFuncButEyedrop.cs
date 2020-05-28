
using static Nexus.GameEngine.WorldFuncTool;

namespace Nexus.GameEngine {

	public class WorldFuncButEyedrop : WorldFuncBut {

		public WorldFuncButEyedrop() : base() {
			this.keyChar = "c";
			this.spriteName = "Icons/Small/Eyedrop";
			this.title = "Eyedrop";
			this.description = "Clones a tile - same behavior as right clicking a tile.";
		}

		public override void ActivateWorldFuncButton() {
			WorldEditorTools.SetWorldFuncTool(WorldFuncTool.WorldFuncToolMap[(byte) WorldFuncToolEnum.Eyedrop]);
		}
	}
}
