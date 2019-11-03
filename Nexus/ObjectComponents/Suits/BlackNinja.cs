using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class BlackNinja : Suit {

		public BlackNinja() : base(SuitRank.PowerSuit, "BlackNinja") {

		}

		public override void UpdateCharacterStats(Character character) {
			character.stats.CanWallJump = true;
			character.stats.CanWallGrab = true;
		}
	}
}
