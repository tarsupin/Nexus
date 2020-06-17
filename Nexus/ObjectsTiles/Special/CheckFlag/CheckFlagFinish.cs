using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlagFinish : CheckFlag {

		// TODO: THE SUBTYPE NEEDS TO BE 0. NOT PASSING A FLAG SUB TYPE.
		// TODO: THE SUBTYPE NEEDS TO BE 0. NOT PASSING A FLAG SUB TYPE.
		// TODO: THE SUBTYPE NEEDS TO BE 0. NOT PASSING A FLAG SUB TYPE.

		public CheckFlagFinish() : base(FlagSubType.FinishFlag) {
			this.Texture = "Flag/Finish";
			this.tileId = (byte)TileEnum.CheckFlagFinish;
			this.title = "Finish Flag";
			this.description = "Triggers a level victory.";
		}
	}
}
