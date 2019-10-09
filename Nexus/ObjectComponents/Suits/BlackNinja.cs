using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class BlackNinja : Suit {

		public BlackNinja( Character character ) : base(character, SuitRank.PowerSuit) {

		}

		public override void UpdateCharacterStats() {
			this.character.stats.CanWallJump = true;
			this.character.stats.CanWallGrab = true;
		}
	}
}
