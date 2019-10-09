using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class BlueWizard : Suit {

		public BlueWizard( Character character ) : base(character, SuitRank.PowerSuit) {

		}

		public override void AssignSuitDefaultHat() {
			this.character.hat = new BlueWizardHat(this.character);
		}

		public override void UpdateCharacterStats() {
			CharacterStats stats = this.character.stats;

			stats.CanFastCast = true;
			stats.JumpDuration = 25;
			stats.JumpStrength = 9;
			stats.BaseGravity = FInt.FromParts(0, 400);

			this.character.physics.SetGravity(stats.BaseGravity);
		}
	}
}
