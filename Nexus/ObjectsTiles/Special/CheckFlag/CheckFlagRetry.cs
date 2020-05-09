using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlagRetry : CheckFlag {

		public CheckFlagRetry() : base(FlagSubType.RetryFlag) {
			this.Texture = "Flag/Blue";
			this.tileId = (byte)TileEnum.CheckFlagRetry;
			this.title = "Retry Flag";
			this.description = "A single-use retry. If the character dies, they get one retry at this flag.";
		}
	}
}
