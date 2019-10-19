using Nexus.Objects;

/*
	Powered Hats:
		Angel Hat - Gravity / Flight Boost, Extra Jump
		Bamboo Hat - Shell Mastery & Immunity
		Cowboy Hat - Gain Wall Jump, and Wall Climb
		Feathered Hat - Higher Jump
		Fedora Hat - Pass Through Platforms
		Hard Hat - Ignore Projectiles From Above
		Ranger Hat - Additional Projectile Firing Power
		Top Hat - Extra Invulnerability Time
	
	Cosmetic Hats:
		Baseball Hat
		Wizard Hats (All Colors)
	
	Thoughts:
		Mind Control Hat - Gain Mind Control Power (Creature Control)
		Pyschokinesis Hat - Gain Targeting Power (Psychokinesis)
		Telekinesis Hat - Gain Telekinesis
		Strong - Can push heavy items, heavy rocks, heavy mobile objects. - Can destroy blocks you couldn't normally.
*/

namespace Nexus.ObjectComponents {

	public enum HatRank : byte {
		BaseHat = 0,			// A Base-Level Hat with no mechanical advantages or powers.
		CosmeticHat = 1,		// A Cosmetic Hat; has no mechanical advantages, but is not a Base Hat.
		PowerHat = 2,			// A Power Hat; grants some sort of advantage when equipped, can soak damage.
	}

	public class Hat {

		// References
		protected readonly Character character;

		protected HatRank hatRank;
		protected bool faceRight;       // True when the hat is facing right.
		protected sbyte yOffset;		// The Y-Offset for placing the hat on the character's head.

		public Hat( Character character, HatRank hatRank = HatRank.BaseHat ) {
			this.character = character;
			this.hatRank = hatRank;
			this.faceRight = this.character.FaceRight;
			this.yOffset = -10;

			// Rendering
			// TODO HIGH PRIORITY: Render Hat. Create Draw() method, mimic game object.

			// Update Character Abilities (in case this hat alters any)
			if(this.character.stats is CharacterStats) { this.character.stats.ResetCharacterStats(); }
		}

		public bool IsCosmeticHat { get { return this.hatRank == HatRank.CosmeticHat; } }
		public bool IsPowerHat { get { return this.hatRank == HatRank.PowerHat; } }

		// Hats with powers may update the stats of the character that wears them.
		public virtual void UpdateCharacterStats() {}

		// Some Hats may require cleanup.
		public void DestroyHat() {
			this.character.hat = null;

			// Reset Default Hat to Suit's Cosmetic Version (if applicable)
			if(this.character.suit is Suit && this.character.suit.IsCosmeticSuit) {
				this.character.hat = new CosmeticHat(this.character, this.character.suit.DefaultCosmeticHat);
			}

			this.character.stats.ResetCharacterStats();
		}
	}
}
