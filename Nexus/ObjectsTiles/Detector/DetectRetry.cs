using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DetectorRetry : Detector {

		protected CheckFlagRetry tileObj;

		public DetectorRetry() : base() {
			this.tileId = (byte)TileEnum.DetectCheckpoint;
			this.tileObj = (CheckFlagRetry) Systems.mapper.TileDict[(byte)TileEnum.CheckFlagRetry];
		}

		protected override bool RunSpecialDetection(RoomScene room, Character actor, short gridX, short gridY, DirCardinal dir) {
			if(actor.GridY < gridY) { return false; }
			return this.tileObj.RunImpact(actor.room, actor, gridX, (short)(gridY - 1), dir);
		}
	}
}
