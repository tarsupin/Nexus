using Nexus.Config;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
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
			this.CreateTextures();
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Door];
			this.tileId = (byte) TileEnum.Door;
			this.title = "Door";
			this.description = "Allows passage to another room or door.";
			this.moveParamSet =  Params.ParamMap["Door"];
		}

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {

			// Only Characters interact with doors.
			if(actor is Character == false) { return false; }

			Character character = (Character)actor;

			// Make sure the character is overlapping the inner door.
			if(!CollideRect.IsTouchingRect(character, gridX * (byte)TilemapEnum.TileWidth + 6, gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.TileWidth - 6, gridY * (byte)TilemapEnum.TileHeight, gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight)) {
				return false;
			}
			DebugConfig.AddDebugNote("touching door");
			// Display "Interaction" Prompt for Character (draws the 'interaction' hand icon above character head)
			// TODO: Apply attachment. Local only. Maybe a particle effect? Lasts one frame only? Or multiple, and follows character? Follows Local MyCharacter.

			// Make sure the Character is interacting with the door.
			if(!character.input.isPressed(IKey.YButton)) { return false; }

			Dictionary<string, short> paramList = room.tilemap.GetParamList(gridX, gridY);

			byte destRoom = paramList.ContainsKey("room") ? (byte) paramList["room"] : room.roomID;
			byte exitType = paramList.ContainsKey("exit") ? (byte) paramList["exit"] : (byte) DoorExitType.ToSameColor;

			byte toTileId = (byte) TileEnum.Door;
			byte toSubType = (byte) DoorSubType.Open;

			// Determine SubType of Door Connection
			if(exitType == (byte) DoorExitType.ToSameColor) {
				toSubType = room.tilemap.GetMainSubType(gridX, gridY);
			}
			
			else if(exitType == (byte) DoorExitType.ToRedCheckpoint) {
				toTileId = (byte)TileEnum.CheckFlagCheckpoint;
				toSubType = 0;
			}

			else if(exitType == (byte) DoorExitType.ToWhiteCheckpoint) {
				toTileId = (byte)TileEnum.CheckFlagPass;
				toSubType = 0;
			}

			// Track down the matching door:
			(short gridX, short gridY) doorMatch;

			if(destRoom == room.roomID) {
				doorMatch = Door.FindDoor(room, toTileId, toSubType, gridX, gridY);
			} else {
				RoomScene toRoom = room.scene.rooms[destRoom];
				doorMatch = Door.FindDoor(toRoom, toTileId, toSubType, gridX, gridY);
			}

			// Make sure the matching door is valid.
			if(doorMatch.gridX == 0 && doorMatch.gridY == 0) { return false; }

			System.Console.WriteLine(doorMatch.gridX + ", " + doorMatch.gridY);

			return true;
		}

		public static (short gridX, short gridY) FindDoor(RoomScene room, byte tileId, byte subType, short gridX, short gridY) {

			// Search the entire room for the door to track down.
			var scans = room.tilemap.ScanMainTilesForSubType(tileId, subType, 0, 0, room.tilemap.XCount, room.tilemap.YCount);
			
			// Find the first result that isn't this grid.
			foreach(var scan in scans) {
				if(scan.gridX == gridX && scan.gridY == gridY) { continue; }
				return scan;
			}

			return (0, 0);
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
