using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Extends Invincibility Time, especially when destroyed.
	public class TopHat : Hat {

		public TopHat( Character character ) : base(character, HatRank.PowerHat) {

		}

		public override void UpdateCharacterStats() {
			this.character.wounds.InvincibleDuration += 30;
		}

		public new void DestroyHat() {
			base.DestroyHat();
			this.character.wounds.SetInvincible((uint) (this.character.wounds.InvincibleDuration + 45));
		}
	}
}
