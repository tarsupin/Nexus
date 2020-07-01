using Nexus.Engine;

namespace Nexus.GameEngine {

	public class MainMenuUI : ICenterMenu {

		private readonly Scene scene;

		private enum MenuOptionActive : byte {
			Return,		// Center
			Log,		// Left
			Worlds,		// Up
			Community,	// Down
			MyLevels,	// Right
			MyWorld,	// Up-Right
			Credits,	// Down-Left
		}

		private MenuOptionActive opt = MenuOptionActive.Return;

		// Center Menu
		private readonly UIIconWithText ret;
		private readonly UIIconWithText log;
		private readonly UIIconWithText worlds;
		private readonly UIIconWithText community;
		private readonly UIIconWithText myLevels;
		private readonly UIIconWithText myWorld;
		private readonly UIIconWithText credits;

		private readonly LoginBox loginBox;

		public MainMenuUI(Scene scene) {
			this.scene = scene;

			short centerX = (short)(Systems.screen.windowHalfWidth - 28);
			short centerY = (short)(Systems.screen.windowHalfHeight - 28);

			this.ret = new UIIconWithText(null, "UI/Back", "Return", centerX, centerY, delegate () { this.scene.SetUIState(Scene.UIState.Playing); } );
			this.log = new UIIconWithText(null, "UI/Login", "Login", (short)(centerX - 66 - 50), centerY, delegate () { } );
			this.worlds = new UIIconWithText(null, "UI/MyWorld", "Worlds", centerX, (short)(centerY - 66 - 50), delegate () { SceneTransition.ToPlanetSelection(); } );
			this.community = new UIIconWithText(null, "UI/Community", "Community", centerX, (short)(centerY + 66 + 50), delegate () { WebHandler.LaunchURL("https://nexus.games"); } );
			this.myLevels = new UIIconWithText(null, "UI/MyLevels", "My Levels", (short)(centerX + 66 + 50), centerY, delegate () { SceneTransition.ToMyLevels(); } );
			this.myWorld = new UIIconWithText(null, "UI/MyWorld", "My World", (short)(centerX + 66 + 50), (short)(centerY - 66 - 50), delegate () { SceneTransition.ToWorldEditor("__World"); } );
			this.credits = new UIIconWithText(null, "UI/About", "Credits", (short)(centerX - 66 - 50), (short)(centerY + 66 + 50), delegate () { WebHandler.LaunchURL("https://nexus.games/credits"); } );

			this.loginBox = new LoginBox(null, 200, 200, 280, 300);
		}

		public void RunTick() {

			// Get User Input
			PlayerInput input = Systems.localServer.MyPlayer.input;

			// Determine which option is selected:
			if(input.isDown(IKey.Right)) {
				if(input.isDown(IKey.Up)) { this.opt = MenuOptionActive.MyWorld; }
				else { this.opt = MenuOptionActive.MyLevels; }
			}

			else if(input.isDown(IKey.Left)) {
				if(input.isDown(IKey.Down)) { this.opt = MenuOptionActive.Credits; }
				else { this.opt = MenuOptionActive.Log; }
			}

			else if(input.isDown(IKey.Down)) { this.opt = MenuOptionActive.Community; }
			else if(input.isDown(IKey.Up)) { this.opt = MenuOptionActive.Worlds; }
			else { this.opt = MenuOptionActive.Return; }

			// Check if the start button was pressed.
			if(input.isPressed(IKey.AButton)) {

				// Close the Menu
				this.scene.SetUIState(Scene.UIState.Playing);

				if (this.opt == MenuOptionActive.Return) { return; }
				else if(this.opt == MenuOptionActive.Log) { this.log.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.Worlds) { this.worlds.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.Community) { this.community.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.MyLevels) { this.myLevels.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.MyWorld) { this.myWorld.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.Credits) { this.credits.ActivateIcon(); return; }
			}

			else if(input.isPressed(IKey.Start)) {
				this.scene.SetUIState(Scene.UIState.Playing);
				return;
			}

			// Center Menu
			this.ret.RunTick();
			this.log.RunTick();
			this.worlds.RunTick();
			this.community.RunTick();
			this.myLevels.RunTick();
			this.myWorld.RunTick();
			this.credits.RunTick();

			this.loginBox.RunTick();
		}

		public void Draw() {
			this.ret.Draw(this.opt == MenuOptionActive.Return);
			this.log.Draw(this.opt == MenuOptionActive.Log);
			this.worlds.Draw(this.opt == MenuOptionActive.Worlds);
			this.community.Draw(this.opt == MenuOptionActive.Community);
			this.myLevels.Draw(this.opt == MenuOptionActive.MyLevels);
			this.myWorld.Draw(this.opt == MenuOptionActive.MyWorld);
			this.credits.Draw(this.opt == MenuOptionActive.Credits);

			this.loginBox.Draw();
		}
	}
}
