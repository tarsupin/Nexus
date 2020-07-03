using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlagFinish : CheckFlag {

		public CheckFlagFinish() : base() {
			this.Texture = "Flag/Finish";
			this.tileId = (byte)TileEnum.CheckFlagFinish;
			this.title = "Finish Flag";
			this.description = "Triggers a level victory.";
		}

		public override void SetupTile(RoomScene room, short gridX, short gridY) {
			base.SetupTile(room, gridX, gridY);

			// Place a Detector beneath the flag:
			room.tilemap.SetMainTile(gridX, (short)(gridY + 1), (byte)TileEnum.DetectFinish, 0);
		}

		protected override void TouchFlag(RoomScene room, Character character, short gridX, short gridY) {

			// Play Victory Sound
			room.PlaySound(Systems.sounds.woohoo, 1f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);

			// Complete the level.
			Systems.handler.levelState.CompleteLevel(character);
		}
	}
}
