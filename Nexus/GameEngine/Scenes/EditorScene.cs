using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class EditorScene : Scene {

		public readonly EditorUI editorUI;
		public Dictionary<byte, EditorRoomScene> rooms;
		public byte currentRoom = 0;

		public EditorScene() : base() {

			// Create UI
			this.editorUI = new EditorUI(this);

			// Generate Each Room
			this.rooms = new Dictionary<byte, EditorRoomScene>();

			foreach(var roomKey in Systems.handler.levelContent.data.room.Keys) {
				byte parsedKey = Byte.Parse(roomKey);
				this.rooms[parsedKey] = new EditorRoomScene(this, roomKey);
			}

			// Important Components
			Systems.camera.UpdateScene(this.rooms[this.currentRoom]);
		}

		public override void RunTick() {

			// Loop through every player and update inputs for this frame tick:
			foreach(var player in Systems.localServer.players) {
				player.Value.input.UpdateKeyStates(0);
			}

			// Debug Console (only runs if visible)
			Console.RunTick();

			// If we're in debug mode:
			this.EditorInput();

			// TODO: RUN EACH ROOM IN LEVEL
			// TODO: RUN EACH ROOM IN LEVEL
			// TODO: RUN EACH ROOM IN LEVEL
			this.rooms[this.currentRoom].RunTick();
		}

		public void EditorInput() {

			// Change Active Debug Mode (press F8)
			InputClient input = Systems.input;

			// Horizontal Camera Shift (0, 33, 66, 100)
			if(input.LocalKeyPressed(Keys.F1)) { /* Convert to a percent camera swap for the level */ }
			else if(input.LocalKeyPressed(Keys.F2)) { /* Convert to a percent camera swap for the level */ }
			else if(input.LocalKeyPressed(Keys.F3)) { /* Convert to a percent camera swap for the level */ }
			else if(input.LocalKeyPressed(Keys.F4)) { /* Convert to a percent camera swap for the level */ }

			// Vertical Camera Shift (0, 33, 66, 100)
			else if(input.LocalKeyPressed(Keys.F5)) { /* Convert to a percent camera swap for the level */ }
			else if(input.LocalKeyPressed(Keys.F6)) { /* Convert to a percent camera swap for the level */ }
			else if(input.LocalKeyPressed(Keys.F7)) { /* Convert to a percent camera swap for the level */ }
			else if(input.LocalKeyPressed(Keys.F8)) { /* Convert to a percent camera swap for the level */ }
		}

		public override void Draw() {

			// Render the Current Rooom
			this.rooms[this.currentRoom].Draw();

			// Draw UI
			this.editorUI.Draw();
			Console.Draw();
		}
	}
}
