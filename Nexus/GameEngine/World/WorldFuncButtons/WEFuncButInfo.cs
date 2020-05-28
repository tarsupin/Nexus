﻿
namespace Nexus.GameEngine {

	public class WEFuncButInfo : WEFuncBut {

		public WEFuncButInfo() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Small/Info";
			this.title = "Editor Help";
			this.description = "Provides help for using the editor.";
		}

		public override void ActivateWorldFuncButton() {
			System.Console.WriteLine("Activated Function Button: Help");
		}
	}
}
