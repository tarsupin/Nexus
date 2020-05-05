using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlagPass : CheckFlag {

		public CheckFlagPass() : base(FlagSubType.PassFlag) {
			this.Texture = "Flag/White";
			this.tileId = (byte)TileEnum.CheckFlagPass;
		}
	}
}
