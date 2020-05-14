using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlagCheckpoint : CheckFlag {

		public CheckFlagCheckpoint() : base(FlagSubType.Checkpoint) {
			this.Texture = "Flag/Red";
			this.tileId = (byte)TileEnum.CheckFlagCheckpoint;
			this.title = "Checkpoint Flag";
			this.description = "Saves the character's position for level re-attempts. May grant bonuses.";
			this.paramSets = new Params[1] { Params.ParamMap["Checkpoint"] };
		}
	}
}
