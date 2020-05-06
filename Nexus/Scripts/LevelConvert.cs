using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Nexus.Scripts {

	class LevelConvert {

		// Config
		readonly string originalFolder = "Levels-Orig";
		readonly string deliveryFolder = "Levels";

		// Level Directory
		readonly string basePath = "";
		readonly string originalPath = "";
		readonly string deliveryPath = "";

		// LevelContent.GetLocalLevelPath		// this returns "QC/QCAL10" from "QCAL10"

		public LevelConvert() {
			this.basePath = Systems.filesLocal.localDir;
			this.originalPath = Path.Combine(basePath, originalFolder);
			this.deliveryPath = Path.Combine(basePath, deliveryFolder);

			this.RunAllLevels();
		}

		private LevelFormat GetLevelJson( string levelId) {
			string levelPath = Path.Combine(this.originalPath, LevelContent.GetLocalLevelPath(levelId));
			string jsonRaw = File.ReadAllText(levelPath);
			LevelFormat jsonContent = JsonConvert.DeserializeObject<LevelFormat>(jsonRaw);
			return jsonContent;
		}
		
		private void ProcessLevel( string levelId ) {
			System.Console.WriteLine("Processing Level ID: " + levelId);

			// Retrieve JSON for the given levelId
			LevelFormat levelJson = this.GetLevelJson(levelId);

			foreach(KeyValuePair<string, RoomFormat> roomKVP in levelJson.room) {
				RoomFormat roomData = roomKVP.Value;

				if(roomData.main != null) { this.ProcessLayerData(roomData.main); }
				if(roomData.obj != null) { this.ProcessLayerData(roomData.obj); }
				if(roomData.fg != null) { this.ProcessLayerData(roomData.fg); }
			}
		}

		private void ProcessLayerData( Dictionary<string, Dictionary<string, ArrayList>> layerJson ) {

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layerJson) {
				ushort gridY = ushort.Parse(yData.Key);

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					//ushort gridX = ushort.Parse(xData.Key);

					// Process the Tile JSON
					this.ProcessTileData(xData.Value);
				}
			}
		}

		protected void ProcessTileData( ArrayList tileJson ) {
			// tileJson[0], tileJson[1], tileJson[2]
		}

		private void RunAllLevels() {
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
					Console.WriteLine("Creating Delivery Directory: " + toDir);
				}

				// Loop through each Level in the Level Directory
				foreach(FileInfo file in levelDir.GetFiles("*.json")) {
					string levelId = Path.GetFileNameWithoutExtension(file.Name);
					this.ProcessLevel(levelId);
				}
			}

			System.Console.WriteLine("-------------------------------------------------------------------------");
		}
	}
}
