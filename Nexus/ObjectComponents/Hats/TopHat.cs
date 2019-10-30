using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Extends Invincibility Time, especially when destroyed.
	public class TopHat : Hat {

		public TopHat( Character character ) : base(character, HatRank.PowerHat) {
			this.SpriteName = "Hat/TopHat";
		}

		public override void UpdateCharacterStats() {
			this.character.wounds.InvincibleDuration += 30;
		}

		public override void DestroyHat( bool resetStats ) {
			base.DestroyHat(resetStats);
			this.character.wounds.SetInvincible((uint) (this.character.wounds.InvincibleDuration + 45));
		}
	}
}
