using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Grants Double Jump
	public class AngelHat : Hat {

		public AngelHat() : base(HatRank.PowerHat) {
			this.subType = (byte)HatSubType.AngelHat;
			this.SpriteName = "Hat/AngelHat";
			this.subStr = "angel";
		}

		public override void UpdateCharacterStats(Character character) {
			character.stats.JumpMaxTimes += 1;
		}
	}
}
