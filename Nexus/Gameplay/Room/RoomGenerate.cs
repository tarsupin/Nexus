using Newtonsoft.Json;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public static class RoomGenerate {

		public static void GenerateRoom(RoomScene room, LevelContent levelContent, string roomId) {

			// NOTE: If room properties are NULL, the LevelFormat probably broke and it needs to be updated (or level data was invalid structure).
			RoomFormat roomData = levelContent.data.rooms[roomId];

			if(roomData.bg != null) { RoomGenerate.GenerateTileLayer(room, roomData.bg, LayerEnum.bg); }
			if(roomData.main != null) { RoomGenerate.GenerateTileLayer(room, roomData.main, LayerEnum.main); }
			if(roomData.obj != null) { RoomGenerate.GenerateObjectLayer(room, roomData.obj); }
			if(roomData.fg != null) { RoomGenerate.GenerateTileLayer(room, roomData.fg, LayerEnum.fg); }

			// Build "Barrier Wall" around Level. Designed to protect against objects crossing tile path.
			short maxX = (short) (room.tilemap.XCount + (byte)TilemapEnum.GapLeft + (byte)TilemapEnum.GapRight - 1);
			short maxY = (short) (room.tilemap.YCount + (byte)TilemapEnum.GapUp + (byte)TilemapEnum.GapDown - 1);

			for(short y = 0; y <= maxY; y++) {
				room.tilemap.SetMainTile(0, y, (byte)TileEnum.BarrierWall, 0);
				room.tilemap.SetMainTile(maxX, y, (byte)TileEnum.BarrierWall, 0);
			}

			for(short x = 0; x <= maxX; x++) {
				room.tilemap.SetMainTile(x, 0, (byte)TileEnum.BarrierWall, 0);
				room.tilemap.SetMainTile(x, maxY, (byte)TileEnum.BarrierWall, 0);
			}
		}

		private static void GenerateTileLayer(RoomScene room, Dictionary<string, Dictionary<string, ArrayList>> layer, LayerEnum layerEnum) {
			var TileDict = Systems.mapper.TileDict;

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layer) {
				short gridY = short.Parse(yData.Key);

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					short gridX = short.Parse(xData.Key);

					byte tileId = Convert.ToByte(xData.Value[0]);
					byte subType = Convert.ToByte(xData.Value[1]);
					Dictionary<string, short> paramList = null;

					if(xData.Value.Count > 2) {

						// ERRORS HERE MEAN: The json data saved a string instead of a short; e.g. {"Suit", "WhiteNinja"} instead of {"Suit", 2}
						// To fix it, we need to run LevelConvert updates that make the appropriate changes.
						paramList = JsonConvert.DeserializeObject<Dictionary<string, short>>(xData.Value[2].ToString());
					}

					// Check the TileDict to identify setup requirements. If it does, run it's setup process.
					TileObject tile = TileDict[tileId];

					// If the tile is considered "Pre-Setup Only" - only run its pre-setup method; don't add it to the scene.
					if(tile.setupRules == SetupRules.PreSetupOnly) {
						(tile as dynamic).PreSetup(room, (short)(gridX + (byte)TilemapEnum.GapLeft), (short)(gridY + (byte)TilemapEnum.GapUp), tileId, subType, paramList);
						continue;
					}

					RoomGenerate.AddTileToScene(room, layerEnum, gridX, gridY, tileId, subType, paramList);

					// Post Setup
					if(tile.setupRules >= SetupRules.SetupTile) {
						(tile as dynamic).SetupTile(room, (short)(gridX + (byte)TilemapEnum.GapLeft), (short)(gridY + (byte)TilemapEnum.GapUp));
					}
				}
			}
		}
		
		private static void GenerateObjectLayer(RoomScene room, Dictionary<string, Dictionary<string, ArrayList>> layer) {

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layer) {
				short gridY = short.Parse(yData.Key);

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					short gridX = short.Parse(xData.Key);

					if(xData.Value.Count == 2) {
						RoomGenerate.AddObjectToScene(room, gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]));
					} else if(xData.Value.Count > 2) {

						// ERRORS HERE MEAN: The json data saved a string instead of a short; e.g. {"Suit", "WhiteNinja"} instead of {"Suit", 2}
							// To fix it, we need to run LevelConvert updates that make the appropriate changes.
						Dictionary<string, short> paramList = JsonConvert.DeserializeObject<Dictionary<string, short>>(xData.Value[2].ToString());
						RoomGenerate.AddObjectToScene(room, gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]), paramList);
					}
				}
			}
		}

		private static void AddTileToScene(RoomScene room, LayerEnum layerEnum, short gridX, short gridY, byte tileId, byte subType = 0, Dictionary<string, short> paramList = null) {

			// Adjust for World Gaps
			gridX += (byte)TilemapEnum.GapLeft;
			gridY += (byte)TilemapEnum.GapUp;

			if(layerEnum == LayerEnum.main) {
				room.tilemap.SetMainTile(gridX, gridY, tileId, subType);
				if(paramList != null) { room.tilemap.SetParamList(gridX, gridY, paramList); }
			} else if(layerEnum == LayerEnum.bg) {
				room.tilemap.SetBGTile(gridX, gridY, tileId, subType);
			} else if(layerEnum == LayerEnum.fg) {
				room.tilemap.SetFGTile(gridX, gridY, tileId, subType);
			}
		}

		private static void AddObjectToScene(RoomScene room, short gridX, short gridY, byte objectId, byte subType = 0, Dictionary<string, short> paramList = null) {

			// Adjust for World Gaps
			gridX += (byte)TilemapEnum.GapLeft;
			gridY += (byte)TilemapEnum.GapUp;

			// Prepare Position
			FVector pos = FVector.Create(
				Snap.GridToPos((short)TilemapEnum.TileWidth, gridX),
				Snap.GridToPos((short)TilemapEnum.TileHeight, gridY)
			);

			GameMapper mapper = Systems.mapper;

			// Identify Object Class Type
			Type classType;
			bool hasType = mapper.ObjectTypeDict.TryGetValue(objectId, out classType);
			if(!hasType || classType == null) { return; }

			// Create Object
			GameObject gameObj = (GameObject) Activator.CreateInstance(classType, new object[] { room, (byte) subType, (FVector) pos, (Dictionary<string, short>) paramList });

			// Add the Object to the Scene
			if(gameObj is GameObject) {
				room.AddToScene((GameObject) gameObj, true);
			}
		}

		public static void DetectRoomSize(RoomFormat roomData, out short xCount, out short yCount) {

			// Prepare Minimum Width and Height for Level
			xCount = (byte)TilemapEnum.MinWidth;
			yCount = (byte)TilemapEnum.MinHeight;

			// Scan the full level to determine it's size:
			if(roomData.bg != null) { RoomGenerate.DetectLayerSize(roomData.bg, ref xCount, ref yCount); }
			if(roomData.main != null) { RoomGenerate.DetectLayerSize(roomData.main, ref xCount, ref yCount); }
			if(roomData.obj != null) { RoomGenerate.DetectLayerSize(roomData.obj, ref xCount, ref yCount); }
			if(roomData.fg != null) { RoomGenerate.DetectLayerSize(roomData.fg, ref xCount, ref yCount); }
		}

		private static void DetectLayerSize(Dictionary<string, Dictionary<string, ArrayList>> layer, ref short xCount, ref short yCount) {

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layer) {
				short gridY = short.Parse(yData.Key);

				if(gridY >= yCount && yData.Value.Count > 0) {
					yCount = (short)(gridY + 1); // +1 is because yCount includes 0
				}

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					short gridX = short.Parse(xData.Key);
					if(gridX >= xCount && xData.Value.Count > 0) { xCount = (short)(gridX + 1); } // +1 is because xCount includes 0
				}
			}
		}
	}
}
