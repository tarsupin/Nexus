using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Grants Double Jump
	public class AngelHat : Hat {

		public AngelHat( Character character ) : base(character, HatRank.PowerHat) {
			this.SpriteName = "Hat/AngelHat";
		}

		public override void UpdateCharacterStats() {
			this.character.stats.JumpMaxTimes += 1;
		}
	}
}
