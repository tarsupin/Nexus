using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Protects you against falling projectiles.
	public class HardHat : Hat {

		public HardHat() : base(HatRank.PowerHat) {
			this.SpriteName = "Hat/HardHat";
			this.subStr = "hard";
		}

		public override void UpdateCharacterStats(Character character) {
			character.stats.SafeVsDamageAbove = true;
		}
	}
}
