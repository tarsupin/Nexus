using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class BlueNinja : Suit {

		public BlueNinja( Character character ) : base(character, SuitRank.PowerSuit, "BlueNinja") {

		}

		public override void UpdateCharacterStats() {
			this.character.stats.CanWallJump = true;
			this.character.stats.CanWallSlide = true;
			this.character.stats.JumpStrength += 2;
		}
	}
}
