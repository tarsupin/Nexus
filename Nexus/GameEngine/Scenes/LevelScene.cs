using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Nexus.GameEngine {

	public class LevelScene : Scene {

		// References
		public readonly LevelUI levelUI;
		public Stopwatch stopwatch;
		public Dictionary<byte, RoomScene> rooms;

		public LevelScene() : base() {

			// TODO CLEANUP: Debugging stopwatch should be removed. Or converted to static access, like Systems.timer.stopwatch.
			this.stopwatch = new Stopwatch();

			// Create UI
			this.levelUI = new LevelUI(this);

			// Generate Each Room
			this.rooms = new Dictionary<byte, RoomScene>();

			foreach(var roomKey in Systems.handler.levelContent.data.room.Keys) {
				byte parsedKey = Byte.Parse(roomKey);
				this.rooms[parsedKey] = new RoomScene(this, roomKey);
			}

			// TODO HIGH PRIORITY: All characters need to be assigned to the level? Or are they somewhere else?
			// TODO HIGH PRIORITY: Assign All Characters according to the match rules:
			foreach(var character in this.rooms[0].objects[(byte)LoadOrder.Character]) {
				Systems.localServer.MyPlayer.AssignCharacter((Character) character.Value);
			}

			// Important Components
			Systems.camera.UpdateScene(this.rooms[0]);
		}

		public override void RunTick() {

			// TODO: LEVEL RUN TICK
			// TODO: LEVEL RUN TICK
			// TODO: LEVEL RUN TICK

			// Loop through every player and update inputs for this frame tick:
			foreach(var player in Systems.localServer.players) {
				player.Value.input.UpdateKeyStates(0);
			}

			// If we're in debug mode and want to run every tick by control:
			if(DebugConfig.Debug && DebugConfig.TickSpeed != (byte) DebugTickSpeed.StandardSpeed) {

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

			// Update Timer
			Systems.timer.RunTick();

			// TODO: RUN EACH ROOM IN LEVEL
			// TODO: RUN EACH ROOM IN LEVEL
			// TODO: RUN EACH ROOM IN LEVEL
			this.rooms[0].RunTick();

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
		}

		public void RunCharacterDeath( Character character ) {
			// TODO HIGH PRIORITY:
			//this.RestartLevel();        // true if all players are just self. for multiplayer, this changes... maybe a new scene for multiplayer?
		}

		public void RestartLevel() {
			// TODO: RUN EVERY RESTARTROOM() IN EACH ROOM
			// TODO: RUN EVERY RESTARTROOM() IN EACH ROOM
			// TODO: RUN EVERY RESTARTROOM() IN EACH ROOM
		}
	}
}
