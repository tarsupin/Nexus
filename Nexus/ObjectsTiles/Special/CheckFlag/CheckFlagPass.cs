using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlagPass : CheckFlag {

		public CheckFlagPass() : base() {
			this.Texture = "Flag/White";
			this.tileId = (byte)TileEnum.CheckFlagPass;
			this.title = "Checkpoint-Pass Flag";
			this.description = "A Checkpoint Flag that reacts to characters passing above it.";
			this.actParamSet = Params.ParamMap["Upgrades"];
		}

		public override void SetupTile(RoomScene room, short gridX, short gridY) {
			base.SetupTile(room, gridX, gridY);

			// Place a Detector beneath the flag:
			room.tilemap.SetMainTile(gridX, (short)(gridY + 1), (byte)TileEnum.DetectCheckpointPass, 0);

			// Place Detectors above the flag:
			this.PlaceDetectorsAbove(room, gridX, gridY);
		}

		private void PlaceDetectorsAbove(RoomScene room, short gridX, short gridY) {
			TilemapLevel tilemap = room.tilemap;

			// Scan for solid tiles above the flag, up to 18 above (for a full screen)
			for(short testY = (short)(gridY - 1); testY > gridY - 18; testY--) {
				if(testY < (byte)TilemapEnum.GapUp) { return; }

				// If there is a blocking tile at this grid sqare, stop placements.
				if(CollideTile.IsBlockingSquare(tilemap, gridX, testY, DirCardinal.Down)) { return; }

				// Otherwise, add a Detector.
				room.tilemap.SetMainTile(gridX, testY, (byte)TileEnum.DetectCheckpointPass, 0);
			}
		}

		protected override void TouchFlag(RoomScene room, Character character, short gridX, short gridY) {
			if(Systems.handler.levelState.SetCheckpoint(room.roomID, gridX, gridY)) {
				room.PlaySound(Systems.sounds.flag, 1f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
				this.ReceiveFlagUpgrades(room, character, gridX, gridY);
			}
		}
	}
}
