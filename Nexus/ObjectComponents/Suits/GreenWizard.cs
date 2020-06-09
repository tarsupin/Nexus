using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class GreenWizard : Suit {

		public GreenWizard() : base(SuitRank.PowerSuit, "GreenWizard", HatMap.WizardGreenHat) {

		}

		public override void UpdateCharacterStats(Character character) {
			CharacterStats stats = character.stats;

			stats.CanFastCast = true;

			stats.JumpDuration = 12; // 10
			stats.JumpStrength = 12; // 11
			stats.AirAcceleration = FInt.Create(0.55);		// 0.45
			stats.AirDeceleration = FInt.Create(-0.2);		// -0.2

			stats.RunMaxSpeed = 8; // 7
			stats.RunAcceleration = FInt.Create(0.4);		// 0.3
			stats.RunDeceleration = FInt.Create(-0.25);     // -0.2

			stats.WallJumpDuration = 7; // 8
			stats.WallJumpXStrength = 7; // 8
			stats.WallJumpYStrength = 9; // 7

			stats.SlideWaitDuration = 30;
			stats.SlideDuration = 15;
			stats.SlideStrength = FInt.Create(14);
			
			stats.BaseGravity = FInt.Create(0.9); // 0.7 - Has more weight so that the wizard feels faster in general.

			character.physics.SetGravity(stats.BaseGravity);
		}
	}
}
