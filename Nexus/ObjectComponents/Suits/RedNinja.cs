using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class RedNinja : Suit {

		public RedNinja() : base(SuitRank.PowerSuit, "RedNinja") {

		}

		public override void UpdateCharacterStats(Character character) {
			CharacterStats stats = character.stats;

			stats.CanWallJump = true;
			stats.CanWallSlide = true;

			stats.JumpMaxTimes++;
			stats.JumpStrength = 9; // 11

			stats.WallJumpDuration = 7;
			stats.WallJumpXStrength = 7;
			stats.WallJumpYStrength = 5;
		}
	}
}
