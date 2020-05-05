using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlagRetry : CheckFlag {

		public CheckFlagRetry() : base(FlagSubType.RetryFlag) {
			this.Texture = "Flag/Blue";
			this.tileId = (byte)TileEnum.CheckFlagRetry;
		}
	}
}
