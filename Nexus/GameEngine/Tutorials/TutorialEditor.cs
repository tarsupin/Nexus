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
		}

		public void RunTick() {

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

			if(EditorUI.currentSlotGroup != (byte)SlotGroup.Ground) {
				this.SetTutorialStep(4); // TODO: UPDATE THIS VALUE
			}

			if(EditorTools.tileTool is TileTool) {
				this.SetTutorialStep(1);
				return;
			}

			this.SetTutorialNote(50, (short)(Systems.screen.windowHeight - 220), "Selecting Tiles", "Select a tile from the utility bar by clicking on it, or by pressing the shortcut key (e.g. 1, 2, etc).", DirRotate.Down);
		}
		
		private void PlacingTiles() {
			if(EditorTools.tileTool is TileTool && UIHandler.uiState == UIState.Playing && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				this.SetTutorialStep(2);
				return;
			}

			this.SetTutorialNote(500, 400, "Placing Tiles", "When you have an active tile selected, you can place it on the level grid by clicking and/or dragging.", DirRotate.Down);
		}
		
		private void ScrollerBar() {
			if(EditorTools.tileTool is TileTool && UIHandler.uiState == UIState.Playing && Cursor.MouseScrollDelta != 0) {
				this.SetTutorialStep(3);
				return;
			}

			this.SetTutorialNote((short)(Systems.screen.windowWidth - 480), 300, "Scroller Bar", "Use your mouse scroll to change the tile variant.", DirRotate.Right);
		}
	}
}
