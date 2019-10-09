using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class WhiteNinja : Suit {

		public WhiteNinja( Character character ) : base(character, SuitRank.PowerSuit) {

		}

		public override void UpdateCharacterStats() {
			this.character.stats.CanWallJump = true;
			this.character.stats.CanWallSlide = true;
		}
	}
}
