using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlagRetry : CheckFlag {

		public CheckFlagRetry() : base() {
			this.Texture = "Flag/Blue";
			this.tileId = (byte)TileEnum.CheckFlagRetry;
			this.title = "Retry Flag";
			this.description = "A single-use retry. If the character dies, they get one retry at this flag.";
		}

		public override void SetupTile(RoomScene room, short gridX, short gridY) {
			base.SetupTile(room, gridX, gridY);

			// Place a Detector beneath the flag:
			room.tilemap.SetMainTile(gridX, (short)(gridY + 1), (byte)TileEnum.DetectRetry, 0);
		}

		protected override void TouchFlag(RoomScene room, Character character, short gridX, short gridY) {
			if(Systems.handler.levelState.SetRetry(room.roomID, gridX, gridY)) {
				Systems.sounds.flag.Play();
			}
		}
	}
}
