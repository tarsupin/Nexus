using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DetectorRetry : Detector {

		public DetectorRetry() : base() {
			this.tileId = (byte)TileEnum.DetectRetry;
		}

		public override bool RunSpecialDetection(RoomScene room, Character actor, short gridX, short gridY, DirCardinal dir) {
			if(actor.GridY < gridY) { return false; }
			return Systems.mapper.TileDict[(byte)TileEnum.CheckFlagRetry].RunImpact(actor.room, actor, gridX, (short)(gridY - 1), dir);
		}
	}
}
