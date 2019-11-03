using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class BlueNinja : Suit {

		public BlueNinja() : base(SuitRank.PowerSuit, "BlueNinja") {

		}

		public override void UpdateCharacterStats(Character character) {
			character.stats.CanWallJump = true;
			character.stats.CanWallSlide = true;
			character.stats.JumpStrength += 2;
		}
	}
}
