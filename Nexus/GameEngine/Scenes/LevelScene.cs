using Microsoft.Xna.Framework.Input;
using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class LevelScene : Scene {

		// References
		public readonly LevelUI levelUI;
		public Dictionary<byte, RoomScene> rooms;

		public LevelScene() : base() {

			// Create UI
			this.levelUI = new LevelUI(this);

			// Generate Each Room
			this.rooms = new Dictionary<byte, RoomScene>();

			foreach(var roomKey in Systems.handler.levelContent.data.rooms.Keys) {
				byte parsedKey = Byte.Parse(roomKey);
				this.rooms[parsedKey] = new RoomScene(this, roomKey);
			}

			// TODO HIGH PRIORITY: All characters need to be assigned to the level? Or are they somewhere else?
			// TODO HIGH PRIORITY: Assign All Characters according to the match rules:
			foreach(var character in this.rooms[0].objects[(byte)LoadOrder.Character]) {
				Systems.localServer.MyPlayer.AssignCharacter((Character) character.Value);
			}

			// Important Components
			Systems.camera.UpdateScene(this.rooms[0], (byte)TilemapEnum.WorldGapUp * (byte)TilemapEnum.TileHeight, (byte)TilemapEnum.WorldGapLeft * (byte)TilemapEnum.TileWidth);
		}

		public override void RunTick() {

			// Loop through every player and update inputs for this frame tick:
			foreach(var player in Systems.localServer.players) {
				//player.Value.input.UpdateKeyStates(Systems.timer.Frame);
				player.Value.input.UpdateKeyStates(0); // TODO: Update LocalServer so frames are interpreted and assigned here.
			}

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
							if(DebugConfig.trackTicks % 2 != 0) { return; }
							break;

						case DebugTickSpeed.QuarterSpeed:
							DebugConfig.trackTicks++;
							if(DebugConfig.trackTicks % 4 != 0) { return; }
							break;

						case DebugTickSpeed.EighthSpeed:
							DebugConfig.trackTicks++;
							if(DebugConfig.trackTicks % 8 != 0) { return; }
							break;

						case DebugTickSpeed.WhenYPressed:
							if(!Systems.localServer.MyPlayer.input.isPressed(IKey.YButton)) { return; }
							break;


						case DebugTickSpeed.WhileYHeld:
							if(!Systems.localServer.MyPlayer.input.isDown(IKey.YButton)) { return; }
							break;

						case DebugTickSpeed.WhileYHeldSlow:
							if(!Systems.localServer.MyPlayer.input.isDown(IKey.YButton)) { return; }
							DebugConfig.trackTicks++;
							if(DebugConfig.trackTicks % 4 != 0) { return; }
							break;
					}
				}
			}

			// Update Timer
			Systems.timer.RunTick();

			// TODO: RUN EACH ROOM IN LEVEL (or at least any active ones)
			// TODO: RUN EACH ROOM IN LEVEL (or at least any active ones)
			// TODO: RUN EACH ROOM IN LEVEL (or at least any active ones)
			this.rooms[0].RunTick();
		}

		public void DebugToggles() {

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

			if(MyCharacter is Character) {
				// TODO: RENDER MY CHARACTER'S ROOM
				// TODO: RENDER MY CHARACTER'S ROOM
				// TODO: RENDER MY CHARACTER'S ROOM
			}

			// TODO HIGH PRIORITY: UPDATE THIS. RENDER THE CORRECT ROOM
			this.rooms[0].Draw();

			// Draw UI
			this.levelUI.Draw();
			Systems.levelConsole.Draw();
		}

		public void RunCharacterDeath( Character character ) {
			// TODO HIGH PRIORITY:
			//this.RestartLevel();        // true if all players are just self. for multiplayer, this changes... maybe a new scene for multiplayer?
			character.ResetCharacter();
		}

		public void RestartLevel() {
			// TODO: RUN EVERY RESTARTROOM() IN EACH ROOM
			// TODO: RUN EVERY RESTARTROOM() IN EACH ROOM
			// TODO: RUN EVERY RESTARTROOM() IN EACH ROOM
		}
	}
}
