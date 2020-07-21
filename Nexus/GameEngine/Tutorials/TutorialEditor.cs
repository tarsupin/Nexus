using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class TutorialEditor : Tutorial {

		public EditorScene scene;

		public Dictionary<short, Action> tutorialMethods;

		public TutorialEditor(EditorScene scene) : base() {
			this.scene = scene;

			this.tutorialMethods = new Dictionary<short, Action>();
			this.tutorialMethods.Add(0, this.SelectingTiles);
			this.tutorialMethods.Add(1, this.PlacingTiles);
			this.tutorialMethods.Add(2, this.ScrollerBar);
			this.tutorialMethods.Add(3, this.AutoTiling);
			this.tutorialMethods.Add(4, this.TabMenu);
			this.tutorialMethods.Add(5, this.TabMenuUse);
			this.tutorialMethods.Add(6, this.CloningTiles);
			this.tutorialMethods.Add(7, this.EraseTiles);
			this.tutorialMethods.Add(8, this.OpenConsole);
			this.tutorialMethods.Add(9, this.ResizeLevel);
			this.tutorialMethods.Add(10, this.SetLevelName);
			this.tutorialMethods.Add(11, this.FasterMove);
			this.tutorialMethods.Add(12, this.ChangeRoom);
			this.tutorialMethods.Add(13, this.HomeRoom);
		}

		public void RunTick() {

			// End the Tutorial if all steps have been reached.
			if(this.tutorialStep > 13) { return; }

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

			if(EditorTools.tileTool is TileTool) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(50, (short)(Systems.screen.windowHeight - 220), "Selecting Tiles", "Select a tile from the utility bar by clicking on it, or by pressing the shortcut key (e.g. 1, 2, etc).", DirRotate.Down);
		}
		
		private void PlacingTiles() {
			if(EditorTools.tileTool is TileTool && UIHandler.uiState == UIState.Playing && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(500, 300, "Placing Tiles", "When you have an active tile selected, you can place it on the level grid by clicking and/or dragging.", DirRotate.Down);
		}
		
		private void ScrollerBar() {
			if(EditorTools.tileTool is TileTool && UIHandler.uiState == UIState.Playing && Cursor.MouseScrollDelta != 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote((short)(Systems.screen.windowWidth - 480), 300, "Scroller Bar", "Use your mouse scroll to change the tile variant.", DirRotate.Right);
		}

		private void AutoTiling() {
			if(EditorTools.tileTool is TileToolGround && (Systems.input.LocalKeyDown(Keys.LeftControl) || Systems.input.LocalKeyDown(Keys.RightControl)) && Cursor.LeftMouseState == Cursor.MouseDownState.Released) {
				this.IncrementTutorialStep();
				return;
			}
									
			if(EditorTools.tileTool is TileToolGround == false) { EditorTools.SetTileToolBySlotGroup((byte) SlotGroup.Ground); }
			this.SetTutorialNote(500, 170, "Auto-Tiling", "Hold the `control` key while pressing and holding the left mouse button to auto-tile. This can auto-tile large sections of ground at once.", DirRotate.Down);
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

			this.SetTutorialNote( 90, (short)(Systems.screen.windowHalfHeight - 70), "Using the Tab Menu", "While in the tab menu, click on a tileset archetype to work with those types of tiles.", DirRotate.Right);
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

			this.SetTutorialNote(720, (short)(Systems.screen.windowHeight - 250), "Editing Console", "Open the Editor Console for additional options by pressing the tilde key `~`, or clicking the settings option.", DirRotate.Down);
		}
		
		private void ResizeLevel() {
			if(UIHandler.uiState == UIState.Menu && UIHandler.menu is Console && ConsoleTrack.instructionText.IndexOf("resize") == 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(80, (short)(Systems.screen.windowHeight - 250), "Level Resizing", "Try resizing the level width by typing `resize width 100`", DirRotate.Down);
		}
		
		private void SetLevelName() {
			if(UIHandler.uiState == UIState.Menu && UIHandler.menu is Console && ConsoleTrack.instructionText.IndexOf("title") == 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(80, (short)(Systems.screen.windowHeight - 250), "Rename Level", "Try renaming the level by typing `title My Test Level`", DirRotate.Down);
		}

		private void FasterMove() {
			if(UIHandler.uiState == UIState.Playing && (Systems.input.LocalKeyDown(Keys.LeftShift) || Systems.input.LocalKeyDown(Keys.RightShift))) {
				var input = Systems.localServer.MyPlayer.input;
				if(input.isDown(IKey.Left) || input.isDown(IKey.Right) || input.isDown(IKey.Up) || input.isDown(IKey.Down)) {
					this.IncrementTutorialStep();
				}
				return;
			}

			this.SetTutorialNote(500, 250, "Level Viewing", "Hold the `shift` key down while moving the camera (WASD keys) to increase the camera's movement speed.", DirRotate.Down);
		}
		
		private void ChangeRoom() {
			if(this.scene.curRoomID > 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(973, (short)(Systems.screen.windowHeight - 220), "Change Rooms", "Click the `Next Room` button to switch to the next room ID.", DirRotate.Down);
		}
		
		private void HomeRoom() {
			if(this.scene.curRoomID == 0) {
				this.IncrementTutorialStep();
				return;
			}

			this.SetTutorialNote(923, (short)(Systems.screen.windowHeight - 220), "Return to Home", "Click the `Home Room` to return to the first room.", DirRotate.Down);
		}
		
		// Wand Tool
		// Selection Tool
	}
}
