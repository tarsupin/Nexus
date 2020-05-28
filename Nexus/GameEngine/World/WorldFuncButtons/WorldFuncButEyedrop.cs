﻿
using static Nexus.GameEngine.WEFuncTool;

namespace Nexus.GameEngine {

	public class WorldFuncButEyedrop : WEFuncBut {

		public WorldFuncButEyedrop() : base() {
			this.keyChar = "c";
			this.spriteName = "Icons/Small/Eyedrop";
			this.title = "Eyedrop";
			this.description = "Clones a tile - same behavior as right clicking a tile.";
		}

		public override void ActivateWorldFuncButton() {
			WETools.SetWorldFuncTool(WEFuncTool.WEFuncToolMap[(byte) WEFuncToolEnum.Eyedrop]);
		}
	}
}
