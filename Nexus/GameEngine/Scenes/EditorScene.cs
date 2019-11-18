using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class EditorScene : Scene {

		public readonly EditorUI editorUI;
		public Dictionary<byte, EditorRoomScene> rooms;
		public byte roomNum = 0;

		public MouseState mouseState;

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
			Systems.camera.UpdateScene(this.rooms[this.roomNum]);
			Systems.camera.SetInputMoveSpeed(15);

			this.mouseState = Mouse.GetState();
		}

		public int MouseGridX { get { return (int) Math.Floor(this.mouseState.X * 0.020833); } }
		public int MouseGridY { get { return (int) Math.Floor(this.mouseState.Y * 0.020833); } }

		public void SetRoom( byte roomNum ) {
			this.roomNum = roomNum;
		}

		public override void RunTick() {

			// Loop through every player and update inputs for this frame tick:
			foreach(var player in Systems.localServer.players) {
				player.Value.input.UpdateKeyStates(0);
			}

			// Update the Mouse State
			this.mouseState = Mouse.GetState();

			// Debug Console (only runs if visible)
			Console.RunTick();

			// If we're in debug mode:
			this.EditorInput();

			// Run this Room's Tick
			this.rooms[this.roomNum].RunTick();
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
			this.rooms[this.roomNum].Draw();

			// Draw UI
			this.editorUI.Draw();
			Console.Draw();
		}
	}
}
