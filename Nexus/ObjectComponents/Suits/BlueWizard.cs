using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class BlueWizard : Suit {

		public BlueWizard( Character character ) : base(character, SuitRank.PowerSuit, "BlueWizard") {

		}

		public override void AssignSuitDefaultHat() {
			this.character.hat = new CosmeticHat(this.character, "Hat/WizBlueHat" );
		}

		public override void UpdateCharacterStats() {
			CharacterStats stats = this.character.stats;

			stats.CanFastCast = true;
			stats.JumpDuration = 25;
			stats.JumpStrength = 9;
			stats.BaseGravity = FInt.Create(0.4);

			this.character.physics.SetGravity(stats.BaseGravity);
		}
	}
}
