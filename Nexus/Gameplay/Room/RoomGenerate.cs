using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Nexus.Gameplay {

	public static class RoomGenerate {

		public static void GenerateRoom(RoomScene room, LevelContent levelContent, string roomId) {

			// NOTE: If room properties are NULL, the LevelFormat probably broke and it needs to be updated (or level data was invalid structure).
			RoomFormat roomData = levelContent.data.room[roomId];

			if(roomData.main != null) { RoomGenerate.GenerateTileLayer(room, roomData.main); }
			if(roomData.obj != null) { RoomGenerate.GenerateObjectLayer(room, roomData.obj); }
			if(roomData.fg != null) { RoomGenerate.GenerateTileLayer(room, roomData.fg, true); }
		}

		private static void GenerateTileLayer(RoomScene room, Dictionary<string, Dictionary<string, ArrayList>> layer, bool useForeground = false) {

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layer) {
				ushort gridY = ushort.Parse(yData.Key);

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					ushort gridX = ushort.Parse(xData.Key);

					if(xData.Value.Count == 2) {
						RoomGenerate.AddTileToScene(room, gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]), useForeground);
					} else if(xData.Value.Count > 2) {
						RoomGenerate.AddTileToScene(room, gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]), useForeground, (JObject) xData.Value[2]);
					}
				}
			}
		}
		
		private static void GenerateObjectLayer(RoomScene room, Dictionary<string, Dictionary<string, ArrayList>> layer) {

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layer) {
				ushort gridY = ushort.Parse(yData.Key);

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					ushort gridX = ushort.Parse(xData.Key);

					if(xData.Value.Count == 2) {
						RoomGenerate.AddObjectToScene(room, gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]));
					} else if(xData.Value.Count > 2) {
						RoomGenerate.AddObjectToScene(room, gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]), (JObject) xData.Value[2]);
					}
				}
			}
		}

		private static void AddTileToScene(RoomScene room, ushort gridX, ushort gridY, byte type, byte subType = 0, bool useForeground = false, JObject paramList = null) {
			GameMapper mapper = Systems.mapper;

			// Identify Tile Class Type, If Applicable
			Type classType;
			bool hasType = useForeground ? mapper.FGTileMap.TryGetValue(type, out classType) : mapper.TileMap.TryGetValue(type, out classType);
			if(!hasType || classType == null) { return; }

			// If there is a "TileGenerate" method, run its special generation rules:
			if(classType.GetMethod("TileGenerate") != null) {
				classType.GetMethod("TileGenerate", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[] { room, (ushort)gridX, (ushort)gridY, (byte)subType });

				// TODO: GET THE TILE BY GRIDX GRIDY
				//if(tile != null && paramList != null) {
				//	tile.UpdateParams(paramList);
				//}
			}
		}

		private static void AddObjectToScene(RoomScene room, ushort gridX, ushort gridY, byte type, byte subType = 0, JObject paramList = null) {

			// Prepare Position
			FVector pos = FVector.Create(
				Snap.GridToPos((ushort)TilemapEnum.TileWidth, gridX),
				Snap.GridToPos((ushort)TilemapEnum.TileHeight, gridY)
			);

			// Character Creation
			if(type == 100) {
				Character character = new Character(room, 0, pos, null);
				room.AddToScene(character, true);
				return;
			}

			GameMapper mapper = Systems.mapper;

			// Identify Object Class Type
			Type classType;
			bool hasType = mapper.ObjectMap.TryGetValue(type, out classType);
			if(!hasType || classType == null) { return; }

			// TODO: See if we can eliminate this; removing reflection would be a good idea. This effect only really benefits platforms, and that was on web.
			if(classType.GetMethod("Generate") != null) {
				classType.GetMethod("Generate", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[] { room, (byte)subType, (FVector)pos, (JObject) paramList });

			// Create Object
			} else {
				GameObject gameObj = (GameObject) Activator.CreateInstance(classType, new object[] { room, (byte) subType, (FVector) pos, (JObject) paramList });

				// Add the Object to the Scene
				if(gameObj is DynamicGameObject) {
					room.AddToScene((DynamicGameObject) gameObj, true);
				}
			}
		}

		// TODO HIGH PRIORITY: See generateCharacter() in LevelGenerator.ts
		//public void GenerateCharacter( FVector pos ) {

		//}

		public static void DetermineRoomSize(RoomFormat roomData, out ushort levelHeight, out ushort levelWidth) {

			// The Room may store its size in the data:
			if(roomData.Width != 0 && roomData.Height != 0) {
				levelHeight = roomData.Height;
				levelWidth = roomData.Width;
				return;
			}

			// Prepare Minimum Width and Height for Level
			levelWidth = 24;
			levelHeight = 16;

			// Scan the full level to determine it's size:
			RoomGenerate.DetermineLayerSize(roomData.main, ref levelHeight, ref levelWidth);
			RoomGenerate.DetermineLayerSize(roomData.obj, ref levelHeight, ref levelWidth);
			RoomGenerate.DetermineLayerSize(roomData.fg, ref levelHeight, ref levelWidth);
		}

		public static void DetermineLayerSize(Dictionary<string, Dictionary<string, ArrayList>> layer, ref ushort levelHeight, ref ushort levelWidth) {

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layer) {
				ushort gridY = ushort.Parse(yData.Key);

				if(gridY > levelHeight) { levelHeight = gridY; }

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					ushort gridX = ushort.Parse(xData.Key);
					if(gridX > levelWidth) { levelWidth = gridX; }
				}
			}
		}
	}
}
