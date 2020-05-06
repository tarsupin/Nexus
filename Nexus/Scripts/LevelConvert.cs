﻿using Newtonsoft.Json;
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

		// Conversion Tracking Values - Use these to know which values to change from the root json (curLevelJson)
		static string curLevelId;
		static LevelFormat curLevelJson;
		static string curRoomLayerId;
		static ushort curGridY;
		static ushort curGridX;

		// LevelContent.GetLocalLevelPath		// this returns "QC/QCAL10" from "QCAL10"

		public LevelConvert() {

			// Set Paths
			this.basePath = Systems.filesLocal.localDir;
			this.originalPath = Path.Combine(basePath, originalFolder);
			this.deliveryPath = Path.Combine(basePath, deliveryFolder);

			this.RunAllLevels();
		}

		protected LevelFormat GetLevelJson( string levelId) {
			string levelPath = Path.Combine(this.originalPath, LevelContent.GetLocalLevelPath(levelId));
			string jsonRaw = File.ReadAllText(levelPath);
			LevelFormat levelJson = JsonConvert.DeserializeObject<LevelFormat>(jsonRaw);
			return levelJson;
		}
		
		protected void ProcessLevel( string levelId ) {
			System.Console.WriteLine("Processing Level ID: " + levelId);

			LevelConvert.curLevelId = levelId;

			// Retrieve JSON for the given levelId
			LevelConvert.curLevelJson = this.GetLevelJson(levelId);

			foreach(KeyValuePair<string, RoomFormat> roomKVP in LevelConvert.curLevelJson.room) {
				RoomFormat roomData = roomKVP.Value;

				if(roomData.bg != null) {
					LevelConvert.curRoomLayerId = "bg";
					this.ProcessLayerData(roomData.bg);
				}

				if(roomData.main != null) {
					LevelConvert.curRoomLayerId = "main";
					this.ProcessLayerData(roomData.main);
				}

				if(roomData.obj != null) {
					LevelConvert.curRoomLayerId = "obj";
					this.ProcessLayerData(roomData.obj);
				}

				if(roomData.fg != null) {
					LevelConvert.curRoomLayerId = "fg";
					this.ProcessLayerData(roomData.fg);
				}
			}
		}

		protected virtual void ProcessLayerData( Dictionary<string, Dictionary<string, ArrayList>> layerJson ) {

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layerJson) {
				LevelConvert.curGridY = ushort.Parse(yData.Key);

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					LevelConvert.curGridX = ushort.Parse(xData.Key);

					// Process the Tile JSON
					this.ProcessTileData(xData.Value);
				}
			}
		}

		protected virtual void ProcessTileData( ArrayList tileJson ) {
			// tileJson[0], tileJson[1], tileJson[2]
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
