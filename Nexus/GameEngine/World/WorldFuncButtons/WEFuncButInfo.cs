
using Microsoft.Xna.Framework;
using Nexus.Engine;
using static Nexus.GameEngine.Scene;

namespace Nexus.GameEngine {

	public class WEFuncButInfo : WEFuncBut {

		public WEFuncButInfo() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Small/Info";
			this.title = "Editor Help";
			this.description = "Provides help for using the editor.";
		}

		public override void ActivateWorldFuncButton() {
			Systems.worldEditConsole.Open();
			Systems.scene.uiState = UIState.Console;
			ChatConsole.SendMessage("--------------------", Color.White);
			ChatConsole.SendMessage("Log on to nexus.games for assistance on building worlds and levels. You'll need to register an account to share your work online.", Color.Green);
			ChatConsole.SendMessage("--------------------", Color.White);
		}
	}
}
