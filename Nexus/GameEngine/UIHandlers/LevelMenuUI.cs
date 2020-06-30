using Nexus.Engine;

namespace Nexus.GameEngine {

	public class LevelMenuUI : ICenterMenu {

		private readonly LevelScene scene;

		private enum MenuOptionActive : byte {
			Continue,
			Retry,
			Restart,
			ToMainMenu,
			EndLevel
		}

		private MenuOptionActive opt = MenuOptionActive.Continue;

		// Center Menu
		private readonly UIIconWithText cont;
		private readonly UIIconWithText retry;
		private readonly UIIconWithText restart;
		private readonly UIIconWithText toMain;
		private readonly UIIconWithText endLevel;

		public LevelMenuUI(LevelScene scene) {
			this.scene = scene;

			short centerX = (short)(Systems.screen.windowHalfWidth - 28);
			short centerY = (short)(Systems.screen.windowHalfHeight - 28);

			this.cont = new UIIconWithText(null, "UI/Continue", "Continue", centerX, centerY, delegate () {} );
			this.retry = new UIIconWithText(null, "UI/Retry", "Retry", (short)(centerX + 66 + 50), centerY, delegate () { this.scene.RestartLevel(false); } );
			this.restart = new UIIconWithText(null, "UI/Restart", "Restart", (short)(centerX - 66 - 50), centerY, delegate () { this.scene.RestartLevel(true); } );
			this.toMain = new UIIconWithText(null, "UI/Menu", "Main Menu", centerX, (short)(centerY - 66 - 50), delegate () { this.scene.uiState = Scene.UIState.MainMenu; } );
			this.endLevel = new UIIconWithText(null, "UI/Exit", "End Level", centerX, (short)(centerY + 66 + 50), delegate () { this.scene.EndLevel(); } );
		}

		public void RunTick() {

			// Get User Input
			PlayerInput input = Systems.localServer.MyCharacter.input;

			// Determine which option is selected:
			if(input.isDown(IKey.Right)) { this.opt = MenuOptionActive.Retry; }
			else if(input.isDown(IKey.Left)) { this.opt = MenuOptionActive.Restart; }
			else if(input.isDown(IKey.Up)) { this.opt = MenuOptionActive.ToMainMenu; }
			else if(input.isDown(IKey.Down)) { this.opt = MenuOptionActive.EndLevel; }
			else { this.opt = MenuOptionActive.Continue; }

			// Check if the start button was released.
			if(input.isPressed(IKey.AButton)) {

				// Close the Menu
				this.scene.uiState = LevelScene.UIState.Playing;

				if (this.opt == MenuOptionActive.Continue) { return; }
				else if(this.opt == MenuOptionActive.Retry) { this.retry.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.Restart) { this.restart.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.ToMainMenu) { this.toMain.ActivateIcon(); return; }
				else if(this.opt == MenuOptionActive.EndLevel) { this.endLevel.ActivateIcon(); return; }
			}

			else if(input.isPressed(IKey.Start)) {
				this.scene.uiState = LevelScene.UIState.Playing;
				return;
			}

			// Center Menu
			this.cont.RunTick();
			this.retry.RunTick();
			this.restart.RunTick();
			this.toMain.RunTick();
			this.endLevel.RunTick();
		}

		public void Draw() {
			this.cont.Draw(this.opt == MenuOptionActive.Continue);
			this.retry.Draw(this.opt == MenuOptionActive.Retry);
			this.restart.Draw(this.opt == MenuOptionActive.Restart);
			this.toMain.Draw(this.opt == MenuOptionActive.ToMainMenu);
			this.endLevel.Draw(this.opt == MenuOptionActive.EndLevel);
		}
	}
}
