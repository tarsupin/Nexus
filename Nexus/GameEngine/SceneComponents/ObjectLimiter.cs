using Nexus.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using static Nexus.Objects.Door;

namespace Nexus.Engine {

	public class TileLimits {

		// Doors
		public byte DoorBlue = 0;
		public byte DoorRed = 0;
		public byte DoorGreen = 0;
		public byte DoorYellow = 0;
		public byte DoorOpen = 0;

		// Checkpoints
		public byte CheckpointRed = 0;
		public byte CheckpointPass = 0;
		public byte CheckpointRetry = 0;
		public byte CheckpointFinish = 0;

		// Other
		public byte Generators = 0;		// Placers, Cannons, Fire Chompers
	}

	// Add an ObjectLimiter class to the Editor. It will track / disallow certain items based on number in the room, level, or other rules.
	public class ObjectLimiter {

		// Maximum Limits
		public const short MaxObjectsPerLevel = 800;        // Total Items + Enemies + Platforms (Per Level)
		public const short MaxItemsPerLevel = 300;
		public const short MaxEnemiesPerLevel = 600;
		public const short MaxPlatformsPerLevel = 600;

		public const short MaxObjectsPerRoom = 500;         // Total Items + Enemies + Platforms (Per Room)
		public const short MaxItemsPerRoom = 200;
		public const short MaxEnemiesPerRoom = 350;
		public const short MaxPlatformsPerRoom = 350;

		public const short MaxCharacters = 16;

		// Static Trackers
		public static string LastFailMessage = "";

		// Object Count: <roomID, item counts>
		// Item Counts: [0] # of objects total, [1] # of Items, [2] # of Enemies, [3] # of Platforms
		public Dictionary<byte, short[]> roomObjects;
		public Dictionary<byte, TileLimits> roomTiles;
		public short charCount;

		public ObjectLimiter() {
			this.charCount = 0;

			this.roomObjects = new Dictionary<byte, short[]>() {
				{  0, new short[4] },
				{  1, new short[4] },
				{  2, new short[4] },
				{  3, new short[4] },
				{  4, new short[4] },
				{  5, new short[4] },
			};

			this.roomTiles = new Dictionary<byte, TileLimits>() {
				{  0, new TileLimits() },
				{  1, new TileLimits() },
				{  2, new TileLimits() },
				{  3, new TileLimits() },
				{  4, new TileLimits() },
				{  5, new TileLimits() },
			};
		}

		public int CountObjectsInLevel { get { return this.roomObjects[0][0] + this.roomObjects[1][0] + this.roomObjects[2][0] + this.roomObjects[3][0] + this.roomObjects[4][0] + this.roomObjects[5][0]; } }
		public int CountItemsInLevel { get { return this.roomObjects[0][1] + this.roomObjects[1][1] + this.roomObjects[2][1] + this.roomObjects[3][1] + this.roomObjects[4][1] + this.roomObjects[5][1]; } }
		public int CountEnemiesInLevel { get { return this.roomObjects[0][2] + this.roomObjects[1][2] + this.roomObjects[2][2] + this.roomObjects[3][2] + this.roomObjects[4][2] + this.roomObjects[5][2]; } }
		public int CountPlatformsInLevel { get { return this.roomObjects[0][3] + this.roomObjects[1][3] + this.roomObjects[2][3] + this.roomObjects[3][3] + this.roomObjects[4][3] + this.roomObjects[5][3]; } }

		public short CountObjectsInRoom(byte roomID) { return this.roomObjects.ContainsKey(roomID) ? this.roomObjects[roomID][0] : (short) 0; }
		public short CountItemsInRoom(byte roomID) { return this.roomObjects.ContainsKey(roomID) ? this.roomObjects[roomID][1] : (short) 0; }
		public short CountEnemiesInRoom(byte roomID) { return this.roomObjects.ContainsKey(roomID) ? this.roomObjects[roomID][2] : (short) 0; }
		public short CountPlatformsInRoom(byte roomID) { return this.roomObjects.ContainsKey(roomID) ? this.roomObjects[roomID][3] : (short) 0; }

		public void RunRoomCount(byte roomID, RoomFormat room) {
			if(roomID > 5) { return; }

			// Scan Through Objects Layer
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in room.obj) {
				short gridY = short.Parse(yData.Key);

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					byte objectID = byte.Parse(xData.Value[0].ToString());

					// Add the object to the list, if applicable.
					this.AddLimitObject(roomID, objectID, true);
				}
			}

