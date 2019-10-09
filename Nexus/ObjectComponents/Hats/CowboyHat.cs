using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Grants Wall Sliding and Jumping
	public class CowboyHat : Hat {

		public CowboyHat( Character character ) : base(character, HatRank.PowerHat) {

		}

		public override void UpdateCharacterStats() {
			this.character.stats.CanWallJump = true;
			this.character.stats.CanWallSlide = true;
		}
	}
}
