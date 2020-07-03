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
		}

		protected override void TouchFlag(RoomScene room, Character character, short gridX, short gridY) {
			if(Systems.handler.levelState.SetCheckpoint(room.roomID, gridX, gridY)) {
				room.PlaySound(Systems.sounds.flag, 1f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
				this.ReceiveFlagUpgrades(room, character, gridX, gridY);
			}
		}
	}
}
