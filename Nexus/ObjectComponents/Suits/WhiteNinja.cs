using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class WhiteNinja : Suit {

		public WhiteNinja() : base(SuitRank.PowerSuit, "WhiteNinja") {
			this.subType = (byte)SuitSubType.WhiteNinja;
			this.baseStr = "ninja";
			this.subStr = "white";
		}

		public override void UpdateCharacterStats(Character character) {
			character.stats.CanWallJump = true;
			character.stats.CanWallSlide = true;
		}
	}
}
