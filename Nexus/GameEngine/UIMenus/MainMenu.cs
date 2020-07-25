using Microsoft.Xna.Framework;
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

		private const string menuText = "Main Menu";
		private readonly short menuTextPos = 0;

		// Center Menu
		//private readonly UICreoTextIcon goBack;
		private readonly UICreoTextIcon log;
		private readonly UICreoTextIcon controls;
		private readonly UICreoTextIcon worlds;
		private readonly UICreoTextIcon myLevels;
		private readonly UICreoTextIcon myWorld;

		public MainMenu() {

			this.textBox = new TextBox(null, (short)(Systems.screen.windowHalfWidth - 150 - 16), (short)(Systems.screen.windowHalfHeight - 150 - 16), 316, 378);

			short centerX = (short)(Systems.screen.windowHalfWidth - 28);
			short centerY = (short)(Systems.screen.windowHalfHeight - 28 + 40);

			this.menuTextPos = (short)(Systems.screen.windowHalfWidth - Systems.fonts.baseText.font.MeasureString(MainMenu.menuText).X * 0.5f);

			//this.goBack = new UICreoTextIcon(null, "Back", "Return", centerX, centerY, delegate () { UIHandler.SetMenu(null, false); } );

			this.log = new UICreoTextIcon(null, "Login", "Login", (short)(centerX - 66 - 50), centerY, delegate () {
				UIHandler.SetMenu(UIHandler.loginMenu, true);
				UIHandler.loginMenu.ShowMenu();
			});

			this.controls = new UICreoTextIcon(null, "Gamepad", "Controls", (short)(centerX - 66 - 50), (short)(centerY - 66 - 50), delegate () {
				UIHandler.SetMenu(UIHandler.controlMenu, true);
			});

			this.worlds = new UICreoTextIcon(null, "MyWorld", "Worlds", centerX, (short)(centerY - 66 - 50), delegate () { SceneTransition.ToPlanetSelection(); } );
			this.myLevels = new UICreoTextIcon(null, "MyLevels", "My Levels", (short)(centerX + 66 + 50), centerY, delegate () { SceneTransition.ToMyLevels(); } );
			
			//this.TOP_RIGHT = new UICreoTextIcon(null, "MyWorld", "My World", (short)(centerX + 66 + 50), (short)(centerY - 66 - 50), delegate () { SceneTransition.ToWorldEditor("__World"); } );

			this.myWorld = new UICreoTextIcon(null, "MyWorld", "My World", centerX, (short)(centerY + 66 + 50), delegate () { SceneTransition.ToWorldEditor("__World"); } );
		}

		public void RunTick() {

			// Get User Input
			PlayerInput input = Systems.localServer.MyPlayer.input;

			// Determine which option is selected:
			if(input.isDown(IKey.Right)) {
				this.opt = MenuOptionActive.MyLevels;
			}

			else if(input.isDown(IKey.Left)) {
				if(input.isDown(IKey.Up)) { this.opt = MenuOptionActive.Controls; }
				else { this.opt = MenuOptionActive.Log; }
			}

			else if(input.isDown(IKey.Down)) { this.opt = MenuOptionActive.MyWorld; }
			else if(input.isDown(IKey.Up)) { this.opt = MenuOptionActive.Worlds; }

			// Check if the start button was pressed.
			if(input.isPressed(IKey.AButton)) {

				// Close the Menu
				UIHandler.SetMenu(null, false);

				//if (this.opt == MenuOptionActive.Return) { return; }
				if(this.opt == MenuOptionActive.Log) { this.log.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.Controls) { this.controls.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.Worlds) { this.worlds.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.MyLevels) { this.myLevels.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.MyWorld) { this.myWorld.ActivateIcon(); return; }
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
			//this.goBack.RunTick();
			this.log.RunTick();
			this.controls.RunTick();
			this.worlds.RunTick();
			this.myLevels.RunTick();
			this.myWorld.RunTick();
		}

		public void Draw() {
			this.textBox.Draw();

			Systems.fonts.baseText.Draw(MainMenu.menuText, this.menuTextPos, this.textBox.trueY + 22, Color.White);

			//this.goBack.Draw(this.opt == MenuOptionActive.Return);
			this.log.Draw(this.opt == MenuOptionActive.Log);
			this.controls.Draw(this.opt == MenuOptionActive.Controls);
			this.worlds.Draw(this.opt == MenuOptionActive.Worlds);
			this.myLevels.Draw(this.opt == MenuOptionActive.MyLevels);
			this.myWorld.Draw(this.opt == MenuOptionActive.MyWorld);
		}
	}
}
