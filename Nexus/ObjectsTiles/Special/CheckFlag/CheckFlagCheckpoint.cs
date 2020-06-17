using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlagCheckpoint : CheckFlag {

		// TODO: THE SUBTYPE NEEDS TO BE 0. NOT PASSING A FLAG SUB TYPE.
		// TODO: THE SUBTYPE NEEDS TO BE 0. NOT PASSING A FLAG SUB TYPE.
		// TODO: THE SUBTYPE NEEDS TO BE 0. NOT PASSING A FLAG SUB TYPE.

		public CheckFlagCheckpoint() : base(FlagSubType.Checkpoint) {
			this.Texture = "Flag/Red";
			this.tileId = (byte)TileEnum.CheckFlagCheckpoint;
			this.title = "Checkpoint Flag";
			this.description = "Saves the character's position for level re-attempts. May grant bonuses.";
			this.moveParamSet =  Params.ParamMap["Checkpoint"];
		}
	}
}
