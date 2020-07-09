using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class DetectorCheckpointPass : Detector {

		public DetectorCheckpointPass() : base() {
			this.tileId = (byte)TileEnum.DetectCheckpointPass;
		}

		public override bool RunSpecialDetection(RoomScene room, Character actor, short gridX, short gridY, DirCardinal dir) {

			// Check to see if the actor has already received a checkpoint at this X value. If so, end the function.
			if(Systems.handler.levelState.checkpoint.active && Systems.handler.levelState.checkpoint.gridX == gridX) { return false; }

			// Otherwise, attempt to locate the checkpoint at this X value:
			short gridYScan = (short)(gridY + 1);
			byte[] tile = room.tilemap.GetTileDataAtGrid(gridX, gridYScan);

			// If the location below is another DetectorCheckpointPass (this tile type), then continue the trend:
			if(tile[0] == this.tileId) {
				DetectorCheckpointPass det = (DetectorCheckpointPass)Systems.mapper.TileDict[(byte)TileEnum.DetectCheckpointPass];
				return det.RunSpecialDetection(room, actor, gridX, gridYScan, dir);
			}

			// If we found the Check Pass Flag from the next scan test, return the impact.
			if(tile[0] == (byte)TileEnum.CheckFlagPass) {
				return Systems.mapper.TileDict[(byte)TileEnum.CheckFlagPass].RunImpact(actor.room, actor, gridX, gridYScan, dir);
			}

			// Check if the checkpoint is above:
			return Systems.mapper.TileDict[(byte)TileEnum.CheckFlagPass].RunImpact(actor.room, actor, gridX, (short)(gridY - 1), dir);
		}
	}
}
