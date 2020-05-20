using Microsoft.Xna.Framework.Graphics.PackedVector;
using Newtonsoft.Json;
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
			RoomFormat roomData = levelContent.data.rooms[roomId];

			if(roomData.bg != null) { RoomGenerate.GenerateTileLayer(room, roomData.bg, LayerEnum.bg); }
			if(roomData.main != null) { RoomGenerate.GenerateTileLayer(room, roomData.main, LayerEnum.main); }
			if(roomData.obj != null) { RoomGenerate.GenerateObjectLayer(room, roomData.obj); }
			if(roomData.fg != null) { RoomGenerate.GenerateTileLayer(room, roomData.fg, LayerEnum.fg); }

			// Build "Resistance Barrier" around Level. Designed to protect against objects crossing tile path.
			for(ushort y = 0; y <= room.tilemap.YCount; y++) {
				room.tilemap.SetMainTile(0, y, 1, 0);
				room.tilemap.SetMainTile((ushort)(room.tilemap.XCount + 1), y, 1, 0);
			}

			for(ushort x = 0; x <= room.tilemap.XCount; x++) {
				room.tilemap.SetMainTile(x, 0, 1, 0);
				room.tilemap.SetMainTile(x, (ushort)(room.tilemap.YCount + (byte)TilemapEnum.WorldGapDown), 1, 0);
			}
		}

		private static void GenerateTileLayer(RoomScene room, Dictionary<string, Dictionary<string, ArrayList>> layer, LayerEnum layerEnum) {
			
			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layer) {
				ushort gridY = ushort.Parse(yData.Key);

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					ushort gridX = ushort.Parse(xData.Key);

					if(xData.Value.Count == 2) {
						RoomGenerate.AddTileToScene(room, layerEnum, gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]));
					} else if(xData.Value.Count > 2) {

						// ERRORS HERE MEAN: The json data saved a string instead of a short; e.g. {"Suit", "WhiteNinja"} instead of {"Suit", 2}
							// To fix it, we need to run LevelConvert updates that make the appropriate changes.
						Dictionary<string, short> paramList = JsonConvert.DeserializeObject<Dictionary<string, short>>(xData.Value[2].ToString());
						RoomGenerate.AddTileToScene(room, layerEnum, gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]), paramList);
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

						// ERRORS HERE MEAN: The json data saved a string instead of a short; e.g. {"Suit", "WhiteNinja"} instead of {"Suit", 2}
							// To fix it, we need to run LevelConvert updates that make the appropriate changes.
						Dictionary<string, short> paramList = JsonConvert.DeserializeObject<Dictionary<string, short>>(xData.Value[2].ToString());
						RoomGenerate.AddObjectToScene(room, gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]), paramList);
					}
				}
			}
		}

		private static void AddTileToScene(RoomScene room, LayerEnum layerEnum, ushort gridX, ushort gridY, byte type, byte subType = 0, Dictionary<string, short> paramList = null) {

			// Adjust for World Gaps
			gridX += (byte)TilemapEnum.WorldGapLeft;
			gridY += (byte)TilemapEnum.WorldGapUp;

			if(layerEnum == LayerEnum.main) {
				room.tilemap.SetMainTile(gridX, gridY, type, subType);
			} else if(layerEnum == LayerEnum.bg) {
				room.tilemap.SetBGTile(gridX, gridY, type, subType);
			} else if(layerEnum == LayerEnum.fg) {
				room.tilemap.SetFGTile(gridX, gridY, type, subType);
			}
		}

		private static void AddObjectToScene(RoomScene room, ushort gridX, ushort gridY, byte type, byte subType = 0, Dictionary<string, short> paramList = null) {

			// Adjust for World Gaps
			gridX += (byte)TilemapEnum.WorldGapLeft;
			gridY += (byte)TilemapEnum.WorldGapUp;

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
			bool hasType = mapper.ObjectTypeDict.TryGetValue(type, out classType);
			if(!hasType || classType == null) { return; }

			// TODO: See if we can eliminate this; removing reflection would be a good idea. This effect only really benefits platforms, and that was on web.
			if(classType.GetMethod("Generate") != null) {
				classType.GetMethod("Generate", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[] { room, (byte)subType, (FVector)pos, (Dictionary<string, short>) paramList });

			// Create Object
			} else {
				DynamicObject gameObj = (DynamicObject) Activator.CreateInstance(classType, new object[] { room, (byte) subType, (FVector) pos, (Dictionary<string, short>) paramList });

				// Add the Object to the Scene
				if(gameObj is DynamicObject) {
					room.AddToScene((DynamicObject) gameObj, true);
				}
			}
		}

		// TODO HIGH PRIORITY: See generateCharacter() in LevelGenerator.ts
		//public void GenerateCharacter( FVector pos ) {

		//}

		public static void DetectRoomSize(RoomFormat roomData, out ushort xCount, out ushort yCount) {

			// The Room may store its size in the data:
			if(roomData.Width != 0 && roomData.Height != 0) {
				xCount = (ushort) roomData.Width;
				yCount = (ushort) roomData.Height;
				return;
			}

			// Prepare Minimum Width and Height for Level
			xCount = 24;
			yCount = 18;

			// Scan the full level to determine it's size:
			if(roomData.bg != null) { RoomGenerate.DetectLayerSize(roomData.bg, ref xCount, ref yCount); }
			if(roomData.main != null) { RoomGenerate.DetectLayerSize(roomData.main, ref xCount, ref yCount); }
			if(roomData.obj != null) { RoomGenerate.DetectLayerSize(roomData.obj, ref xCount, ref yCount); }
			if(roomData.fg != null) { RoomGenerate.DetectLayerSize(roomData.fg, ref xCount, ref yCount); }

			// The final gap needs to be provided
			xCount++;
			yCount++;
		}

		private static void DetectLayerSize(Dictionary<string, Dictionary<string, ArrayList>> layer, ref ushort xCount, ref ushort yCount) {

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layer) {
				ushort gridY = ushort.Parse(yData.Key);

				if(gridY > yCount) {
					yCount = (ushort) (gridY);
				}

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					ushort gridX = ushort.Parse(xData.Key);
					if(gridX > xCount) { xCount = (ushort) (gridX); }
				}
			}
		}
	}
}
