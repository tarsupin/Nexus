using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlagCheckpoint : CheckFlag {

		public CheckFlagCheckpoint() : base(FlagSubType.Checkpoint) {
			this.Texture = "Flag/Red";
			this.tileId = (byte)TileEnum.CheckFlagCheckpoint;
		}
	}
}
