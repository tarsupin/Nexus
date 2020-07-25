using Nexus.Engine;

namespace Nexus.GameEngine {

	public class MainMenu : IMenu {

		private enum MenuOptionActive : byte {
			Return,		// Center
			Log,		// Left
			Controls,	// Up-Left
			Worlds,		// Up
			Community,	// Down
			MyLevels,	// Right
			MyWorld,	// Up-Right
			Credits,	// Down-Left
		}

		private MenuOptionActive opt = MenuOptionActive.Return;

		TextBox textBox;

		// Center Menu
		private readonly UICreoTextIcon ret;
		private readonly UICreoTextIcon log;
		private readonly UICreoTextIcon controls;
		private readonly UICreoTextIcon worlds;
		private readonly UICreoTextIcon community;
		private readonly UICreoTextIcon myLevels;
		private readonly UICreoTextIcon myWorld;
		private readonly UICreoTextIcon credits;

		public MainMenu() {

			this.textBox = new TextBox(null, (short)(Systems.screen.windowHalfWidth - 150 - 16), (short)(Systems.screen.windowHalfHeight - 150 - 16), 316, 328);

			short centerX = (short)(Systems.screen.windowHalfWidth - 28);
			short centerY = (short)(Systems.screen.windowHalfHeight - 28);

			this.ret = new UICreoTextIcon(null, "Back", "Return", centerX, centerY, delegate () { UIHandler.SetMenu(null, false); } );

			this.log = new UICreoTextIcon(null, "Login", "Login", (short)(centerX - 66 - 50), centerY, delegate () {
				UIHandler.SetMenu(UIHandler.loginMenu, true);
				UIHandler.loginMenu.ShowMenu();
			});

			this.controls = new UICreoTextIcon(null, "Gamepad", "Controls", (short)(centerX - 66 - 50), (short)(centerY - 66 - 50), delegate () {
				UIHandler.SetMenu(UIHandler.controlMenu, true);
			});

			this.worlds = new UICreoTextIcon(null, "MyWorld", "Worlds", centerX, (short)(centerY - 66 - 50), delegate () { SceneTransition.ToPlanetSelection(); } );
			this.community = new UICreoTextIcon(null, "Community", "Community", centerX, (short)(centerY + 66 + 50), delegate () { WebHandler.LaunchURL("https://nexus.games"); } );
			this.myLevels = new UICreoTextIcon(null, "MyLevels", "My Levels", (short)(centerX + 66 + 50), centerY, delegate () { SceneTransition.ToMyLevels(); } );
			this.myWorld = new UICreoTextIcon(null, "MyWorld", "My World", (short)(centerX + 66 + 50), (short)(centerY - 66 - 50), delegate () { SceneTransition.ToWorldEditor("__World"); } );
			this.credits = new UICreoTextIcon(null, "About", "Credits", (short)(centerX - 66 - 50), (short)(centerY + 66 + 50), delegate () { WebHandler.LaunchURL("https://creo.nexus.games/about.html"); } );
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
				else if(input.isDown(IKey.Up)) { this.opt = MenuOptionActive.Controls; }
				else { this.opt = MenuOptionActive.Log; }
			}

			else if(input.isDown(IKey.Down)) { this.opt = MenuOptionActive.Community; }
			else if(input.isDown(IKey.Up)) { this.opt = MenuOptionActive.Worlds; }
			else { this.opt = MenuOptionActive.Return; }

			// Check if the start button was pressed.
			if(input.isPressed(IKey.AButton)) {

				// Close the Menu
				UIHandler.SetMenu(null, false);

				if (this.opt == MenuOptionActive.Return) { return; }
				else if(this.opt == MenuOptionActive.Log) { this.log.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.Controls) { this.controls.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.Worlds) { this.worlds.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.Community) { this.community.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.MyLevels) { this.myLevels.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.MyWorld) { this.myWorld.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.Credits) { this.credits.ActivateIcon(); return; }
			}

			else if(input.isPressed(IKey.Start)) {
				UIHandler.SetMenu(null, false);
				return;
			}

			// If the left mouse button was clicked, we leave the menu unless it was opening the menu.
			// It might click on an object below, but if not, we clicked off of the menu.
			else if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				if(UIComponent.ComponentWithFocus is UICreoIcon) { return; }
				UIHandler.SetMenu(null, false);
			}

			// Center Menu
			this.ret.RunTick();
			this.log.RunTick();
			this.controls.RunTick();
			this.worlds.RunTick();
			this.community.RunTick();
			this.myLevels.RunTick();
			this.myWorld.RunTick();
			this.credits.RunTick();
		}

		public void Draw() {
			this.textBox.Draw();
			this.ret.Draw(this.opt == MenuOptionActive.Return);
			this.log.Draw(this.opt == MenuOptionActive.Log);
			this.controls.Draw(this.opt == MenuOptionActive.Controls);
			this.worlds.Draw(this.opt == MenuOptionActive.Worlds);
			this.community.Draw(this.opt == MenuOptionActive.Community);
			this.myLevels.Draw(this.opt == MenuOptionActive.MyLevels);
			this.myWorld.Draw(this.opt == MenuOptionActive.MyWorld);
			this.credits.Draw(this.opt == MenuOptionActive.Credits);
		}
	}
}
