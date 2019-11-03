using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Protects from damage above, and inflicts damage above.
	public class SpikeyHat : Hat {

		public SpikeyHat() : base(HatRank.PowerHat) {
			this.SpriteName = "Hat/SpikeyHat";
		}

		public override void UpdateCharacterStats(Character character) {
			CharacterStats stats = character.stats;
			stats.SafeVsDamageAbove = true;
			stats.InflictDamageAbove = true;
		}
	}
}
