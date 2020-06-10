using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class BlueWizard : Suit {

		public BlueWizard() : base(SuitRank.PowerSuit, "BlueWizard", HatMap.WizBlue) {

		}

		public override void UpdateCharacterStats(Character character) {
			CharacterStats stats = character.stats;

			stats.CanFastCast = true;
			stats.JumpDuration = 25;
			stats.JumpStrength = 9;
			stats.BaseGravity = FInt.Create(0.4);

			character.physics.SetGravity(stats.BaseGravity);
		}
	}
}
