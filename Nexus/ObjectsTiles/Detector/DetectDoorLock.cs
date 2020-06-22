using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DetectorDoorLock : Detector {

		public DetectorDoorLock() : base() {
			this.tileId = (byte)TileEnum.DetectDoorLock;
		}

		protected override bool RunSpecialDetection(RoomScene room, Character actor, short gridX, short gridY, DirCardinal dir) {
			if(actor.GridY < gridY) { return false; }
			return Systems.mapper.TileDict[(byte)TileEnum.DoorLock].RunImpact(actor.room, actor, gridX, (short)(gridY - 1), dir);
		}
	}
}
