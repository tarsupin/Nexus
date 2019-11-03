using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class GreenNinja : Suit {

		public GreenNinja() : base(SuitRank.PowerSuit, "GreenNinja") {

		}

		public override void UpdateCharacterStats(Character character) {
			CharacterStats stats = character.stats;

			stats.CanWallJump = true;
			stats.CanWallSlide = true;

			stats.AirAcceleration = FInt.Create(0.75);            // 0.45
			stats.AirDeceleration = 0 - FInt.Create(0.2);     // -0.2

			stats.RunMaxSpeed = 8;
			stats.RunAcceleration = FInt.Create(0.4);         // 0.3
			stats.RunDeceleration = 0 - FInt.Create(0.15);      // -0.2

			stats.SlideWaitDuration = 30;
			stats.SlideDuration = 15;
			stats.SlideStrength = FInt.Create(14);
		}
	}
}
