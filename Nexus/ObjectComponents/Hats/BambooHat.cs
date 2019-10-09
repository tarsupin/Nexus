using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Grants Shell Master
	public class BambooHat : Hat {

		public BambooHat( Character character ) : base(character, HatRank.PowerHat) {

		}

		public override void UpdateCharacterStats() {
			this.character.stats.ShellMastery = true;
		}
	}
}
