using Microsoft.Xna.Framework.Input;
using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;
using System;

namespace Nexus.GameEngine {

	public class LevelScene : Scene {

		// References
		public readonly LevelUI levelUI;
		public readonly MenuUI menuUI;
		public RoomScene[] rooms;

		// Trackers
		public int levelResetFrame = 0;

		public LevelScene() : base() {

			// Create UI
			this.levelUI = new LevelUI();
			this.menuUI = new MenuUI(this, MenuUI.MenuUIOption.Level);

			// Generate Each Room Class
			this.rooms = new RoomScene[8];

			foreach(var roomKey in Systems.handler.levelContent.data.rooms.Keys) {
				byte roomID = Byte.Parse(roomKey);
				this.rooms[roomID] = new RoomScene(this, roomID);
			}

			// Restart the level, generate all rooms.
			this.RestartLevel(true);

			// Play Music
			if(Systems.handler.levelContent.data.music > 0) {
				Systems.music.Play((byte) Systems.handler.levelContent.data.music);
			}
		}

		protected virtual void LoadMyPlayer() {
			Systems.localServer.ResetPlayers();

			// Assign All Characters according to the match rules:
			foreach(var character in this.rooms[0].objects[(byte)LoadOrder.Character]) {
				Character nChar = (Character)character.Value;

				// Each character can only be assigned to one player:
				if(nChar.player is Player) { continue; }

				// TODO: Determine which player(s) meet the parameters for this character:
				// TODO

				// If My Character has not been assigned, assign it now:
				if(Systems.localServer.MyCharacter is Character == false) {
					Systems.localServer.MyPlayer.AssignCharacter(nChar, true);
					continue;
				}

				// If the Character has no players to assign to it:
				nChar.AssignPlayer(Placeholders.Player);
			}
		}

		protected virtual void RunSceneLoop() {
			Character character = Systems.localServer.MyCharacter;

			// Run Transport Action, if applicable
			if(character is Character) {
				if(character.status.action is TransportAction) {
					((TransportAction)character.status.action).TriggerTransport(character);
				}
			}

			// Check Player Survival
			if(character is Character == false || character.deathFrame > 0) {

				// Prepare the whole level to be rebuilt.
				this.levelResetFrame = Systems.timer.Frame + 2;
			}

			Systems.localServer.MyPlayer.input.UpdateKeyStates(0);
		}

		public override void RunTick() {

			// Scene Loop will perform scene-specific critical checks, such as identifying all players' input.
			// Single Player will only retrieve one player, while MP will review all players connected.
			this.RunSceneLoop();

			// If Console UI is active:
			if(this.uiState == UIState.Console) {

				// Determine if the console needs to be closed (escape or tilde):
				if(Systems.input.LocalKeyPressed(Keys.Escape) || Systems.input.LocalKeyPressed(Keys.OemTilde)) {
					Systems.levelConsole.SetVisible(false);
					this.uiState = UIState.Playing;
				}

				Systems.levelConsole.RunTick();
				return;
			}

			// Menu UI is active:
			else if(this.uiState == UIState.SubMenu || this.uiState == UIState.MainMenu) {
				this.menuUI.RunTick(); // Also handles menu close option.
				return;
			}

			// Play UI is active:

			// Open Menu (Start)
			if(Systems.localServer.MyPlayer.input.isPressed(IKey.Start)) { this.uiState = UIState.SubMenu; }

			// Open Console (Tilde)
			else if(Systems.input.LocalKeyPressed(Keys.OemTilde)) {
				this.uiState = UIState.Console;
				Systems.levelConsole.Open();
			}

			// Some Scenes will disable this, or limit behavior (such as for multiplayer).
			if(this.RunLocalDebugFeatures()) { return; }

			// Update Timer
			Systems.timer.RunTick();

			// Run Each Room in Level
			this.RunRoomLoop();
		}

		protected virtual void RunRoomLoop() {
			
			// If this level is in the process of being reset, we cannot allow rooms to continue their activity.
			if(this.levelResetFrame > 0) {

				// Rebuild the Level
				if(this.levelResetFrame > Systems.timer.Frame) {
					this.RestartLevel();
				}

				return;
			}

			// Run the room the character is currently in.
			this.rooms[Systems.localServer.MyCharacter.room.roomID].RunTick();
		}

		protected virtual bool RunLocalDebugFeatures() {

			// If we're in debug mode:
			if(DebugConfig.Debug) {
				this.DebugToggles();

				// Tick Speed
				if(DebugConfig.TickSpeed != (byte)DebugTickSpeed.StandardSpeed) {

					switch(DebugConfig.TickSpeed) {

						case DebugTickSpeed.HalfSpeed:
							DebugConfig.trackTicks++;
							if(DebugConfig.trackTicks % 2 != 0) { return true; }
							break;

						case DebugTickSpeed.QuarterSpeed:
							DebugConfig.trackTicks++;
							if(DebugConfig.trackTicks % 4 != 0) { return true; }
							break;

						case DebugTickSpeed.EighthSpeed:
							DebugConfig.trackTicks++;
							if(DebugConfig.trackTicks % 8 != 0) { return true; }
							break;

						case DebugTickSpeed.WhenYPressed:
							if(!Systems.localServer.MyPlayer.input.isPressed(IKey.YButton)) { return true; }
							break;


						case DebugTickSpeed.WhileYHeld:
							if(!Systems.localServer.MyPlayer.input.isDown(IKey.YButton)) { return true; }
							break;

						case DebugTickSpeed.WhileYHeldSlow:
							if(!Systems.localServer.MyPlayer.input.isDown(IKey.YButton)) { return true; }
							DebugConfig.trackTicks++;
							if(DebugConfig.trackTicks % 4 != 0) { return true; }
							break;
					}
				}
			}

			return false;
		}

