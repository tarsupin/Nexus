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

		protected override void TouchFlag(RoomScene room, Character character, short gridX, short gridY) {

			// Play Victory Sound
			Systems.sounds.woohoo.Play();

			// Complete the level.
			Systems.handler.levelState.CompleteLevel();

			// If you're in a campaign, update the level completion:
			// TODO
			//Systems.handler.campaignState.ProcessLevelCompletion();
		}
	}
}
