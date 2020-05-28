﻿
namespace Nexus.GameEngine {

	public class WEFuncButSettings : WEFuncBut {

		public WEFuncButSettings() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Small/Settings";
			this.title = "Settings";
			this.description = "No behavior at this time.";
		}

		public override void ActivateWorldFuncButton() {
			System.Console.WriteLine("Activated Function Button: Settings");
		}
	}
}
