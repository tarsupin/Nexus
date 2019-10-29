using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.GameEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Nexus.Gameplay {

	public class LevelGenerate {

		// References
		private readonly LevelContent level;
		private LevelScene scene;

		public LevelGenerate(LevelContent level) {
			this.level = level;
		}

		public void GenerateRoom(LevelScene scene, string roomNum) {
			this.scene = scene;

			// NOTE: If room properties are NULL, the LevelFormat probably broke and it needs to be updated (or level data was invalid structure).
			RoomFormat room = this.level.data.room[roomNum];

			this.GenerateTileLayer(room.main);
			this.GenerateObjectLayer(room.obj);
			this.GenerateTileLayer(room.fg, true);
		}

		private void GenerateTileLayer(Dictionary<string, Dictionary<string, ArrayList>> layer, bool useForeground = false) {

			// TODO HIGH PRIORITY: This should be done by level, since we'll need to know it to create tilemap sizing.
			// Prepare Minimum Width and Height for Level
			ushort levelWidth = 24;
			ushort levelHeight = 16;

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layer) {
				ushort gridY = ushort.Parse(yData.Key);

				if(gridY > levelHeight) { levelHeight = gridY; }

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					ushort gridX = ushort.Parse(xData.Key);

					if(gridX > levelWidth) { levelWidth = gridX; }

					if(xData.Value.Count == 2) {
						this.AddTileToScene(gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]), useForeground);
					} else if(xData.Value.Count > 2) {
						this.AddTileToScene(gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]), useForeground, (JObject) xData.Value[2]);
					}
				}
			}
		}
		
		private void GenerateObjectLayer(Dictionary<string, Dictionary<string, ArrayList>> layer) {

			// Prepare Minimum Width and Height for Level
			ushort levelWidth = 24;
			ushort levelHeight = 16;

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layer) {
				ushort gridY = ushort.Parse(yData.Key);

				if(gridY > levelHeight) { levelHeight = gridY; }

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					ushort gridX = ushort.Parse(xData.Key);

					if(gridX > levelWidth) { levelWidth = gridX; }

					if(xData.Value.Count == 2) {
						this.AddObjectToScene(gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]));
					} else if(xData.Value.Count > 2) {
						this.AddObjectToScene(gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]), (JObject) xData.Value[2]);
					}
				}
			}
		}

		private void AddTileToScene(ushort gridX, ushort gridY, byte type, byte subType = 0, bool useForeground = false, JObject paramList = null) {
			GameMapper mapper = Systems.mapper;

			// Identify Tile Class Type, If Applicable
			Type classType;
			bool hasType = useForeground ? mapper.FGTileMap.TryGetValue(type, out classType) : mapper.TileMap.TryGetValue(type, out classType);
			if(!hasType || classType == null) { return; }

			// If there is a "TileGenerate" method, run its special generation rules:
			if(classType.GetMethod("TileGenerate") != null) {
				classType.GetMethod("TileGenerate", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[] { (Scene)this.scene, (ushort)gridX, (ushort)gridY, (byte)subType });

				// TODO: GET THE TILE BY GRIDX GRIDY
				//if(tile != null && paramList != null) {
				//	tile.UpdateParams(paramList);
				//}
			}
		}

		private void AddObjectToScene(ushort gridX, ushort gridY, byte type, byte subType = 0, JObject paramList = null) {

			// Skip Certain Flags
			// TODO: Might need to adjust how "Spawn" flags work here.
			if(type == 100) { return; }      // "Character" flag should be ignored.

			GameMapper mapper = Systems.mapper;

			// Identify Object Class Type
			Type classType;
			bool hasType = mapper.ObjectMap.TryGetValue(type, out classType);
			if(!hasType || classType == null) { return; }

			// Prepare Position
			FVector pos = FVector.Create(
				Snap.GridToPos((ushort)TilemapEnum.TileWidth, gridX),
				Snap.GridToPos((ushort)TilemapEnum.TileHeight, gridY)
			);

			// TODO: See if we can eliminate this; removing reflection would be a good idea. This effect only really benefits platforms, and that was on web.
			if(classType.GetMethod("Generate") != null) {
				classType.GetMethod("Generate", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[] { (LevelScene)this.scene, (byte)subType, (FVector)pos, (JObject) paramList });

			// Create Object
			} else {
				GameObject gameObj = (GameObject) Activator.CreateInstance(classType, new object[] { this.scene, (byte) subType, (FVector) pos, (JObject) paramList });

				// Add the Object to the Scene
				if(gameObj is DynamicGameObject) {
					this.scene.AddToScene((DynamicGameObject) gameObj, true);
				}
			}
		}

		// TODO HIGH PRIORITY: See generateCharacter() in LevelGenerator.ts
		//public void GenerateCharacter( FVector pos ) {

		//}
	}
}
