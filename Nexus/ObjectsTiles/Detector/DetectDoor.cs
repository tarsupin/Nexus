using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DetectorDoor : Detector {

		public DetectorDoor() : base() {
			this.tileId = (byte)TileEnum.DetectDoor;
		}

		protected override bool RunSpecialDetection(RoomScene room, Character actor, short gridX, short gridY, DirCardinal dir) {
			if(actor.GridY < gridY) { return false; }

			// Identify the tile above (check if it's a door or a locked door)
			byte[] tileData = room.tilemap.GetTileDataAtGrid(gridX, (short)(gridY - 1));
			byte tileId = tileData[0];

			// Interact with the door
			if(tileId == (byte)TileEnum.Door || tileId == (byte)TileEnum.DoorLock) {
				return Systems.mapper.TileDict[tileId].RunImpact(actor.room, actor, gridX, (short)(gridY - 1), dir);
			}

			return false;
		}
	}
}
