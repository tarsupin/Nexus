using Newtonsoft.Json;
using Nexus.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Nexus.Gameplay {

	public class LevelContent {

		// Level Data
		public string levelId;          // Level ID (e.g. "QCALQOD16")
		public LevelFormat data;        // Level Data

		// Path
		public static string levelPath;

		public LevelContent(string levelPath = null, string levelId = null) {
			LevelContent.SetLevelPath(levelPath);

			// Attempt to load Level Data
			if(levelId != null) {
				this.LoadLevelData(levelId);
			}
		}

		// Verify that the level exists in the level directory.
		public static bool LevelExists(string levelId) {
			string fullLevelPath = LevelContent.GetFullLevelPath(levelId);
			return File.Exists(fullLevelPath);
		}

		public static void SetLevelPath(string levelPath = null) {

			// If a level directory is not provided, use the default local directory.
			if(levelPath == null) {
				Systems.filesLocal.MakeDirectory("Levels"); // Make sure the Levels directory exists.
				LevelContent.levelPath = Path.Combine(Systems.filesLocal.localDir, "Levels");
			}

			// Make sure the level directory provided exists.
			else {
				LevelContent.levelPath = Directory.Exists(levelPath) ? levelPath : null;
			}
		}

		public static string GetLocalLevelPath(string levelId) {
			return Path.Combine(levelId.Substring(0, 2), levelId + ".json");
		}

		public static string GetFullLevelPath(string levelId) {
			return Path.Combine(LevelContent.levelPath, LevelContent.GetLocalLevelPath(levelId));
		}

		public string GetFullDestinationPath(string destPath, string levelId) {
			return Path.Combine(destPath, LevelContent.GetLocalLevelPath(levelId));
		}

		public bool LoadLevelData(string levelId = "") {

			// Update the Level ID, or use existing Level ID if applicable.
			if(levelId.Length > 0) { this.levelId = levelId; } else { return false; }

			string fullLevelPath = LevelContent.GetFullLevelPath(this.levelId);
			
			// Make sure the level exists:
			if(!File.Exists(fullLevelPath)) { return false; }

			string json = File.ReadAllText(fullLevelPath);

			// If there is no JSON content, end the attempt to load level:
			if(json == "") { return false; }

			// Load the Data
			this.data = JsonConvert.DeserializeObject<LevelFormat>(json);

			return true;
		}

		public bool LoadLevelData(LevelFormat levelData) {
			this.data = levelData;
			this.levelId = levelData.id;
			return true;
		}

		public bool BuildRoomData(byte roomID) {
			string roomStr = roomID.ToString();

			// Build Initial Room Format
			if(!this.data.rooms.ContainsKey(roomStr)) {
				this.data.rooms[roomStr] = new RoomFormat();
			}

			// Main Layer
			if(this.data.rooms[roomStr].main == null) {
				this.data.rooms[roomStr].main = new Dictionary<string, Dictionary<string, ArrayList>>();
			}
			
			// BG Layer
			if(this.data.rooms[roomStr].bg == null) {
				this.data.rooms[roomStr].bg = new Dictionary<string, Dictionary<string, ArrayList>>();
			}
			
			// FG Layer
			if(this.data.rooms[roomStr].fg == null) {
				this.data.rooms[roomStr].fg = new Dictionary<string, Dictionary<string, ArrayList>>();
			}
			
			// Obj Layer
			if(this.data.rooms[roomStr].obj == null) {
				this.data.rooms[roomStr].obj = new Dictionary<string, Dictionary<string, ArrayList>>();
			}

			return true;
		}

		// Assign Level Data
		public void SetAccount( string account ) { this.data.account = account; }
		public void SetTitle( string title ) { this.data.title = title; }
		public void SetDescription( string description ) { this.data.description = description; }
		public void SetTimeLimit( short timeLimit ) { this.data.timeLimit = Math.Max((short) 10, timeLimit); }
		public void SetGameClass( GameClassFlag gameClass ) { this.data.gameClass = (short) gameClass; }
		public void SetMusicTrack( MusicTrackEnum track ) { this.data.music = (short) track; }

		public void SaveLevel( string destDir = null, string destLevelId = null ) {

			// Determine the Destination Path and Destination Level ID
			if(destDir == null) { destDir = LevelContent.levelPath; }
			if(destLevelId == null) { destLevelId = this.levelId; }

			// Make sure the directory exists:
			if(!Directory.Exists(destDir)) { Directory.CreateDirectory(destDir); }

			// Can only save a level state if the level ID is assigned correctly.
			if(destLevelId == null || destLevelId.Length == 0) { return; }

			// TODO LOW PRIORITY: Verify that the levelId exists; not just that an ID is present.

			// Save State
			string fullDestPath = this.GetFullDestinationPath(destDir, this.levelId);
			string json = JsonConvert.SerializeObject(this.data);
			File.WriteAllText(fullDestPath, json);
		}

		public static Dictionary<string, Dictionary<string, ArrayList>> GetLayerData(RoomFormat roomData, LayerEnum layerEnum) {
			if(layerEnum == LayerEnum.main) { return roomData.main; }
			if(layerEnum == LayerEnum.obj) { return roomData.obj; }
			if(layerEnum == LayerEnum.bg) { return roomData.bg; }
			if(layerEnum == LayerEnum.fg) { return roomData.fg; }
			return null;
		}

		public static bool VerifyTiles(Dictionary<string, Dictionary<string, ArrayList>> layerData, short gridX, short gridY) {
			if(!layerData.ContainsKey(gridY.ToString())) { return false; }
			if(!layerData[gridY.ToString()].ContainsKey(gridX.ToString())) { return false; }
			return true;
		}

		public static byte[] GetTileData(Dictionary<string, Dictionary<string, ArrayList>> layerData, short gridX, short gridY) {
			if(!LevelContent.VerifyTiles(layerData, gridX, gridY)) { return null; }
			ArrayList tileList = layerData[gridY.ToString()][gridX.ToString()];
			return new byte[] { byte.Parse(tileList[0].ToString()), byte.Parse(tileList[1].ToString()) };
		}

		public static ArrayList GetTileDataWithParams(Dictionary<string, Dictionary<string, ArrayList>> layerData, short gridX, short gridY) {
			if(!LevelContent.VerifyTiles(layerData, gridX, gridY)) { return null; }
			ArrayList tileObj = layerData[gridY.ToString()][gridX.ToString()];

			// Convert the parameter list from JObject to Dictionary<string, short>
			if(tileObj.Count > 2) {

				// If the parameter list is already a Dictionary<string, short>, we don't need to convert it.
				// This can occur if we recently edited the data in the editor.
				if(tileObj[2] is Dictionary<string, short> == false) {
					tileObj[2] = JsonConvert.DeserializeObject<Dictionary<string, short>>(tileObj[2].ToString());
				}
			}

			return tileObj;
		}

		public void SetTile(Dictionary<string, Dictionary<string, ArrayList>> layerData, short gridX, short gridY, byte tileId, byte subType, Dictionary<string, short> paramList = null) {

			string xStr = gridX.ToString();
			string yStr = gridY.ToString();

			// Make sure the dictionaries exist:
			if(!layerData.ContainsKey(yStr)) {
				layerData[yStr] = new Dictionary<string, ArrayList>();
			}

			// Place the Tile
			layerData[yStr][xStr] = new ArrayList() { tileId, subType };

			// Handle Params when Adding Tiles and Objects
			if(paramList != null && paramList.Count > 0) {
				layerData[yStr][xStr].Add(paramList);
			}
		}

		public void DeleteTile(string roomID, short gridX, short gridY) {
			RoomFormat roomData = this.data.rooms[roomID];

			string strX = gridX.ToString();
			string strY = gridY.ToString();

			if(roomData.obj.ContainsKey(strY)) { roomData.obj[strY].Remove(strX); }
			if(roomData.main.ContainsKey(strY)) { roomData.main[strY].Remove(strX); }
			if(roomData.fg.ContainsKey(strY)) { roomData.fg[strY].Remove(strX); }
			if(roomData.bg.ContainsKey(strY)) { roomData.bg[strY].Remove(strX); }
		}

		public void DeleteTileOnLayer(LayerEnum layerEnum, string roomID, short gridX, short gridY) {
			RoomFormat roomData = this.data.rooms[roomID];

			string strX = gridX.ToString();
			string strY = gridY.ToString();

			if(layerEnum == LayerEnum.obj && roomData.obj.ContainsKey(strY)) { roomData.obj[strY].Remove(strX); }
			else if(layerEnum == LayerEnum.main && roomData.main.ContainsKey(strY)) { roomData.main[strY].Remove(strX); }
			else if(layerEnum == LayerEnum.fg && roomData.fg.ContainsKey(strY)) { roomData.fg[strY].Remove(strX); }
			else if(layerEnum == LayerEnum.bg && roomData.bg.ContainsKey(strY)) { roomData.bg[strY].Remove(strX); }
		}
	}
}
