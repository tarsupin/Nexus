using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nexus.Scripts {

	class LevelConvert {

		// Config
		readonly string originalFolder = "Levels-Orig";
		readonly string deliveryFolder = "Levels";

		// Level Directory
		readonly string basePath = "";
		readonly string originalPath = "";
		readonly string deliveryPath = "";

		// Conversion Tracking Values - Use these to know which values to change from the root json (curLevelJson)
		protected static string curRoomId;
		protected static string curRoomLayerId;
		protected static ushort curGridY;
		protected static ushort curGridX;

		// Level Data
		protected LevelContent levelContent;

		// LevelContent.GetLocalLevelPath		// this returns "QC/QCAL10" from "QCAL10"

		public LevelConvert() {

			// Set Paths
			this.basePath = Systems.filesLocal.localDir;
			this.originalPath = Path.Combine(basePath, originalFolder);
			this.deliveryPath = Path.Combine(basePath, deliveryFolder);

			this.levelContent = new LevelContent(this.originalPath);

			this.RunAllLevels();
		}

		protected void ProcessLevel( string levelId ) {
			System.Console.WriteLine("Processing Level ID: " + levelId);

			// Load the Level Content
			this.levelContent.LoadLevelData(levelId);

			// If the level content wasn't loaded correctly:
			if(this.levelContent.levelId != levelId) {
				throw new Exception("Level content was not loaded correctly.");
			}

			foreach(KeyValuePair<string, RoomFormat> roomKVP in this.levelContent.data.rooms) {
				LevelConvert.curRoomId = roomKVP.Key;
				RoomFormat roomData = roomKVP.Value;

				if(roomData.obj != null) {
					LevelConvert.curRoomLayerId = "obj";
					this.ProcessLayerData(roomData.obj, true);
				} else {
					roomData.obj = new Dictionary<string, Dictionary<string, ArrayList>>();
				}

				if(roomData.bg != null) {
					LevelConvert.curRoomLayerId = "bg";
					this.ProcessLayerData(roomData.bg);
				} else {
					roomData.bg = new Dictionary<string, Dictionary<string, ArrayList>>();
				}

				if(roomData.main != null) {
					LevelConvert.curRoomLayerId = "main";
					this.ProcessLayerData(roomData.main);
				} else {
					roomData.main = new Dictionary<string, Dictionary<string, ArrayList>>();
				}

				if(roomData.fg != null) {
					LevelConvert.curRoomLayerId = "fg";
					this.ProcessLayerData(roomData.fg);
				} else {
					roomData.fg = new Dictionary<string, Dictionary<string, ArrayList>>();
				}
			}

			// Save the level content.
			this.levelContent.SaveLevel(this.deliveryPath, levelId);
		}

		protected virtual void ProcessLayerData( Dictionary<string, Dictionary<string, ArrayList>> layerJson, bool isObjectLayer = false ) {

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layerJson) {
				LevelConvert.curGridY = ushort.Parse(yData.Key);

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value.ToList()) {
					LevelConvert.curGridX = ushort.Parse(xData.Key);

					// Process the Tile JSON
					this.ProcessTileData(xData.Value, isObjectLayer);
				}
			}
		}

		protected virtual void ProcessTileData( ArrayList tileJson, bool isObject = false ) {
			// tileJson[0], tileJson[1], tileJson[2]
		}

		// Overwrites the Tile JSON (per the currently indexed [static] grid coordinate trackers) with the new data:
		protected void OverwriteTileData( byte tileId, byte subTypeId, Dictionary<string, short> paramList = null ) {
			RoomFormat roomData = this.levelContent.data.rooms[curRoomId];
			Dictionary<string, Dictionary<string, ArrayList>> roomLayer = null;

			// Need to load the layer property with a switch.
			if(LevelConvert.curRoomLayerId == "main") { roomLayer = roomData.main; }
			else if(LevelConvert.curRoomLayerId == "obj") { roomLayer = roomData.obj; }
			else if(LevelConvert.curRoomLayerId == "bg") { roomLayer = roomData.bg; }
			else if(LevelConvert.curRoomLayerId == "fg") { roomLayer = roomData.fg; }

			if(roomLayer != null) {
				if(paramList == null) {
					roomLayer[curGridY.ToString()][curGridX.ToString()] = new ArrayList { tileId, subTypeId };
				} else {
					roomLayer[curGridY.ToString()][curGridX.ToString()] = new ArrayList { tileId, subTypeId, paramList };
				}
			}
		}

		// Moves the Tile JSON (per the currently indexed [static] grid coordinate trackers) with the new data:
		protected void MoveTileDataToLayer( LayerEnum newLayerEnum, byte tileId, byte subTypeId, Dictionary<string, short> paramList = null ) {
			RoomFormat roomData = this.levelContent.data.rooms[curRoomId];
			Dictionary<string, Dictionary<string, ArrayList>> roomLayer = null;

			// Get the layer property with a switch.
			if(LevelConvert.curRoomLayerId == "main") { roomLayer = roomData.main; }
			else if(LevelConvert.curRoomLayerId == "obj") { roomLayer = roomData.obj; }
			else if(LevelConvert.curRoomLayerId == "bg") { roomLayer = roomData.bg; }
			else if(LevelConvert.curRoomLayerId == "fg") { roomLayer = roomData.fg; }

			// Remove the Layer
			if(roomLayer != null) {
				roomLayer[curGridY.ToString()].Remove(curGridX.ToString());
			}

			// Move to New Layer
			var newLayer = LevelContent.GetLayerData(roomData, newLayerEnum);

			if(paramList == null) {
				newLayer[curGridY.ToString()][curGridX.ToString()] = new ArrayList { tileId, subTypeId };
			} else {
				newLayer[curGridY.ToString()][curGridX.ToString()] = new ArrayList { tileId, subTypeId, paramList };
			}
		}

		protected void RunAllLevels() {
			DirectoryInfo baseDir = new DirectoryInfo(this.originalPath);
			DirectoryInfo deliverDir = new DirectoryInfo(this.deliveryPath);

			System.Console.WriteLine("-------------------------------------------------------------------------");
			System.Console.WriteLine("Converting All Levels in " + baseDir.FullName);

			// Loop through each Level Directory (e.g. "QC", "TA", etc.)
			foreach(DirectoryInfo levelDir in baseDir.GetDirectories()) {
				System.Console.WriteLine("Scanning through Level Subdirectory: " + levelDir.Name);

				// Verify Level Directory Exists in Delivery Folder
				string toDir = Path.Combine(deliverDir.FullName, levelDir.Name);

				if(!Directory.Exists(toDir)) {
					Directory.CreateDirectory(toDir);
					System.Console.WriteLine("Creating Delivery Directory: " + toDir);
				}

				// Loop through each Level in the Level Directory
				foreach(FileInfo file in levelDir.GetFiles("*.json")) {
					string levelId = Path.GetFileNameWithoutExtension(file.Name);

					// Specific Level Allowance
					if(levelId != "QCALQOD16") { continue; } else { System.Console.WriteLine("WARNING: RESTRICTED level conversions to ID " + levelId); }

					this.ProcessLevel(levelId);
				}
			}

			System.Console.WriteLine("-------------------------------------------------------------------------");
		}
	}
}
