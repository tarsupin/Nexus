using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class RedWizard : Suit {

		public RedWizard( Character character ) : base(character, SuitRank.PowerSuit) {

		}

		public override void AssignSuitDefaultHat() {
			this.character.hat = new RedWizardHat(this.character);
		}

		public override void UpdateCharacterStats() {
			CharacterStats stats = this.character.stats;
			stats.CanFastCast = true;
			stats.JumpMaxTimes += 1;
		}
	}
}
