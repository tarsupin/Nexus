using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class GreenNinja : Suit {

		public GreenNinja( Character character ) : base(character, SuitRank.PowerSuit) {

		}

		public override void UpdateCharacterStats() {
			CharacterStats stats = this.character.stats;

			stats.CanWallJump = true;
			stats.CanWallSlide = true;

			stats.AirAcceleration = FInt.FromParts(0, 750);            // 0.45
			stats.AirDeceleration = 0 - FInt.FromParts(0, 200);     // -0.2

			stats.RunMaxSpeed = 8;
			stats.RunAcceleration = FInt.FromParts(0, 400);         // 0.3
			stats.RunDeceleration = 0 - FInt.FromParts(0, 150);      // -0.2

			stats.SlideWaitDuration = 30;
			stats.SlideDuration = 15;
			stats.SlideStrength = 14;
		}
	}
}
