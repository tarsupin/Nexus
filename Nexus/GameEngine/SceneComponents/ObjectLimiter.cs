using System;
using System.Collections.Generic;

namespace Nexus.Engine {

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

		public enum SpecialRule : byte {
			None = 0,
			OnlyInFirstRoom = 1,
			Doors = 2,
		}

		// Static Trackers
		public static string LastFailMessage = "";

		// Object Count: <roomID, item counts>
		// Item Counts: [0] # of objects total, [1] # of Items, [2] # of Enemies, [3] # of Platforms
		public Dictionary<byte, short[]> roomCounts;

		public ObjectLimiter() {
			this.roomCounts = new Dictionary<byte, short[]>() {
				{  0, new short[4] { 0, 0, 0, 0 } },
				{  1, new short[4] { 0, 0, 0, 0 } },
				{  2, new short[4] { 0, 0, 0, 0 } },
				{  3, new short[4] { 0, 0, 0, 0 } },
				{  4, new short[4] { 0, 0, 0, 0 } },
				{  5, new short[4] { 0, 0, 0, 0 } },
				{  6, new short[4] { 0, 0, 0, 0 } },
				{  7, new short[4] { 0, 0, 0, 0 } },
			};
		}

		public int CountObjectsInLevel { get { return this.roomCounts[0][0] + this.roomCounts[1][0] + this.roomCounts[2][0] + this.roomCounts[3][0] + this.roomCounts[4][0] + this.roomCounts[5][0] + this.roomCounts[6][0] + this.roomCounts[7][0]; } }
		public int CountItemsInLevel { get { return this.roomCounts[0][1] + this.roomCounts[1][1] + this.roomCounts[2][1] + this.roomCounts[3][1] + this.roomCounts[4][1] + this.roomCounts[5][1] + this.roomCounts[6][1] + this.roomCounts[7][1]; } }
		public int CountEnemiesInLevel { get { return this.roomCounts[0][2] + this.roomCounts[1][2] + this.roomCounts[2][2] + this.roomCounts[3][2] + this.roomCounts[4][2] + this.roomCounts[5][2] + this.roomCounts[6][2] + this.roomCounts[7][2]; } }
		public int CountPlatformsInLevel { get { return this.roomCounts[0][3] + this.roomCounts[1][3] + this.roomCounts[2][3] + this.roomCounts[3][3] + this.roomCounts[4][3] + this.roomCounts[5][3] + this.roomCounts[6][3] + this.roomCounts[7][3]; } }

		public short CountObjectsInRoom(byte roomID) { return this.roomCounts.ContainsKey(roomID) ? this.roomCounts[roomID][0] : (short) 0; }
		public short CountItemsInRoom(byte roomID) { return this.roomCounts.ContainsKey(roomID) ? this.roomCounts[roomID][1] : (short) 0; }
		public short CountEnemiesInRoom(byte roomID) { return this.roomCounts.ContainsKey(roomID) ? this.roomCounts[roomID][2] : (short) 0; }
		public short CountPlatformsInRoom(byte roomID) { return this.roomCounts.ContainsKey(roomID) ? this.roomCounts[roomID][3] : (short) 0; }

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
		private bool CanAddObjectToRoom(byte roomID, byte objectID) {
			if(roomID > 7) { return false; }

			string objName = this.GetClassNameFromObjectID(objectID);
			short[] roomCounter = this.roomCounts[roomID];

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

				if(this.CountItemsInLevel > ObjectLimiter.MaxItemsPerLevel) {
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

				if(this.CountEnemiesInLevel > ObjectLimiter.MaxEnemiesPerLevel) {
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

				if(this.CountPlatformsInLevel > ObjectLimiter.MaxPlatformsPerLevel) {
					ObjectLimiter.LastFailMessage = "Have reached the limit of " + ObjectLimiter.MaxPlatformsPerLevel + " platforms per level.";
					return false;
				}
			}

			else {
				return true;
			}

			// Total Object Limits
			if(this.CountObjectsInLevel > ObjectLimiter.MaxObjectsPerLevel) {
				ObjectLimiter.LastFailMessage = "Have reached the limit of " + ObjectLimiter.MaxObjectsPerLevel + " total objects per level.";
				return false;
			}

			return true;
		}

		// Attempt to add an object to the limiter.
		public bool AddObject(byte roomID, byte objectID, bool bypass = false) {
			if(!bypass && !this.CanAddObjectToRoom(roomID, objectID)) { return false; }

			string objName = this.GetClassNameFromObjectID(objectID);

			if(objName == "Item") {
				this.roomCounts[roomID][0]++;
				this.roomCounts[roomID][1]++;
			}

			else if(objName == "Enemy") {
				this.roomCounts[roomID][0]++;
				this.roomCounts[roomID][2]++;
			}

			else if(objName == "Platform") {
				this.roomCounts[roomID][0]++;
				this.roomCounts[roomID][2]++;
			}

			return true;
		}

		public void RemoveObject(byte roomID, byte objectID) {
			if(roomID > 7) { return; }

			string objName = this.GetClassNameFromObjectID(objectID);

			short[] roomCounter = this.roomCounts[roomID];

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
