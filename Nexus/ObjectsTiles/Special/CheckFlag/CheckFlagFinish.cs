using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlagFinish : CheckFlag {

		public CheckFlagFinish() : base(FlagSubType.FinishFlag) {
			this.Texture = "Flag/Finish";
			this.tileId = (byte)TileEnum.CheckFlagFinish;
			this.title = "Finish Flag";
			this.description = "Triggers a level victory.";
		}
	}
}
