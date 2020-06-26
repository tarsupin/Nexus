using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class BlackNinja : Suit {

		public BlackNinja() : base(SuitRank.PowerSuit, "BlackNinja") {
			this.subType = (byte)SuitSubType.BlackNinja;
			this.baseStr = "ninja";
			this.subStr = "black";
		}

		public override void UpdateCharacterStats(Character character) {
			character.stats.CanWallJump = true;
			character.stats.CanWallGrab = true;
		}
	}
}
