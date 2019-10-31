using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class RedWizard : Suit {

		public RedWizard( Character character ) : base(character, SuitRank.PowerSuit, "RedWizard") {

		}

		public override void AssignSuitDefaultHat() {
			this.character.hat = new CosmeticHat(this.character, "Hat/WizRedHat");
		}

		public override void UpdateCharacterStats() {
			CharacterStats stats = this.character.stats;
			stats.CanFastCast = true;
			stats.JumpMaxTimes += 1;
		}
	}
}
