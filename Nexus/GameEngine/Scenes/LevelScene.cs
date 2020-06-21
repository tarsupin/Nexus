using Microsoft.Xna.Framework.Input;
using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;
using static Nexus.Gameplay.MusicAssets;

namespace Nexus.GameEngine {

	public class LevelScene : Scene {

		// References
		public readonly LevelUI levelUI;
		public RoomScene[] rooms;

		// Trackers
		public int levelResetFrame = 0;

		public LevelScene() : base() {

			// Create UI
			this.levelUI = new LevelUI(this);

			// Generate Each Room Class
			this.rooms = new RoomScene[4];

			foreach(var roomKey in Systems.handler.levelContent.data.rooms.Keys) {
				byte roomID = Byte.Parse(roomKey);
				this.rooms[roomID] = new RoomScene(this, roomID);
			}

			// Reset the level to it's full restarted position.
			Systems.handler.levelState.FullReset();

			// Restart the level, generate all rooms.
			this.RestartLevel();

			// Play Music
			Systems.music.Play(MusicTrack.Intensity1);
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

			// Check Player Survival
			if(Systems.localServer.MyCharacter is Character == false || Systems.localServer.MyCharacter.deathFrame > 0) {

				// Prepare the whole level to be rebuilt.
				this.levelResetFrame = Systems.timer.Frame + 2;
			}

			Systems.localServer.MyPlayer.input.UpdateKeyStates(0);
		}

		public override void RunTick() {

			// Scene Loop will perform scene-specific critical checks, such as identifying all players' input.
			// Single Player will only retrieve one player, while MP will review all players connected.
			this.RunSceneLoop();

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

			// Debug Console (only runs if visible)
			Systems.levelConsole.RunTick();

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
			this.levelUI.Draw();
			Systems.levelConsole.Draw();
		}

		protected virtual void RestartLevel() {
			this.levelResetFrame = 0;

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
			Systems.handler.levelState.SoftReset();

			// TODO: CHANGE TO NEW ROOM? NEW CHECKPOINT?
			// TODO: CHANGE TO NEW ROOM? NEW CHECKPOINT?

			// TODO: Reset Character's Position To Appropriate Checkpoint
			// If the character's new position is being declared (such as for a door/portal),
			// then we must identify what checkpoint and room the player should be at.
			//let chk = this.game.level.state.checkpoint;

			//if(chk.active) {
			//	this.setRoom(chk.room);

			//	// Update Character Generation Position to match checkpoint
			//	posX = chk.pos.x;
			//	posY = chk.pos.y + 48;

			//	// Return to the original room (since no checkpoint was located). Or, if playtesting, to the same room.
			//} else {

			//	// If the user is the level's author, they can restart in the same room (for playtesting purposes).
			//	let userHash = this.game.cache.get('myUserHash');
			//	if(userHash && this.game.level.id.startsWith(userHash)) {
			//		this.setRoom(this.game.level.state.roomId);
			//	} else {
			//		this.setRoom(0);
			//	}
			//}

			// TODO: Update Camera if in the same room
			// this.camera.bindToWorld(); // Update Camera Bounds

			// TODO: Camera must follow (or cut) to the position. Only applies if in the same room.
			// this.camera.cutToPosition(this.character.pos.x, this.character.pos.y);

		}

		public void MoveCharacterToNewRoom(Character character, byte roomID) {

			// Remove Character from Scene's Objects
			character.room.RemoveFromScene(character);

			// Add Character to New Scene's Objects
			this.rooms[roomID].AddToScene(character, true);

			// Update Character's New Room
			character.MoveToNewRoom(roomID);
		}
	}
}
