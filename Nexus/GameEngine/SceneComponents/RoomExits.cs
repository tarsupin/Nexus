
using System.Collections.Generic;

namespace Nexus.Engine {

	// Add a RoomExits class for every RoomScene. It will track the Doors within the scene, or other portals if additional types are added.
	// Doors have a .SetupTile method, run by the RoomGenerate class, which adds themselves to RoomExits.AddDoor();
	public class RoomExits {

		public List<(byte tileId, byte subTypeId, short gridX, short gridY)> exits { get; protected set; }

		public RoomExits() {
			this.exits = new List<(byte tileId, byte subTypeId, short gridX, short gridY)>();
		}

		public (byte tileId, byte subTypeId, short gridX, short gridY) GetExit(short gridX, short gridY) {
			foreach(var exit in this.exits) {
				if(exit.gridX == gridX && exit.gridY == gridY) { return exit; }
			}
			return (0, 0, 0, 0);
		}

		public void AddExit(byte tileId, byte subTypeId, short gridX, short gridY) {
			this.exits.Add((tileId, subTypeId, gridX, gridY));
		}

		public void UpdateDoor(short gridX, short gridY, byte toSubTypeId) {
			for(short i = 0; i < this.exits.Count; i++) {
				if(this.exits[i].gridX == gridX && this.exits[i].gridY == gridY) {
					this.exits[i] = (this.exits[i].tileId, toSubTypeId, gridX, gridY);
				}
			}
		}
	}
}
