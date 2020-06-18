using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class RedWizard : Suit {

		public RedWizard() : base(SuitRank.PowerSuit, "RedWizard", HatMap.WizRed) {
			this.baseStr = "wizard";
			this.subStr = "red";
		}

		public override void UpdateCharacterStats(Character character) {
			CharacterStats stats = character.stats;
			stats.CanFastCast = true;
			stats.JumpMaxTimes++;
			stats.JumpStrength = 9; // 11
		}
	}
}
