﻿using Newtonsoft.Json.Linq;
using Nexus.GameEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public static class EditorRoomGenerate {

		public static void GenerateRoom(EditorRoomScene room, LevelContent levelContent, string roomId) {

			// NOTE: If room properties are NULL, the LevelFormat probably broke and it needs to be updated (or level data was invalid structure).
			RoomFormat roomData = levelContent.data.rooms[roomId];

			if(roomData.bg != null) { EditorRoomGenerate.GenerateLayer(room, roomData.bg); }
			if(roomData.main != null) { EditorRoomGenerate.GenerateLayer(room, roomData.main); }
			//if(roomData.obj != null) { EditorRoomGenerate.GenerateLayer(room, roomData.obj); }
			if(roomData.fg != null) { EditorRoomGenerate.GenerateLayer(room, roomData.fg, true); }
		}

		private static void GenerateLayer(EditorRoomScene room, Dictionary<string, Dictionary<string, ArrayList>> layer, bool useForeground = false) {

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layer) {
				ushort gridY = ushort.Parse(yData.Key);

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					ushort gridX = ushort.Parse(xData.Key);

					if(xData.Value.Count == 2) {
						EditorRoomGenerate.AddTileToScene(room, gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]), useForeground);
					} else if(xData.Value.Count > 2) {
						EditorRoomGenerate.AddTileToScene(room, gridX, gridY, Convert.ToByte(xData.Value[0]), Convert.ToByte(xData.Value[1]), useForeground, (JObject) xData.Value[2]);
					}
				}
			}
		}
		
		private static void AddTileToScene(EditorRoomScene room, ushort gridX, ushort gridY, byte type, byte subType = 0, bool useForeground = false, JObject paramList = null) {
			room.tilemap.AddTileAtGrid(gridX, gridY, type, subType);
		}

		public static void DetermineRoomSize(RoomFormat roomData, out ushort xCount, out ushort yCount) {

			// The Room may store its size in the data:
			if(roomData.Width != 0 && roomData.Height != 0) {
				xCount = roomData.Width;
				yCount = roomData.Height;
				return;
			}

			// Prepare Minimum Width and Height for Level
			xCount = 24;
			yCount = 16;

			// Scan the full level to determine it's size:
			if(roomData.bg != null) { EditorRoomGenerate.DetermineLayerSize(roomData.bg, ref xCount, ref yCount); }
			if(roomData.main != null) { EditorRoomGenerate.DetermineLayerSize(roomData.main, ref xCount, ref yCount); }
			if(roomData.obj != null) { EditorRoomGenerate.DetermineLayerSize(roomData.obj, ref xCount, ref yCount); }
			if(roomData.fg != null) { EditorRoomGenerate.DetermineLayerSize(roomData.fg, ref xCount, ref yCount); }
		}

		private static void DetermineLayerSize(Dictionary<string, Dictionary<string, ArrayList>> layer, ref ushort xCount, ref ushort yCount) {

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layer) {
				ushort gridY = ushort.Parse(yData.Key);

				if(gridY > yCount) {
					yCount = gridY;
				}

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					ushort gridX = ushort.Parse(xData.Key);
					if(gridX > xCount) { xCount = gridX; }
				}
			}
		}
	}
}
