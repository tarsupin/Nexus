using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class TutorialWorldEdit : Tutorial {

		public WEScene scene;

		public Dictionary<short, Action> tutorialMethods;

		public TutorialWorldEdit(WEScene scene) : base() {
			this.scene = scene;
			this.tutorialStep = (short)(Systems.settings.tutorial.WorldEditor);

			this.tutorialMethods = new Dictionary<short, Action>();
			this.tutorialMethods.Add(0, this.SelectingTiles);
			this.tutorialMethods.Add(1, this.PlacingTiles);
			this.tutorialMethods.Add(2, this.PlacingTerrainNotes);
			this.tutorialMethods.Add(3, this.ScrollerBar);
			this.tutorialMethods.Add(4, this.TabMenu);
			this.tutorialMethods.Add(5, this.TabMenuUse);
			this.tutorialMethods.Add(6, this.CloningTiles);
			this.tutorialMethods.Add(7, this.EraseTiles);
			this.tutorialMethods.Add(8, this.OpenConsole);
			this.tutorialMethods.Add(9, this.ResizeLevel);
			this.tutorialMethods.Add(10, this.SetWorldName);
			this.tutorialMethods.Add(11, this.FasterMove);
			this.tutorialMethods.Add(12, this.ChangeZone);
			this.tutorialMethods.Add(13, this.HomeZone);
		}

		public override void IncrementTutorialStep() {
			base.IncrementTutorialStep();
			Systems.settings.tutorial.UpdateWorldEditorStep(this.tutorialStep);
		}

		public void RunTick() {

			// End the Tutorial if all steps have been reached.
			if(this.tutorialStep > 13 && (this.notify is UINotification == false || this.notify.alpha == 0)) { return; }

			// Update Notification Fading
			if(this.notify is UINotification) {
				this.notify.RunTick();
			}

			// Invoke any tutorial method that should be running.
			if(this.tutorialMethods.ContainsKey(this.tutorialStep) && (this.notify is UINotification == false || this.notify.exitFrame < Systems.timer.UniFrame)) {
				this.tutorialMethods[this.tutorialStep].Invoke();
			}
		}

		private void SelectingTiles() {

			if(WETools.WETileTool is WETileTool && WETools.WETileTool.index > 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(50, (short)(Systems.screen.windowHeight - 220), "Selecting Tiles", "Change your active tile by selecting a different tile from the utility bar. You can click on it, or press the shortcut key (e.g. 1, 2, etc).", DirRotate.Down);
		}

		private void PlacingTiles() {
			if(WETools.WETileTool is WETileTool && UIHandler.uiState == UIState.Playing && Cursor.MouseY < Systems.screen.windowHeight - 100 && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(500, 300, "Placing Tiles", "Click and/or drag on the world grid to place a tile that you have actively selected.", DirRotate.Down);
		}
		
		private void PlacingTerrainNotes() {
			if(WETools.WETileTool is WETileTool && UIHandler.uiState == UIState.Playing && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(300, 300, "Drawing Coastlines", "Some tiles create coastlines in the ocean, but not every tile combination exists. Fill in your coastlines completely. (Click to continue)", DirRotate.Center);
		}

		private void ScrollerBar() {
			if(WETools.WETileTool is WETileTool && UIHandler.uiState == UIState.Playing && Cursor.MouseScrollDelta != 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote((short)(Systems.screen.windowWidth - 480), 100, "Scroller Bar", "Use your mouse scroll to change the tile variant.", DirRotate.Right);
		}

		private void TabMenu() {
			if(Systems.input.LocalKeyPressed(Keys.Tab)) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote((short)(Systems.screen.windowHalfWidth - 200), (short)(Systems.screen.windowHeight - 220), "Tab Menu", "Press and hold the `tab` key to access the Tab Menu.", DirRotate.Up);
		}

		private void TabMenuUse() {
			if(Systems.input.LocalKeyDown(Keys.Tab) && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(90, (short)(Systems.screen.windowHalfHeight - 70), "Using the Tab Menu", "While in the tab menu, click on a tileset archetype to work with those types of tiles.", DirRotate.Right);
		}

		private void CloningTiles() {
			if(Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(500, 250, "Quick-Clone Tiles", "You can quick-clone tiles that are already on the level grid by right-clicking them. This will update your current tile set accordingly.", DirRotate.Down);
		}

		private void EraseTiles() {
			if(Systems.input.LocalKeyDown(Keys.X) && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(500, 250, "Quick-Erase Tiles", "To quickly erase tiles, hold down the X button while clicking and dragging across the tiles you want to erase.", DirRotate.Down);
		}

		private void OpenConsole() {
			if(UIHandler.uiState == UIState.Menu && UIHandler.menu is Console) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(450, (short)(Systems.screen.windowHeight - 250), "Editing Console", "Open the Editor Console for additional options by pressing the tilde key `~`, or clicking the settings option.", DirRotate.Down);
		}

		private void ResizeLevel() {
			if(UIHandler.uiState == UIState.Menu && UIHandler.menu is Console && ConsoleTrack.instructionText.IndexOf("resize") == 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(80, (short)(Systems.screen.windowHeight - 250), "Level Resizing", "Try resizing the level width by typing `resize width 100`", DirRotate.Down);
		}

		private void SetWorldName() {
			if(UIHandler.uiState == UIState.Menu && UIHandler.menu is Console && ConsoleTrack.instructionText.IndexOf("title") == 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(80, (short)(Systems.screen.windowHeight - 250), "Rename World", "Try renaming the world by typing `title My Test World`", DirRotate.Down);
		}

		private void FasterMove() {
			if(UIHandler.uiState == UIState.Playing && (Systems.input.LocalKeyDown(Keys.LeftShift) || Systems.input.LocalKeyDown(Keys.RightShift))) {
				var input = Systems.localServer.MyPlayer.input;
				if(input.isDown(IKey.Left) || input.isDown(IKey.Right) || input.isDown(IKey.Up) || input.isDown(IKey.Down)) {
					this.IncrementTutorialStep();
				}
				return;
			}

			this.SetTutorialNote(500, 250, "World Viewing", "Hold the `shift` key down while moving the camera (WASD keys) to increase the camera's movement speed.", DirRotate.Down);
		}

		private void ChangeZone() {
			if(this.scene.campaign.zoneId > 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(610, (short)(Systems.screen.windowHeight - 220), "Change Zones", "Click the `Next Zone` button to switch to the next zone ID.", DirRotate.Down);
		}

		private void HomeZone() {
			if(this.scene.campaign.zoneId == 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(580, (short)(Systems.screen.windowHeight - 220), "Return to Home", "Click the `Home Zone` to return to the first zone.", DirRotate.Down);
		}

		// Placing Nodes
		// Placing Objects - they go above other things.
	}
}