			// Scan Through Main Tile Layer
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in room.main) {
				short gridY = short.Parse(yData.Key);

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					byte objectID = byte.Parse(xData.Value[0].ToString());
					byte subType = byte.Parse(xData.Value[1].ToString());

					// Add the object to the list, if applicable.
					this.AddLimitTile(roomID, objectID, subType);
				}
			}
		}

		public bool AddLimitTile(byte roomID, byte tileID, byte subType) {
			if(roomID > 5) { return false; }
			TileLimits tileLimits = this.roomTiles[roomID];
			
			// Doors
			if(tileID == (byte)TileEnum.Door || tileID == (byte)TileEnum.DoorLock) {

				// Blue Doors
				if(subType == (byte)DoorSubType.Blue) {
					if(tileLimits.DoorBlue < 2) { tileLimits.DoorBlue++; return true; }
					ObjectLimiter.LastFailMessage = "Have reached the limit of Blue Doors for this room (2).";
					return false;
				}

				// Red Doors
				else if(subType == (byte)DoorSubType.Red) {
					if(tileLimits.DoorRed < 2) { tileLimits.DoorRed++; return true; }
					ObjectLimiter.LastFailMessage = "Have reached the limit of Red Doors for this room (2).";
					return false;
				}

				// Green Doors
				else if(subType == (byte)DoorSubType.Green) {
					if(tileLimits.DoorGreen < 2) { tileLimits.DoorGreen++; return true; }
					ObjectLimiter.LastFailMessage = "Have reached the limit of Green Doors for this room (2).";
					return false;
				}

				// Yellow Doors
				else if(subType == (byte)DoorSubType.Yellow) {
					if(tileLimits.DoorYellow < 2) { tileLimits.DoorYellow++; return true; }
					ObjectLimiter.LastFailMessage = "Have reached the limit of Yellow Doors for this room (2).";
					return false;
				}

				// Open Doors
				else if(subType == (byte)DoorSubType.Open) {
					if(tileLimits.DoorOpen < 1) { tileLimits.DoorOpen++; return true; }
					ObjectLimiter.LastFailMessage = "Have reached the limit of Open Doors for this room (1).";
					return false;
				}
			}

			// Object Generation Tiles (Cannons, Placers, Fire Chompers)
			else if(tileID == (byte)TileEnum.CannonDiagonal || tileID == (byte)TileEnum.CannonHorizontal || tileID == (byte)TileEnum.CannonVertical || tileID == (byte)TileEnum.Placer || tileID == (byte)TileEnum.ChomperFire) {
				if(tileLimits.Generators < 80) { tileLimits.Generators++; return true; }
				ObjectLimiter.LastFailMessage = "Have reached the limit of Object Generation Tiles for this room (80).";
				return false;
			}

			// Checkpoints (Red)
			else if(tileID == (byte)TileEnum.CheckFlagCheckpoint) {
				if(tileLimits.CheckpointRed < 3) { tileLimits.CheckpointRed++; return true; }
				ObjectLimiter.LastFailMessage = "Have reached the limit of Red Checkpoints for this room (3).";
				return false;
			}

			// Checkpoints (White, Pass)
			else if(tileID == (byte)TileEnum.CheckFlagPass) {
				if(tileLimits.CheckpointPass < 3) { tileLimits.CheckpointPass++; return true; }
				ObjectLimiter.LastFailMessage = "Have reached the limit of White Checkpoints for this room (3).";
				return false;
			}

			// Checkpoints (Blue, Retry)
			else if(tileID == (byte)TileEnum.CheckFlagRetry) {
				if(tileLimits.CheckpointRetry < 3) { tileLimits.CheckpointRetry++; return true; }
				ObjectLimiter.LastFailMessage = "Have reached the limit of Retry Flags for this room (3).";
				return false;
			}

			// Checkpoints (Finish)
			else if(tileID == (byte)TileEnum.CheckFlagFinish) {
				if(tileLimits.CheckpointFinish < 3) { tileLimits.CheckpointFinish++; return true; }
				ObjectLimiter.LastFailMessage = "Have reached the limit of Finish Flags for this room (3).";
				return false;
			}

			return true;
		}

		public bool RemoveLimitTile(byte roomID, byte tileID, byte subType) {
			if(roomID > 5) { return false; }
			TileLimits tileLimits = this.roomTiles[roomID];
			
			// Doors
			if(tileID == (byte)TileEnum.Door || tileID == (byte)TileEnum.DoorLock) {

				// Blue Doors
				if(subType == (byte)DoorSubType.Blue) {
					if(tileLimits.DoorBlue > 0) { tileLimits.DoorBlue--; return true; }
					return false;
				}

				// Red Doors
				else if(subType == (byte)DoorSubType.Red) {
					if(tileLimits.DoorRed > 0) { tileLimits.DoorRed--; return true; }
					return false;
				}

				// Green Doors
				else if(subType == (byte)DoorSubType.Green) {
					if(tileLimits.DoorGreen > 0) { tileLimits.DoorGreen--; return true; }
					return false;
				}

				// Yellow Doors
				else if(subType == (byte)DoorSubType.Yellow) {
					if(tileLimits.DoorYellow > 0) { tileLimits.DoorYellow--; return true; }
					return false;
				}

				// Open Doors
				else if(subType == (byte)DoorSubType.Open) {
					if(tileLimits.DoorOpen > 0) { tileLimits.DoorOpen--; return true; }
					return false;
				}
			}

			// Object Generation Tiles (Cannons, Placers, Fire Chompers)
			else if(tileID == (byte)TileEnum.CannonDiagonal || tileID == (byte)TileEnum.CannonHorizontal || tileID == (byte)TileEnum.CannonVertical || tileID == (byte)TileEnum.Placer || tileID == (byte)TileEnum.ChomperFire) {
				if(tileLimits.Generators > 0) { tileLimits.Generators--; return true; }
				return false;
			}

			// Checkpoints (Red)
			else if(tileID == (byte)TileEnum.CheckFlagCheckpoint) {
				if(tileLimits.CheckpointRed > 0) { tileLimits.CheckpointRed--; return true; }
				return false;
			}

			// Checkpoints (White, Pass)
			else if(tileID == (byte)TileEnum.CheckFlagPass) {
				if(tileLimits.CheckpointPass > 0) { tileLimits.CheckpointPass--; return true; }
				return false;
			}

			// Checkpoints (Blue, Retry)
			else if(tileID == (byte)TileEnum.CheckFlagRetry) {
				if(tileLimits.CheckpointRetry > 0) { tileLimits.CheckpointRetry--; return true; }
				return false;
			}

			// Checkpoints (Finish)
			else if(tileID == (byte)TileEnum.CheckFlagFinish) {
				if(tileLimits.CheckpointFinish > 0) { tileLimits.CheckpointFinish--; return true; }
				return false;
			}

			return true;
		}

		private string GetClassNameFromObjectID(byte objectID) {

			Type classType;
			bool hasType = Systems.mapper.ObjectTypeDict.TryGetValue(objectID, out classType);
			if(!hasType || classType == null) { return ""; }

			for(byte i = 0; i < 7; i++) {
				if(classType.BaseType.Name == "GameObject") { return classType.Name; }
				classType = classType.BaseType;
			}

			return "GameObject";
		}

		// Returns TRUE if you can add an object to the room.
		private bool CanAddLimitObjectToRoom(byte roomID, byte objectID) {
			if(roomID > 5) { return false; }

			string objName = this.GetClassNameFromObjectID(objectID);
			short[] roomCounter = this.roomObjects[roomID];

			// Character
			if(objectID == (byte)ObjectEnum.Character) {

				// Can only place Character in the first room.
				if(roomID != 0) {
					ObjectLimiter.LastFailMessage = "Characters can only be placed in the first room.";
					return false;
				}

				if(this.charCount >= ObjectLimiter.MaxCharacters) {
					ObjectLimiter.LastFailMessage = "Reached maximum of " + ObjectLimiter.MaxCharacters + " Character Starting Positions.";
					return false;
				}
			}

			// Item Limits
			if(objName == "Item") {

				if(roomCounter[0] >= ObjectLimiter.MaxObjectsPerRoom) {
					ObjectLimiter.LastFailMessage = "Have reached the limit of " + ObjectLimiter.MaxObjectsPerRoom + " total objects per room.";
					return false;
				}

				if(roomCounter[1] >= ObjectLimiter.MaxItemsPerRoom) {
					ObjectLimiter.LastFailMessage = "Have reached the limit of " + ObjectLimiter.MaxItemsPerRoom + " items per room.";
					return false;
				}

				if(this.CountItemsInLevel >= ObjectLimiter.MaxItemsPerLevel) {
					ObjectLimiter.LastFailMessage = "Have reached the limit of " + ObjectLimiter.MaxItemsPerLevel + " items per level.";
					return false;
				}
			}

			// Enemy Limits
			else if(objName == "Enemy") {

				if(roomCounter[0] >= ObjectLimiter.MaxObjectsPerRoom) {
					ObjectLimiter.LastFailMessage = "Have reached the limit of " + ObjectLimiter.MaxObjectsPerRoom + " total objects per room.";
					return false;
				}

				if(roomCounter[2] >= ObjectLimiter.MaxEnemiesPerRoom) {
					ObjectLimiter.LastFailMessage = "Have reached the limit of " + ObjectLimiter.MaxEnemiesPerRoom + " enemies per room.";
					return false;
				}

				if(this.CountEnemiesInLevel >= ObjectLimiter.MaxEnemiesPerLevel) {
					ObjectLimiter.LastFailMessage = "Have reached the limit of " + ObjectLimiter.MaxEnemiesPerLevel + " enemies per level.";
					return false;
				}
			}

			// Platform Limits
			else if(objName == "Platform") {

				if(roomCounter[0] >= ObjectLimiter.MaxObjectsPerRoom) {
					ObjectLimiter.LastFailMessage = "Have reached the limit of " + ObjectLimiter.MaxObjectsPerRoom + " total objects per room.";
					return false;
				}

				if(roomCounter[2] >= ObjectLimiter.MaxPlatformsPerRoom) {
					ObjectLimiter.LastFailMessage = "Have reached the limit of " + ObjectLimiter.MaxPlatformsPerRoom + " platforms per room.";
					return false;
				}

				if(this.CountPlatformsInLevel >= ObjectLimiter.MaxPlatformsPerLevel) {
					ObjectLimiter.LastFailMessage = "Have reached the limit of " + ObjectLimiter.MaxPlatformsPerLevel + " platforms per level.";
					return false;
				}
			}

			else {
				return true;
			}

			// Total Object Limits
			if(this.CountObjectsInLevel >= ObjectLimiter.MaxObjectsPerLevel) {
				ObjectLimiter.LastFailMessage = "Have reached the limit of " + ObjectLimiter.MaxObjectsPerLevel + " total objects per level.";
				return false;
			}

			return true;
		}

		// Attempt to add an object to the limiter.
		public bool AddLimitObject(byte roomID, byte objectID, bool bypass = false) {
			if(!bypass && !this.CanAddLimitObjectToRoom(roomID, objectID)) { return false; }
			
			if(objectID == (byte)ObjectEnum.Character) {
				this.charCount++;
				return true;
			}

			string objName = this.GetClassNameFromObjectID(objectID);

			if(objName == "Item") {
				this.roomObjects[roomID][0]++;
				this.roomObjects[roomID][1]++;
			}

			else if(objName == "Enemy") {
				this.roomObjects[roomID][0]++;
				this.roomObjects[roomID][2]++;
			}

			else if(objName == "Platform") {
				this.roomObjects[roomID][0]++;
				this.roomObjects[roomID][2]++;
			}

			return true;
		}

		public void RemoveObject(byte roomID, byte objectID) {
			if(roomID > 5) { return; }

			if(objectID == (byte)ObjectEnum.Character) {
				this.charCount--;
				return;
			}

			string objName = this.GetClassNameFromObjectID(objectID);

			short[] roomCounter = this.roomObjects[roomID];

			// Make sure there are sufficient OBJECTS (ANY TYPE) in the room so it doesn't break the result.
			if(roomCounter[0] < 1) {
				#if debug
					throw new Exception("Inaccurate count. There should be 0 objects in the room.");
				#endif
				return;
			}

			if(objName == "Item") {
				
				// Make sure there are sufficient ITEMS in the room so it doesn't break the result.
				if(roomCounter[1] < 1) {
					#if debug
						throw new Exception("Inaccurate count. There should be 0 items in the room.");
					#endif
					return;
				}

				roomCounter[0]--;
				roomCounter[1]--;
			}

			else if(objName == "Enemy") {
				
				// Make sure there are sufficient ENEMIES in the room so it doesn't break the result.
				if(roomCounter[2] < 1) {
					#if debug
						throw new Exception("Inaccurate count. There should be 0 enemies in the room.");
					#endif
					return;
				}

				roomCounter[0]--;
				roomCounter[2]--;
			}

			else if(objName == "Platform") {
				
				// Make sure there are sufficient PLATFORMS in the room so it doesn't break the result.
				if(roomCounter[3] < 1) {
					#if debug
						throw new Exception("Inaccurate count. There should be 0 platforms in the room.");
					#endif
					return;
				}

				roomCounter[0]--;
				roomCounter[3]--;
			}
		}
	}
}
