﻿using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;
using static Nexus.GameEngine.FuncTool;

namespace Nexus.GameEngine {

	public class EditorScene : Scene {

		public readonly EditorUI editorUI;
		public readonly MenuUI menuUI;
		public LevelContent levelContent;
		public Dictionary<byte, EditorRoomScene> rooms;
		public byte roomNum = 0;

		public EditorScene() : base() {

			// References
			this.levelContent = Systems.handler.levelContent;

			// UI State
			this.mouseAlwaysVisible = true;
			this.SetUIState(UIState.Playing);

			// Create UI
			this.editorUI = new EditorUI(this);
			this.menuUI = new MenuUI(this, MenuUI.MenuUIOption.Main);

			// Generate Each Room
			this.rooms = new Dictionary<byte, EditorRoomScene>();

			foreach(var roomKey in this.levelContent.data.rooms.Keys) {
				byte parsedKey = Byte.Parse(roomKey);
				this.rooms[parsedKey] = new EditorRoomScene(this, roomKey);
			}
		}

		public override void StartScene() {

			// Set Default Tool to Selection Tool
			EditorTools.SetFuncTool(FuncTool.funcToolMap[(byte)FuncToolEnum.Select]);

			// Important Components
			Systems.camera.UpdateScene(this.CurrentRoom);
			Systems.camera.SetInputMoveSpeed(15);

			Systems.SetMouseVisible(true);
			Cursor.UpdateMouseState();

			// End Music
			Systems.music.StopMusic();
		}

		public EditorRoomScene CurrentRoom { get { return this.rooms[this.roomNum]; } }

		public override void RunTick() {

			// Loop through every player and update inputs for this frame tick:
			foreach(var player in Systems.localServer.players) {
				player.Value.input.UpdateKeyStates(0);
			}

			// Update Timer
			Systems.timer.RunTick();

			// Update the Mouse State Every Tick
			Cursor.UpdateMouseState();

			// If Console UI is active:
			if(this.uiState == UIState.Console) {

				// Determine if the console needs to be closed (escape or tilde):
				if(Systems.input.LocalKeyPressed(Keys.Escape) || Systems.input.LocalKeyPressed(Keys.OemTilde)) {
					Systems.editorConsole.SetVisible(false);
					this.SetUIState(UIState.Playing);
				}

				Systems.editorConsole.RunTick();
				return;
			}

			// Menu UI is active:
			else if(this.uiState == UIState.SubMenu || this.uiState == UIState.MainMenu) {
				this.menuUI.RunTick(); // Also handles menu close option.
				return;
			}

			// Play UI is active:

			// Open Menu (Start)
			if(Systems.localServer.MyPlayer.input.isPressed(IKey.Start)) { this.SetUIState(UIState.MainMenu); }

			// Open Console (Tilde)
			else if(Systems.input.LocalKeyPressed(Keys.OemTilde)) {
				this.SetUIState(UIState.Console);
				Systems.editorConsole.Open();
			}

			// Update UI Components
			this.editorUI.RunTick();

			// Run Input for Full Editor and Current Room
			this.EditorInput();
			this.CurrentRoom.RunTick();
		}

		public void EditorInput() {
			InputClient input = Systems.input;

			// Release TempTool Control every tick:
			if(EditorTools.tempTool != null) {
				EditorTools.ClearTempTool();
			}

			// Get the Local Keys Held Down
			Keys[] localKeys = input.GetAllLocalKeysDown();
			if(localKeys.Length == 0) { return; }

			// Key Presses that AREN'T using control keys:
			if(!input.LocalKeyDown(Keys.LeftControl) && !input.LocalKeyDown(Keys.RightControl)) {

				// Func Tool Key Binds
				if(FuncTool.funcToolKey.ContainsKey(localKeys[0])) {
					EditorTools.SetTempTool(FuncTool.funcToolMap[FuncTool.funcToolKey[localKeys[0]]]);
				}

				// Tile Tool Key Binds
				else if(EditorUI.currentSlotGroup > 0) {
					this.CheckTileToolKeyBinds(localKeys[0]);
				}
			}

			// Open Wheel Menu
			if(input.LocalKeyPressed(Keys.Tab)) { this.editorUI.contextMenu.OpenMenu(); }

			// If holding shift down, increase camera movement speed by 3.
			byte moveMult = (input.LocalKeyDown(Keys.LeftShift) || input.LocalKeyDown(Keys.RightShift)) ? (byte)3 : (byte)1;

			// Camera Movement
			Systems.camera.MoveWithInput(Systems.localServer.MyPlayer.input, moveMult);
			Systems.camera.StayBoundedAuto(350, 250);
		}

