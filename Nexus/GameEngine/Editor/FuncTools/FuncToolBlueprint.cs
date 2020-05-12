using System.Collections;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	// This class tracks a grid up to 20x20 blueprint of tiles, allowing user to place them all at once.
	public class FuncToolBlueprint : FuncTool {

		private ArrayList[,] gridTrack = new ArrayList[20, 20];
		private byte blueprintWidth;
		private byte blueprintHeight;

		public FuncToolBlueprint() : base() {
			this.spriteName = "Icons/Blueprint";
			this.title = "Blueprint";
			this.description = "No behavior at this time.";
		}

		public void CopyBlueprint(EditorRoomScene scene, ushort xStart, ushort yStart) {
			var layerData = scene.levelContent.data.rooms[scene.roomID].main;

			// Loop through the blueprint:
			for(ushort y = 0; y < this.blueprintHeight; y++) {
				for(ushort x = 0; x < this.blueprintWidth; x++) {

					// Get the value stored in this blueprint at correct tile position:
					ArrayList bpData = this.gridTrack[y, x];
					
					// Copy the blueprint at correct tile position in level editor:
					scene.PlaceTile(layerData, (ushort)(xStart + x), (ushort)(yStart + y), (byte)bpData[0], (byte)bpData[1], (Dictionary<string, object>) bpData[2]);
				}
			}
		}
	}
}
