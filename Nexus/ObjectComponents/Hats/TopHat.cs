using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Extends Invincibility Time, especially when destroyed.
	public class TopHat : Hat {

		public TopHat() : base(HatRank.PowerHat) {
			this.SpriteName = "Hat/TopHat";
			this.subStr = "top";
		}

		public override void UpdateCharacterStats(Character character) {
			character.wounds.InvincibleDuration += 30;
		}

		public override void DestroyHat(Character character, bool resetStats, bool removeCosmetic = false) {
			base.DestroyHat(character, resetStats);
			character.wounds.SetInvincible(character.wounds.InvincibleDuration + 45);
		}
	}
}
