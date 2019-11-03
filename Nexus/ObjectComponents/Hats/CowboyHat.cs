using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Grants Wall Sliding and Jumping
	public class CowboyHat : Hat {

		public CowboyHat() : base(HatRank.PowerHat) {
			this.SpriteName = "Hat/CowboyHat";
		}

		public override void UpdateCharacterStats(Character character) {
			character.stats.CanWallJump = true;
			character.stats.CanWallSlide = true;
		}
	}
}
