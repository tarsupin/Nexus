using Newtonsoft.Json.Linq;
using Nexus.GameEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public static class EditorDetection {

		public static void DetectRoomSize(RoomFormat roomData, out ushort xCount, out ushort yCount) {

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
			if(roomData.bg != null) { EditorDetection.DetectLayerSize(roomData.bg, ref xCount, ref yCount); }
			if(roomData.main != null) { EditorDetection.DetectLayerSize(roomData.main, ref xCount, ref yCount); }
			if(roomData.obj != null) { EditorDetection.DetectLayerSize(roomData.obj, ref xCount, ref yCount); }
			if(roomData.fg != null) { EditorDetection.DetectLayerSize(roomData.fg, ref xCount, ref yCount); }
		}

		private static void DetectLayerSize(Dictionary<string, Dictionary<string, ArrayList>> layer, ref ushort xCount, ref ushort yCount) {

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
