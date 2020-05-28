﻿
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WEFuncButHome : WEFuncBut {

		public WEFuncButHome() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Small/Home";
			this.title = "Home";
			this.description = "Switches to the 'home' room, where the level begins.";
		}

		public override void ActivateWorldFuncButton() {
			WEScene scene = (WEScene)Systems.scene;
			scene.SwitchZone(0);
		}
	}
}
