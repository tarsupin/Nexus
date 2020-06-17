using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlagPass : CheckFlag {

		// TODO: THE SUBTYPE NEEDS TO BE 0. NOT PASSING A FLAG SUB TYPE.
		// TODO: THE SUBTYPE NEEDS TO BE 0. NOT PASSING A FLAG SUB TYPE.
		// TODO: THE SUBTYPE NEEDS TO BE 0. NOT PASSING A FLAG SUB TYPE.

		public CheckFlagPass() : base(FlagSubType.PassFlag) {
			this.Texture = "Flag/White";
			this.tileId = (byte)TileEnum.CheckFlagPass;
			this.title = "Checkpoint-Pass Flag";
			this.description = "A Checkpoint Flag that reacts to characters passing above it.";
			this.moveParamSet = Params.ParamMap["Checkpoint"];
		}
	}
}
