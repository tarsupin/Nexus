
using static Nexus.GameEngine.WEFuncTool;

namespace Nexus.GameEngine {

	public class WorldFuncButWand : WEFuncBut {

		public WorldFuncButWand() : base() {
			this.keyChar = "e";
			this.spriteName = "Icons/Small/Wand";
			this.title = "Wand";
			this.description = "An advanced tool that allows you to modify properties on game objects.";
		}

		public override void ActivateWorldFuncButton() {
			WETools.SetWorldFuncTool(WEFuncTool.WEFuncToolMap[(byte)WEFuncToolEnum.Wand]);
		}
	}
}
