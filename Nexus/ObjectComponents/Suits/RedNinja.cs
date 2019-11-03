using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class RedNinja : Suit {

		public RedNinja() : base(SuitRank.PowerSuit, "RedNinja") {

		}

		public override void UpdateCharacterStats(Character character) {
			CharacterStats stats = character.stats;

			stats.CanWallJump = true;
			stats.CanWallSlide = true;

			stats.JumpMaxTimes = 2;
			stats.JumpStrength = 9; // 11

			stats.WallJumpXStrength = 6;
			stats.WallJumpYStrength = 9;
		}
	}
}
