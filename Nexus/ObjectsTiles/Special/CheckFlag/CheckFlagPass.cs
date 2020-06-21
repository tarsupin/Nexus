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
			this.moveParamSet = Params.ParamMap["Checkpoint"];
		}

		protected override void TouchFlag(RoomScene room, Character character, short gridX, short gridY) {
			if(Systems.handler.levelState.SetCheckpoint(room.roomID, gridX, gridY)) {
				base.TouchFlag(room, character, gridX, gridY);
			}
		}
	}
}
