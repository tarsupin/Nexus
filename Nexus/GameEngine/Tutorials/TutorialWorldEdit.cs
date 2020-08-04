using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class TutorialWorldEdit : Tutorial {

		public WEScene scene;

		public Dictionary<short, Action> tutorialMethods;

		public const byte finalStep = 19;

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
			this.tutorialMethods.Add(14, this.PlaceLevelNode);
			this.tutorialMethods.Add(15, this.WandTool);
			this.tutorialMethods.Add(16, this.ChangeLevelID);
			this.tutorialMethods.Add(17, this.PlaceCharacter);
			this.tutorialMethods.Add(18, this.SaveWorld);
			this.tutorialMethods.Add(19, this.PlayWorld);
		}

		public override void IncrementTutorialStep() {
			base.IncrementTutorialStep();
			Systems.settings.tutorial.UpdateWorldEditorStep(this.tutorialStep);
		}

		public void RunTick() {

			// End the Tutorial if all steps have been reached.
			if(this.tutorialStep > TutorialWorldEdit.finalStep && (this.notify is UINotification == false || this.notify.alpha == 0)) { return; }

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

			this.SetTutorialNote(50, (short)(Systems.screen.viewHeight - 220), "Selecting Tiles", "Change your active tile by selecting a different tile from the utility bar. You can click on it, or press the shortcut key (e.g. 1, 2, etc).", DirRotate.Down);
		}

		private void PlacingTiles() {
			if(WETools.WETileTool is WETileTool && UIHandler.uiState == UIState.Playing && Cursor.MouseY < Systems.screen.viewHeight - 100 && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
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

			this.SetTutorialNote((short)(Systems.screen.viewWidth - 480), 100, "Scroller Bar", "Use your mouse scroll to change the tile variant.", DirRotate.Right);
		}

		private void TabMenu() {
			if(Systems.input.LocalKeyPressed(Keys.Tab)) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote((short)(Systems.screen.viewHalfWidth - 200), (short)(Systems.screen.viewHeight - 220), "Tab Menu", "Press and hold the `tab` key to access the Tab Menu.", DirRotate.Up);
		}

		private void TabMenuUse() {
			if(Systems.input.LocalKeyDown(Keys.Tab) && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(90, (short)(Systems.screen.viewHalfHeight - 70), "Using the Tab Menu", "While in the tab menu, click on a tileset archetype to work with those types of tiles.", DirRotate.Right);
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
				UIHandler.AddNotification(UIAlertType.Warning, "About Erasing", "The eraser will NOT change terrain into ocean. If you want to place ocean, select (or clone) the ocean tile.", 300);
				return;
			}

			this.SetTutorialNote(500, 250, "Quick-Erase Tiles", "To quickly erase tiles, hold down the X button while clicking and dragging across the tiles you want to erase.", DirRotate.Down);
		}

		private void OpenConsole() {
			if(UIHandler.uiState == UIState.Menu && UIHandler.menu is Console) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(450, (short)(Systems.screen.viewHeight - 250), "Editing Console", "Open the Editor Console for additional options by pressing the tilde key `~`, or clicking the settings option.", DirRotate.Down);
		}

		private void ResizeLevel() {
			if(UIHandler.uiState == UIState.Menu && UIHandler.menu is Console && ConsoleTrack.instructionText.IndexOf("resize") == 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(80, (short)(Systems.screen.viewHeight - 250), "Level Resizing", "Try resizing the level width by typing `resize width 100`", DirRotate.Down);
		}

		private void SetWorldName() {
			if(UIHandler.uiState == UIState.Menu && UIHandler.menu is Console && ConsoleTrack.instructionText.IndexOf("title") == 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(80, (short)(Systems.screen.viewHeight - 250), "Rename World", "Try renaming the world by typing `title My Test World`", DirRotate.Down);
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

			this.SetTutorialNote(610, (short)(Systems.screen.viewHeight - 220), "Change Zones", "Click the `Next Zone` button to switch to the next zone ID.", DirRotate.Down);
		}

		private void HomeZone() {
			if(this.scene.campaign.zoneId == 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(580, (short)(Systems.screen.viewHeight - 220), "Return to Home", "Click the `Home Zone` to return to the first zone.", DirRotate.Down);
		}

		private void PlaceLevelNode() {
			if(WETools.WETileTool is WETileToolNodes && WETools.WETileTool.index == 0 && UIHandler.uiState == UIState.Playing && Cursor.MouseY < Systems.screen.viewHeight - 100 && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				this.IncrementTutorialStep();
				UIHandler.AddNotification(UIAlertType.Normal, "Using Travel Nodes", "Place Nodes near each other to create pathways to travel across.", 300);
				return;
			}

			this.SetTutorialNote((short)(Systems.screen.viewHalfWidth - 200), (short)(Systems.screen.viewHeight - 220), "Place a Level Node", "Select \"Nodes\" from the tab menu and place a level node.", DirRotate.Up);
		}

		private void WandTool() {
			if(GameValues.LastAction == "WEWandTool" || WETools.WETempTool is WEFuncToolWand || WETools.WEFuncTool is WEFuncToolWand) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(420, (short)(Systems.screen.viewHeight - 250), "Wand Tool", "Click the wand button (or hold down 'e') to use the wand tool. The wand is used to assign level IDs to Nodes.", DirRotate.Down);
		}

		private void ChangeLevelID() {
			if(UIHandler.uiState == UIState.Menu && UIHandler.menu is Console && ConsoleTrack.instructionText.IndexOf("setLevel", StringComparison.OrdinalIgnoreCase) == 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(90, (short)(Systems.screen.viewHalfHeight - 70), "Using the Wand", "With the wand tool selected, click on a node to edit the level ID assigned. A console will appear, with the option to assign a desired level ID.", DirRotate.Right);
		}
		
		private void PlaceCharacter() {
			if(WETools.WETileTool is WETileToolNodes && WETools.WETileTool.index == 3 && UIHandler.uiState == UIState.Playing && Cursor.MouseY < Systems.screen.viewHeight - 100 && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				this.IncrementTutorialStep();
				UIHandler.AddNotification(UIAlertType.Normal, "Character Position", "Characters must be placed on a level node to work correctly.", 300);
				return;
			}

			this.SetTutorialNote(90, (short)(Systems.screen.viewHalfHeight - 70), "Place a Character", "Select a character from the tab menu (the \"Nodes\" category). Place it on a level node to assign the starting position.", DirRotate.Right);
		}

		private void SaveWorld() {
			if(GameValues.LastAction == "WESaveButton") {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(720, (short)(Systems.screen.viewHeight - 220), "Save World", "Press the `Save` button on the utility bar to save your world.", DirRotate.Down);
		}
		
		private void PlayWorld() {
			if(GameValues.LastAction == "WEPlayButton") {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(750, (short)(Systems.screen.viewHeight - 220), "Play Your World", "Press the `Play` button when you're ready to playtest the world.", DirRotate.Down);
		}
		
		// Placing Objects - they go above other things.
	}
}
