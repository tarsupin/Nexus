
using static Nexus.GameEngine.WorldFuncTool;

namespace Nexus.GameEngine {

	public class WorldFuncButWand : WorldFuncBut {

		public WorldFuncButWand() : base() {
			this.keyChar = "e";
			this.spriteName = "Icons/Wand";
			this.title = "Wand";
			this.description = "An advanced tool that allows you to modify properties on game objects.";
		}

		public override void ActivateWorldFuncButton() {
			WorldEditorTools.SetWorldFuncTool(WorldFuncTool.WorldFuncToolMap[(byte)WorldFuncToolEnum.Wand]);
		}
	}
}
