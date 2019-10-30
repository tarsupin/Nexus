using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Protects from damage above, and inflicts damage above.
	public class SpikeyHat : Hat {

		public SpikeyHat( Character character ) : base(character, HatRank.PowerHat) {
			this.SpriteName = "Hat/SpikeyHat";
		}

		public override void UpdateCharacterStats() {
			CharacterStats stats = this.character.stats;
			stats.SafeVsDamageAbove = true;
			stats.InflictDamageAbove = true;
		}
	}
}