		public void CheckTileToolKeyBinds(Keys keyPressed) {
			if(keyPressed == Keys.D1) { EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup, 0); }
			else if(keyPressed == Keys.D2) { EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup, 1); }
			else if(keyPressed == Keys.D3) { EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup, 2); }
			else if(keyPressed == Keys.D4) { EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup, 3); }
			else if(keyPressed == Keys.D5) { EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup, 4); }
			else if(keyPressed == Keys.D6) { EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup, 5); }
			else if(keyPressed == Keys.D7) { EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup, 6); }
			else if(keyPressed == Keys.D8) { EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup, 7); }
			else if(keyPressed == Keys.D9) { EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup, 8); }
			else if(keyPressed == Keys.D0) { EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup, 9); }
		}

		public override void Draw() {

			// Render the Current Rooom
			this.CurrentRoom.Draw();

			// Draw UI
			if(this.uiState == UIState.Playing) { this.editorUI.Draw(); }
			else if(this.uiState == UIState.SubMenu || this.uiState == UIState.MainMenu) { this.menuUI.Draw(); }
			else if(this.uiState == UIState.Console) { Systems.editorConsole.Draw(); }
		}

		private void PrepareEmptyRoom(byte newRoomId) {
			string roomStr = newRoomId.ToString();

			// Build Initial Room Format
			if(!Systems.handler.levelContent.data.rooms.ContainsKey(roomStr)) {
				Systems.handler.levelContent.data.rooms[roomStr] = new RoomFormat();
			}

			// If there is no data for the room, create an empty object for it.
			Systems.handler.levelContent.data.rooms[roomStr] = LevelContent.BuildRoomData();

			// We must generate the room scene as well:
			if(!this.rooms.ContainsKey(newRoomId)) {
				this.rooms[newRoomId] = new EditorRoomScene(this, newRoomId.ToString());
			}
		}

		// Swaps this room to the right (switches with next room)
		public void SwapRoomOrder() {

			// Cannot swap this room if the room is at the far end.
			if(this.roomNum > 8) { return; }

			byte newRoomId = (byte) (this.roomNum + 1);

			this.PrepareEmptyRoom(newRoomId);

			// Swap Rooms Accordingly
			var tempRoomData = Systems.handler.levelContent.data.rooms[newRoomId.ToString()];
			Systems.handler.levelContent.data.rooms[newRoomId.ToString()] = Systems.handler.levelContent.data.rooms[this.roomNum.ToString()];
			Systems.handler.levelContent.data.rooms[this.roomNum.ToString()] = tempRoomData;

			var tempRoom = this.rooms[this.roomNum];
			this.rooms[this.roomNum] = this.rooms[newRoomId];
			this.rooms[newRoomId] = tempRoom;

			this.SwitchRoom(newRoomId);
		}

		public void SwitchRoom(byte newRoomId) {
			newRoomId = Math.Max((byte) 0, Math.Min((byte) 9, newRoomId)); // Number must be between 0 and 9

			this.PrepareEmptyRoom(newRoomId);
			this.roomNum = newRoomId;

			// Important Components
			Systems.camera.UpdateScene(this.CurrentRoom);

			// Update Camera Memory in Editor
			//this.editor.swapCameraMemoryRooms(this.roomNum, newRoomId);

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
