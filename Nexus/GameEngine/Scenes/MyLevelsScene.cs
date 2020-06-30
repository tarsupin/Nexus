using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;
using static Nexus.Engine.PagingSystem;

namespace Nexus.GameEngine {

	public class MyLevelsScene : Scene {

		// Static Values
		public const string openSlot = "{ Open Slot }";
		public const string openImg = "UI/MyLevels";
		public const string activeImg = "Exclaim/Active";
		public const short slotsAllowed = 28 * 4;

		// References, Component
		public Atlas atlas;
		public readonly PlayerInput playerInput;
		public readonly MenuUI menuUI;
		public PagingSystem paging;

		// Levels
		public Dictionary<short, LevelFormat> levels = new Dictionary<short, LevelFormat>();

		public MyLevelsScene() : base() {

			// Prepare Components
			this.playerInput = Systems.localServer.MyPlayer.input;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
			this.menuUI = new MenuUI(this, MenuUI.MenuUIOption.PlanetSelect);

			// Prepare Level Paging System (full paging system)
			this.paging = new PagingSystem(7, 4, (short) MyLevelsScene.slotsAllowed);

			// Load Level Data
			for(short i = this.paging.MinVal; i < this.paging.MaxVal; i++) {
				this.ApplyLevelDataByNumber(i);
			}
		}

		public override void StartScene() {

			// Reset Timer
			Systems.timer.ResetTimer();
		}

		public override void EndScene() {
			//if(Systems.music.whatever) { Systems.music.SomeTrack.Stop(); }
		}

		public bool ApplyLevelDataByNumber(short levelNum) {
			string levelId = "__" + levelNum.ToString();

			// Check if the level has already been attached into the dictionary. If so, return TRUE.
			if(this.levels.ContainsKey(levelNum)) { return true; }

			// Make sure the level exists.
			LevelFormat levelData = LevelContent.GetLevelData(levelId);

			if(levelData == null) { return false; }

			// Attach the level data to the levels dictionary.
			this.levels.Add(levelNum, levelData);

			return true;
		}

		public override void RunTick() {

			// Update Timer
			Systems.timer.RunTick();

			// Loop through every player and update inputs for this frame tick:
			foreach(var player in Systems.localServer.players) {
				//player.Value.input.UpdateKeyStates(Systems.timer.Frame);
				player.Value.input.UpdateKeyStates(0); // TODO: Update LocalServer so frames are interpreted and assigned here.
			}

			// Paging Input (only when in the paging area)
			InputClient input = Systems.input;

			if(this.uiState == UIState.Playing) {
				PagingPress pageInput = this.paging.PagingInput(playerInput);

				if(pageInput != PagingPress.None) {
					Systems.sounds.click2.Play(0.5f, 0, 0.5f);

					if(pageInput == PagingPress.PageChange) {

						// Apply New Level Data
						for(short i = this.paging.MinVal; i < this.paging.MaxVal; i++) {
							this.ApplyLevelDataByNumber(i);
						}
					}
				}

				// Activate Planet / World
				if(playerInput.isPressed(IKey.AButton) == true) {
					short curVal = this.paging.CurrentSelectionVal;
					SceneTransition.ToLevelEditor("", "__" + curVal.ToString(), curVal);
					return;
				}
			}

			// Menu UI
			this.menuUI.RunTick();

			// Open Menu
			if(input.LocalKeyPressed(Keys.Tab) || input.LocalKeyPressed(Keys.Escape) || playerInput.isPressed(IKey.Start) || playerInput.isPressed(IKey.Select)) {
				this.uiState = UIState.MainMenu;
			}
		}

		public override void Draw() {

			// SamplerState.PointClamp will force Sprites to draw without blurring effect. Without this, all scaling upward is blurry.
			Systems.spriteBatch.End();
			Systems.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

			// Draw Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(0, 0, Systems.screen.windowWidth, Systems.screen.windowHeight), Color.DarkSlateGray);

			short posX = 100;
			short posY = 150;

			// Draw Current Paging Selection
			short highlightX = (short)(this.paging.selectX * 200 + posX);
			short highlightY = (short)(this.paging.selectY * 200 + posY);

			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(highlightX - 60, highlightY - 60, 155, 195), Color.DarkRed);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(highlightX - 50, highlightY - 50, 135, 175), Color.DarkSlateGray);

			// Draw Levels
			for(short levelNum = this.paging.MinVal; levelNum < this.paging.MaxVal; levelNum++) {
				this.DrawLevel(levelNum, posX, posY);

				// Update Next Position
				posX += 200;
				if(posX >= 1500) { posY += 200; posX = 100; }
			}

			// Draw Menu UI
			this.menuUI.Draw();
		}

		public void DrawLevel( short levelNum, short posX, short posY ) {

			// Display Slot Number
			Systems.fonts.console.Draw("#" + levelNum, posX + 6, posY + 98, Color.White);

			// If there is no level data, mark it as an open slot:
			if(!this.levels.ContainsKey(levelNum)) {

				// Draw Level
				this.atlas.DrawAdvanced(MyLevelsScene.openImg, posX - 5, posY, Color.White, 0f, 2);

				//// Draw Character
				//Head.GetHeadBySubType(6).Draw(false, posX - 30, posY - 15, 0, 0);
				//Suit.GetSuitBySubType(51).Draw("StandLeft", posX - 30, posY - 15, 0, 0);

				// Display Empty Level Slot
				short emptySize = (short)Systems.fonts.baseText.font.MeasureString(MyLevelsScene.openSlot).X;
				Systems.fonts.baseText.Draw(MyLevelsScene.openSlot, posX + 16 - (byte)Math.Floor(emptySize * 0.5f), posY + 73, Color.White);
				
				return;
			}

			LevelFormat levelData = this.levels[levelNum];

			// Draw Level
			this.atlas.DrawAdvanced(MyLevelsScene.activeImg, posX - 5, posY, Color.White, 0f, 2);

			// Display Name
			short titleSize = (short)Systems.fonts.baseText.font.MeasureString(levelData.title).X;
			Systems.fonts.baseText.Draw(levelData.title, posX + 16 - (byte)Math.Floor(titleSize * 0.5f), posY + 73, Color.White);

			//// Display Character
			//if(levelData.icon[0] > 0 && levelData.icon[1] > 0) {
			//	Head.GetHeadBySubType(levelData.icon[0]).Draw(false, posX - 30, posY - 15, 0, 0);
			//	Suit.GetSuitBySubType(levelData.icon[1]).Draw("StandLeft", posX - 30, posY - 15, 0, 0);

			//	if(levelData.icon[2] > 0) {
			//		Hat.GetHatBySubType(levelData.icon[2]).Draw(false, posX - 30, posY - 15, 0, 0);
			//	} else if(levelData.icon[0] == (byte)HeadSubType.PooHead) {
			//		Hat.GetHatBySubType((byte)HatSubType.PooHat).Draw(false, posX - 30, posY - 15, 0, 0);
			//	}
			//}
		}
	}
}
