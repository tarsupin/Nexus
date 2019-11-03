using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public enum SuitRank : byte {
		BaseSuit = 0,			// A base-level suit with no mechanical advantages or powers.
		CosmeticSuit = 1,		// A cosmetic suit; has no mechanical advantages, but is not a base suit.
		PowerSuit = 2,			// A power suit; grants some sort of advantage when equipped, can soak damage.
	}

	public class Suit {

		protected readonly Character character;
		protected SuitRank suitRank;
		public readonly string texture;

		public Suit( Character character, SuitRank suitRank, string texture, string defaultCosmeticHat = "" ) {
			this.character = character;

			// Destroy existing Suit, if applicable.
			if(character.suit is Suit) { this.DestroySuit(false); }

			this.suitRank = suitRank;
			this.texture = texture;
			this.DefaultCosmeticHat = defaultCosmeticHat;

			// Assign Default Hat for this Suit
			if(this.character.hat == null) { this.AssignSuitDefaultHat(); }

			// If the Character has no hat, but it's a base hat or cosmetic hat, reassign the default hat (if applicable)
			else if(this.character.hat is Hat && this.character.hat.IsCosmeticHat) {
				this.character.hat.DestroyHat(false);
				this.AssignSuitDefaultHat();
			}

			// Reset Character Stats (in case this suit alters any)
			if(this.character.stats != null) { this.character.stats.ResetCharacterStats();  }
		}

		public bool IsCosmeticSuit { get { return this.suitRank == SuitRank.CosmeticSuit; } }
		public bool IsPowerSuit { get { return this.suitRank == SuitRank.PowerSuit; } }

		// A default, Cosmetic Hat associated with the Suit (such as Wizard Hats for Wizards).
		public string DefaultCosmeticHat { get; protected set; }

		// Suits with powers may update the stats of the character that wears them.
		public virtual void UpdateCharacterStats() {}

		// Some Suits come with default hats.
		public virtual void AssignSuitDefaultHat() {
			if(this.DefaultCosmeticHat != "") {
				this.character.hat = new CosmeticHat(this.character, this.DefaultCosmeticHat);
			}
		}

		// Some Suits may require cleanup.
		public virtual void DestroySuit( bool resetStats ) {
			// TODO HIGH PRIORITY: Determine the character's base suit type based on player class? Or scene, or something...
			this.character.suit = null;
			if(resetStats) { this.character.stats.ResetCharacterStats(); }
		}
	}
}
