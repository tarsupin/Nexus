using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DetectorCheckpoint : Detector {

		public DetectorCheckpoint() : base() {
			this.tileId = (byte)TileEnum.DetectCheckpoint;
		}

		protected override bool RunSpecialDetection(RoomScene room, Character actor, short gridX, short gridY, DirCardinal dir) {
			if(actor.GridY < gridY) { return false; }
			return Systems.mapper.TileDict[(byte)TileEnum.CheckFlagCheckpoint].RunImpact(actor.room, actor, gridX, (short)(gridY - 1), dir);
		}
	}
}
