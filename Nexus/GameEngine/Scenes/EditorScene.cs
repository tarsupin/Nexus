using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class EditorScene : Scene {

		public readonly EditorUI editorUI;
		public Dictionary<byte, EditorRoomScene> rooms;
		public byte roomNum = 0;

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
			Systems.camera.UpdateScene(this.CurrentRoom);
			Systems.camera.SetInputMoveSpeed(15);

			Systems.SetMouseVisible(true);
			EditorCursor.UpdateMouseState();

			// TODO CLEANUP: Remove
			var a = new TileToolBlocks(this);
		}

		public EditorRoomScene CurrentRoom { get { return this.rooms[this.roomNum]; } }

		public void SetRoom( byte roomNum ) {
			this.roomNum = roomNum;
		}

		public override void RunTick() {

			// Loop through every player and update inputs for this frame tick:
			foreach(var player in Systems.localServer.players) {
				player.Value.input.UpdateKeyStates(0);
			}

			// Update the Mouse State
			EditorCursor.UpdateMouseState();

			// TODO CLEANUP: REMOVE
			if(EditorCursor.mouseState.LeftButton == ButtonState.Pressed) {
				ChatConsole.SendMessage("admin", "placed at " + EditorCursor.MouseGridX + ", " + EditorCursor.MouseGridY, Color.DarkGreen);
			}

			// Debug Console (only runs if visible)
			Console.RunTick();

			// If we're in debug mode:
			this.EditorInput();

			// Run this Room's Tick
			this.CurrentRoom.RunTick();
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
			this.CurrentRoom.Draw();

			// Draw UI
			this.editorUI.Draw();
			Console.Draw();
		}

		//// Swaps this room to the right (switches with next room)
		//swapRoom(): void {
		//	var curRoomId = this.game.level.state.roomId;

		//	// Cannot swap this room if the room is unavailable.
		//	if(curRoomId > 9) { return; }

		//	// If there is no data for the room, create an empty object for it.
		//	if(!this.level.levelData.rooms[curRoomId + 1]) {
		//		this.level.levelData.rooms[curRoomId + 1] = {} as RoomData;
		//	}

		//	// Swap Rooms Accordingly
		//	var tempRoom = this.level.levelData.rooms[curRoomId + 1];
		//	this.level.levelData.rooms[curRoomId + 1] = this.level.levelData.rooms[curRoomId];
		//	this.level.levelData.rooms[curRoomId] = tempRoom;

		//	this.switchRoom( curRoomId + 1 );

		//	// Update Camera Memory in Editor
		//	this.editor.swapCameraMemoryRooms( curRoomId, curRoomId + 1 );
		//}

		public void SwitchRoom(byte newRoomId) {
			this.roomNum = Math.Max((byte) 0, Math.Min((byte) 9, newRoomId)); // Number must be between 0 and 9

			// If there is no data for the room, create an empty object for it.
			if(Systems.handler.levelContent.data.room[this.roomNum.ToString()] == null) {
				Systems.handler.levelContent.data.room.Add(this.roomNum.ToString(), new RoomFormat());
			}

			// TODO: Update the editor camera's position:
			//// If there is no saved camera position, set one based on character's position for that room (if available), or default it.
			//if(!this.editor.cameraMemory[this.roomNum]) {
			//	if(this.level.currentRoom.charStart) {
			//		let char = this.level.currentRoom.charStart;
			//		this.editor.setCameraMemory(this.roomNum, char.x - 300, char.y - 600);
			//	} else {
			//		this.editor.setCameraMemory(this.roomNum, 0, 0);
			//	}
			//}

			//// Move Camera to Saved Camera Position.
			//this.camera.x = this.editor.cameraMemory[this.roomNum].x;
			//this.camera.y = this.editor.cameraMemory[this.roomNum].y;
		}
	}
}
