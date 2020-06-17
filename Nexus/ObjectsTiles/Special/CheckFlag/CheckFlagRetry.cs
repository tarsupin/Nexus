using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlagRetry : CheckFlag {

		// TODO: THE SUBTYPE NEEDS TO BE 0. NOT PASSING A FLAG SUB TYPE.
		// TODO: THE SUBTYPE NEEDS TO BE 0. NOT PASSING A FLAG SUB TYPE.
		// TODO: THE SUBTYPE NEEDS TO BE 0. NOT PASSING A FLAG SUB TYPE.

		public CheckFlagRetry() : base(FlagSubType.RetryFlag) {
			this.Texture = "Flag/Blue";
			this.tileId = (byte)TileEnum.CheckFlagRetry;
			this.title = "Retry Flag";
			this.description = "A single-use retry. If the character dies, they get one retry at this flag.";
		}
	}
}
