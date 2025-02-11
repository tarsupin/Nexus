﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Door : TileObject {

		protected string[] Texture;
		protected DoorSubType subType;

		public enum DoorSubType : byte {
			Blue = 0,
			Green = 1,
			Red = 2,
			Yellow = 3,
			Open = 4,
		}

		public Door() : base() {
			this.setupRules = SetupRules.SetupTile;
			this.CreateTextures();
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Door];
			this.tileId = (byte) TileEnum.Door;
			this.title = "Door";
			this.description = "Allows passage to another room or door.";
			this.moveParamSet =  Params.ParamMap["Door"];
		}

		public void SetupTile(RoomScene room, short gridX, short gridY) {
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);
			room.roomExits.AddExit(this.tileId, subType, gridX, gridY);

			// Place a Detector beneath the flag:
			room.tilemap.SetMainTile(gridX, (short)(gridY + 1), (byte)TileEnum.DetectDoor, 0);
		}

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {

			// Only Characters interact with doors.
			if(actor is Character == false) { return false; }

			Character character = (Character) actor;

			// Display "Interaction" Prompt for Character (draws the 'interaction' hand icon above character head)
			//if(Systems.timer.frame16Modulus % 8 == 5) {
			//	StationaryParticle.SetParticle(character.room, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], "Prompt/Hand", new Vector2(gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight - (byte)TilemapEnum.HalfHeight), Systems.timer.Frame + 8);
			//}

			// Make sure the character is overlapping the inner door.
			if(!CollideRect.IsTouchingRect(character, gridX * (byte)TilemapEnum.TileWidth + 16, gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.TileWidth - 12, gridY * (byte)TilemapEnum.TileHeight, gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight * 2)) {
				return false;
			}

			// Make sure the Character is actually attempting to interact with the door.
			if(!character.input.isPressed(IKey.YButton)) { return false; }

			return this.InteractWithDoor(character, room, gridX, gridY);
		}

		public virtual bool InteractWithDoor(Character character, RoomScene room, short gridX, short gridY) {

			// Identify the destination door.
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);
			(RoomScene destRoom, short gridX, short gridY) arrivalExit = Door.GetLinkingDoor(room, subType, gridX, gridY);

			// Make sure the matching door is valid.
			if(arrivalExit.gridX == 0 && arrivalExit.gridY == 0) {

				// No matching door exists. Can announce error.
				room.PlaySound(Systems.sounds.collectDisable, 0.4f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
				return false;
			}

			// Open Door
			room.PlaySound(Systems.sounds.door, 1f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);

			// Transport Character to Destination Door
			ActionMap.Transport.StartAction(character, arrivalExit.destRoom.roomID, arrivalExit.gridX * (byte)TilemapEnum.TileWidth + 6, arrivalExit.gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight * 2 - character.bounds.Bottom);

			// Unlock the arrival door (if applicable), since you came from within.
			Door.UnlockDoor(character, arrivalExit.destRoom, subType, arrivalExit.gridX, arrivalExit.gridY, false);

			return true;
		}

		public static bool UnlockDoor(Character character, RoomScene room, byte subTypeId, short gridX, short gridY, bool requiresKey = true) {

			// The door is locked. Must have a key, or prevent entry.
			if(requiresKey && !character.trailKeys.HasKey) { return false; }

			// Check if the tile is locked:
			byte[] tileData = room.tilemap.GetTileDataAtGrid(gridX, gridY);
			if(tileData[0] != (byte)TileEnum.DoorLock) { return false; }

			// Remove the Key, Unlock the Door.
			if(requiresKey) { character.trailKeys.RemoveKey(); }
			room.PlaySound(Systems.sounds.unlock, 1f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);

			// Change Door to Unlocked
			room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.Door, subTypeId);

			return true;
		}

		public static (RoomScene room, short gridX, short gridY) GetLinkingDoor(RoomScene room, byte subType, short gridX, short gridY) {

			// Character is attempting to open an unlocked door.
			Dictionary<string, short> paramList = room.tilemap.GetParamList(gridX, gridY);

			byte destRoomId = paramList.ContainsKey("room") ? (byte)paramList["room"] : (byte) 0; // Defaults to Room #1 (which is ID 0)
			byte exitType = paramList.ContainsKey("exit") ? (byte)paramList["exit"] : (byte)DoorExitType.ToSameColor;

			byte toTileId = (byte)TileEnum.Door;
			byte toSubType = (byte)DoorSubType.Open;

			// Make sure the destination room exists. Otherwise, return as a failure.
			if(destRoomId != room.roomID && room.scene.rooms.Length < destRoomId + 1) { return (room, 0, 0); }

			// Determine SubType of Door Connection
			if(exitType == (byte)DoorExitType.ToSameColor) {
				toSubType = subType;
			} else if(exitType == (byte)DoorExitType.ToRedCheckpoint) {
				toTileId = (byte)TileEnum.CheckFlagCheckpoint;
				toSubType = 0;
			} else if(exitType == (byte)DoorExitType.ToWhiteCheckpoint) {
				toTileId = (byte)TileEnum.CheckFlagPass;
				toSubType = 0;
			}

			// Track down the matching door. Scan for both locked and unlocked doors, and take the upper-left most.
			(short gridX, short gridY) myExit = Door.FindExitType(room.scene.rooms[destRoomId], room.roomID, toTileId, (byte)(exitType == (byte)DoorExitType.ToSameColor ? (byte)TileEnum.DoorLock : 0), toSubType, gridX, gridY);
			
			return (room.scene.rooms[destRoomId], myExit.gridX, myExit.gridY);
		}

		public static (short gridX, short gridY) FindExitType(RoomScene destRoom, byte fromRoomId, byte toTileId, byte altTileId, byte toSubTypeId, short fromGridX, short fromGridY) {
			var exits = destRoom.roomExits.exits;
			(short gridX, short gridY) result = (0, 0);

			foreach(var exit in exits) {

				// Check if the grid has the same tileId and subType.
				if(exit.subTypeId != toSubTypeId || (exit.tileId != toTileId && exit.tileId != altTileId)) { continue; }

				// Skip the origin door. We only want to find any matching doors.
				if(exit.gridX == fromGridX && exit.gridY == fromGridY && destRoom.roomID == fromRoomId) { continue; }
				
				if(exit.gridY > result.gridY) { result = (exit.gridX, exit.gridY); }
				else if(exit.gridY == result.gridY && exit.gridX > result.gridX) { result = (exit.gridX, exit.gridY); }
			}

			return result;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}

		protected virtual void CreateTextures() {
			this.Texture = new string[5];
			this.Texture[(byte)DoorSubType.Open] = "Door/Open";
			this.Texture[(byte)DoorSubType.Blue] = "Door/Blue";
			this.Texture[(byte)DoorSubType.Green] = "Door/Green";
			this.Texture[(byte)DoorSubType.Red] = "Door/Red";
			this.Texture[(byte)DoorSubType.Yellow] = "Door/Yellow";
		}
	}
}
