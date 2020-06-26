using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Grants Shell Master
	public class BambooHat : Hat {

		public BambooHat() : base(HatRank.PowerHat) {
			this.subType = (byte)HatSubType.BambooHat;
			this.SpriteName = "Hat/BambooHat";
			this.subStr = "bamboo";
		}

		public override void UpdateCharacterStats(Character character) {
			character.stats.ShellMastery = true;
		}
	}
}
