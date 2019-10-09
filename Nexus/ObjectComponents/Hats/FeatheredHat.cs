using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Increases Jump Height and Duration
	public class FeatheredHat : Hat {

		public FeatheredHat( Character character ) : base(character, HatRank.PowerHat) {

		}

		public override void UpdateCharacterStats() {
			CharacterStats stats = this.character.stats;
			stats.JumpDuration += 2;
			stats.JumpStrength += 1;
			stats.WallJumpDuration += 2;
			stats.WallJumpYStrength += 1;
		}
	}
}
