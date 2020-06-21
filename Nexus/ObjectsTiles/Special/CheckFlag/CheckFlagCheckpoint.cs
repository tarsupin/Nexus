using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlagCheckpoint : CheckFlag {

		public CheckFlagCheckpoint() : base() {
			this.Texture = "Flag/Red";
			this.tileId = (byte)TileEnum.CheckFlagCheckpoint;
			this.title = "Checkpoint Flag";
			this.description = "Saves the character's position for level re-attempts. May grant bonuses.";
			this.moveParamSet =  Params.ParamMap["Checkpoint"];
		}

		public override void SetupTile(RoomScene room, short gridX, short gridY) {
			base.SetupTile(room, gridX, gridY);

			// Place a Detector beneath the flag:
			room.tilemap.SetMainTile(gridX, (short)(gridY + 1), (byte)TileEnum.DetectCheckpoint, 0);
		}

		protected override void TouchFlag(RoomScene room, Character character, short gridX, short gridY) {
			if(Systems.handler.levelState.SetCheckpoint(room.roomID, gridX, gridY)) {
				base.TouchFlag(room, character, gridX, gridY);
			}
		}
	}
}