		protected void DebugToggles() {

			// Change Active Debug Mode (press F8)
			InputClient input = Systems.input;

			if(input.LocalKeyPressed(Keys.F1)) { Systems.levelConsole.SendCommand(Systems.settings.input.macroF1); }
			else if(input.LocalKeyPressed(Keys.F2)) { Systems.levelConsole.SendCommand(Systems.settings.input.macroF2); }
			else if(input.LocalKeyPressed(Keys.F3)) { Systems.levelConsole.SendCommand(Systems.settings.input.macroF3); }
			else if(input.LocalKeyPressed(Keys.F4)) { Systems.levelConsole.SendCommand(Systems.settings.input.macroF4); }
			else if(input.LocalKeyPressed(Keys.F5)) { Systems.levelConsole.SendCommand(Systems.settings.input.macroF5); }
			else if(input.LocalKeyPressed(Keys.F6)) { Systems.levelConsole.SendCommand(Systems.settings.input.macroF6); }
			else if(input.LocalKeyPressed(Keys.F7)) { Systems.levelConsole.SendCommand(Systems.settings.input.macroF7); }
			else if(input.LocalKeyPressed(Keys.F8)) { Systems.levelConsole.SendCommand(Systems.settings.input.macroF8); }

			//else if(input.LocalKeyPressed(Keys.F5)) { DebugConfig.ResetDebugValues(); }
			//else if(input.LocalKeyPressed(Keys.F6)) { DebugConfig.ToggleDebugFrames(); }
			//else if(input.LocalKeyPressed(Keys.F7)) { DebugConfig.ToggleTickSpeed(true); }
			//else if(input.LocalKeyPressed(Keys.F8)) { DebugConfig.ToggleTickSpeed(false); }
		}

		public override void Draw() {

			// My Character
			Character MyCharacter = Systems.localServer.MyCharacter;

			// Draw the Room that the local character is in:
			if(MyCharacter is Character) {
				this.rooms[MyCharacter.room.roomID].Draw();
			}

			// Draw UI
			if(this.uiState == UIState.Playing) { this.levelUI.Draw(); }
			else if(this.uiState == UIState.SubMenu || this.uiState == UIState.MainMenu) { this.menuUI.Draw(); }
			else if(this.uiState == UIState.Console) { Systems.levelConsole.Draw(); }
		}

		public virtual void RestartLevel(bool fullReset = false) {
			this.levelResetFrame = 0;

			// Reset the level to it's full restarted position.
			if(fullReset) { Systems.handler.levelState.FullReset(); }

			// Timer Reset
			Systems.timer.Unpause();
			Systems.timer.ResetTimer();
			
			// Build Each Room
			foreach(RoomScene room in this.rooms) {
				if(room is RoomScene == false) { continue; }
				room.BuildRoom();
			}

			// Retrieves the local player (as opposed to other players connected online, who may also be linked up).
			this.LoadMyPlayer();

			// Update Camera Limitations
			Systems.camera.UpdateScene(this.rooms[Systems.localServer.MyCharacter.room.roomID], (byte)TilemapEnum.GapUp * (byte)TilemapEnum.TileHeight, (byte)TilemapEnum.GapLeft * (byte)TilemapEnum.TileWidth);

			// Reset Level State, Maintain Checkpoints.
			LevelState levelState = Systems.handler.levelState;
			levelState.SoftReset();

			// Reset Character's Position To Appropriate Checkpoint (if applicable)
			FlagJson checkpoint = levelState.checkpoint;

			if(checkpoint.active) {
				levelState.checkpoint.active = false;
				ActionMap.Transport.StartAction(Systems.localServer.MyCharacter, checkpoint.roomId, levelState.checkpoint.gridX * (byte)TilemapEnum.TileWidth, levelState.checkpoint.gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight);
			}
		}

		// NOTE: You probably want to call this from TransportAction - it will handle your room transitions correctly.
		public void MoveCharacterToNewRoom(Character character, byte roomID) {

			// Make sure the character isn't already in this room:
			if(character.room.roomID == roomID) { return; }

			// Remove Character from Scene's Objects
			character.room.RemoveFromScene(character, true);

			// Add Character to New Scene's Objects
			this.rooms[roomID].AddToScene(character, true);

			// Update Character's New Room
			character.MoveToNewRoom(roomID);
		}
	}
}
