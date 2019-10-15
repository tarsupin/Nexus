using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class GreenWizard : Suit {

		public GreenWizard( Character character ) : base(character, SuitRank.PowerSuit) {

		}

		public override void AssignSuitDefaultHat() {
			this.character.hat = new CosmeticHat(this.character, "Hat/WizGreenHat");
		}

		public override void UpdateCharacterStats() {
			CharacterStats stats = this.character.stats;

			stats.CanFastCast = true;

			stats.JumpDuration = 12; // 10
			stats.JumpStrength = 13; // 11
			stats.AirAcceleration = FInt.Create(0.6); // 0.45
			stats.AirDeceleration = 0 - FInt.Create(0.2); // -0.2

			stats.RunMaxSpeed = 8; // 7
			stats.RunAcceleration = FInt.Create(0.4); // 0.3
			stats.RunDeceleration = 0 - FInt.Create(0.15); // -0.2

			stats.SlideWaitDuration = 30;
			stats.SlideDuration = 15;
			stats.SlideStrength = FInt.Create(14);

			stats.BaseGravity = FInt.Create(0.9); // 0.7 - Has more weight so that the wizard feels faster in general.

			this.character.physics.SetGravity(stats.BaseGravity);
		}
	}
}
