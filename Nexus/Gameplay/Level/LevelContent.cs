﻿using Newtonsoft.Json;
using Nexus.Engine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Nexus.Gameplay {

	public class LevelContent {

		// Level Data
		public string levelId;          // Level ID (e.g. "QCALQOD16")
		public LevelFormat data;        // Level Data

		// Path
		public string levelPath;

		public LevelContent(string levelPath = null, string levelId = null) {
			this.SetLevelPath(levelPath);

			// Attempt to load Level Data
			if(levelId != null) {
				this.LoadLevelData(levelId);
			}
		}

		public void SetLevelPath(string levelPath = null) {

			// If a level directory is not provided, use the default local directory.
			if(levelPath == null) {
				Systems.filesLocal.MakeDirectory("Levels"); // Make sure the Levels directory exists.
				this.levelPath = Path.Combine(Systems.filesLocal.localDir, "Levels");
			}

			// Make sure the level directory provided exists.
			else {
				this.levelPath = Directory.Exists(levelPath) ? levelPath : null;
			}
		}

		public static string GetLocalLevelPath(string levelId) {
			return Path.Combine(levelId.Substring(0, 2), levelId + ".json");
		}

		public string GetFullLevelPath(string levelId) {
			return Path.Combine(this.levelPath, LevelContent.GetLocalLevelPath(levelId));
		}

		public string GetFullDestinationPath(string destPath, string levelId) {
			return Path.Combine(destPath, LevelContent.GetLocalLevelPath(levelId));
		}

		public bool LoadLevelData(string levelId = "") {

			// Update the Level ID, or use existing Level ID if applicable.
			if(levelId.Length > 0) { this.levelId = levelId; } else { return false; }

			string fullLevelPath = this.GetFullLevelPath(this.levelId);
			
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

		private LevelFormat BuildLevelStruct() {
			LevelFormat levelStructure = new LevelFormat();

			levelStructure.id = levelId;
			levelStructure.rooms["0"] = new RoomFormat();
			//levelStructure.room["0"].bgLayer;
			levelStructure.rooms["0"].main["16"]["0"].Add(1);
			levelStructure.rooms["0"].main["16"]["0"].Add(1);
			levelStructure.rooms["0"].main["16"]["1"].Add(1);
			levelStructure.rooms["0"].main["16"]["1"].Add(1);
			//levelStructure.room["0"].cosmeticLayer;

			return levelStructure;
		}

		public void SaveLevel( string destDir = null, string destLevelId = null ) {

			// Determine the Destination Path and Destination Level ID
			if(destDir == null) { destDir = this.levelPath; }
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

		public static byte[] GetTileData(Dictionary<string, Dictionary<string, ArrayList>> layerData, ushort gridX, ushort gridY) {
			if(!layerData.ContainsKey(gridY.ToString())) { return null; }
			if(!layerData[gridY.ToString()].ContainsKey(gridX.ToString())) { return null; }
			var tileList = layerData[gridY.ToString()][gridX.ToString()];
			return new byte[] { byte.Parse(tileList[0].ToString()), byte.Parse(tileList[1].ToString()) };
		}

		public void SetTile(string roomID, LayerEnum layer, ushort gridX, ushort gridY, byte tileId, byte subType, Dictionary<string, object> paramList = null) {

			// Check Tiles with special requirements (such as being restricted to one):
			//if(type == ObjectEnum.Character) {
			//	let tileLoc = Tile.scanLayerForTile(roomData, "mainLayer", "Character/Ryu");

			//	// Delete the existing version:
			//	if(tileLoc) { this.deleteTileLayer(roomData, "mainLayer", tileLoc.x, tileLoc.y); }

			//	// Update the character's starting point:
			//	roomData.charStart = {
			//	x: gridX * this.tilemap.tileWidth,
			//		y: gridY * this.tilemap.tileHeight
			//	};
			//}

			// Place the Tile
			if(layer == LayerEnum.main) {
				this.data.rooms[roomID].main[gridY.ToString()][gridX.ToString()] = new ArrayList() { tileId, subType };
			} else if(layer == LayerEnum.bg) {
				this.data.rooms[roomID].bg[gridY.ToString()][gridX.ToString()] = new ArrayList() { tileId, subType };
			} else if(layer == LayerEnum.fg) {
				this.data.rooms[roomID].fg[gridY.ToString()][gridX.ToString()] = new ArrayList() { tileId, subType };
			} else if(layer == LayerEnum.obj) {
				this.data.rooms[roomID].obj[gridY.ToString()][gridX.ToString()] = new ArrayList() { tileId, subType };
			}

			// TODO: Handle Params when Adding Tiles and Objects
			//if(params && typeof(params) === "object") {
			//	for(let p in params ) {
			//		if(p === "id") { continue; }
			//		yData[gridX][p] = params[p];
			//	}
			//}
		}

		public void DeleteTile(string roomID, byte gridX, byte gridY) {
			this.data.rooms[roomID].main[gridY.ToString()][gridX.ToString()] = null;
		}
	}
}
