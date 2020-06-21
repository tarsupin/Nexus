using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DetectorFinish : Detector {

		protected CheckFlagFinish tileObj;

		public DetectorFinish() : base() {
			this.tileId = (byte)TileEnum.DetectCheckpoint;
			this.tileObj = (CheckFlagFinish) Systems.mapper.TileDict[(byte)TileEnum.CheckFlagFinish];
		}

		protected override bool RunSpecialDetection(RoomScene room, Character actor, short gridX, short gridY, DirCardinal dir) {
			if(actor.GridY < gridY) { return false; }
			return this.tileObj.RunImpact(actor.room, actor, gridX, (short)(gridY - 1), dir);
		}
	}
}
