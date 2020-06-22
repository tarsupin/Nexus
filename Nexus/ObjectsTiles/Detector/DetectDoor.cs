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
			return Systems.mapper.TileDict[(byte)TileEnum.Door].RunImpact(actor.room, actor, gridX, (short)(gridY - 1), dir);
		}
	}
}
