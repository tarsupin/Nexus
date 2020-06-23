using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class BlueWizard : Suit {

		public BlueWizard() : base(SuitRank.PowerSuit, "BlueWizard", HatMap.WizBlue) {
			this.baseStr = "wizard";
			this.subStr = "blue";
		}

		public override void UpdateCharacterStats(Character character) {
			CharacterStats stats = character.stats;

			stats.CanFastCast = true;
			character.stats.JumpStrength += 2;
		}
	}
}
