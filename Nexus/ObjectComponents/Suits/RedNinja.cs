using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class RedNinja : Suit {

		public RedNinja( Character character ) : base(character, SuitRank.PowerSuit, "RedNinja") {

		}

		public override void UpdateCharacterStats() {
			CharacterStats stats = this.character.stats;

			stats.CanWallJump = true;
			stats.CanWallSlide = true;

			stats.JumpMaxTimes = 2;
			stats.JumpStrength = 9; // 11

			stats.WallJumpXStrength = 6;
			stats.WallJumpYStrength = 9;
		}
	}
}
