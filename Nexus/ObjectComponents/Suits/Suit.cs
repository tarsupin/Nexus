
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public enum SuitRank : byte {
		BaseSuit = 0,			// A base-level suit with no mechanical advantages or powers.
		CosmeticSuit = 1,		// A cosmetic suit; has no mechanical advantages, but is not a base suit.
		PowerSuit = 2,			// A power suit; grants some sort of advantage when equipped, can soak damage.
	}

	public class Suit {

		protected SuitRank suitRank;
		public readonly string texture;
		public readonly Hat DefaultCosmeticHat; // A default, Cosmetic Hat associated with the Suit (such as Wizard Hats for Wizards).

		public Suit( SuitRank suitRank, string texture, Hat defaultCosmeticHat = null ) {
			this.suitRank = suitRank;
			this.texture = texture;
			this.DefaultCosmeticHat = defaultCosmeticHat;
		}

		public void ApplySuit( Character character, bool resetStats ) {
			character.suit = this; // Apply Suit to Character

			// Apply the Suit's Default Hat if the character has no hat OR a cosmetic hat.
			if(character.hat == null || (character.hat is Hat && character.hat.IsCosmeticHat)) {
				this.AssignSuitDefaultHat(character);
			}

			// Reset Character Stats
			if(resetStats && character.stats != null) { character.stats.ResetCharacterStats(); }
		}

		public bool IsCosmeticSuit { get { return this.suitRank == SuitRank.CosmeticSuit; } }
		public bool IsPowerSuit { get { return this.suitRank == SuitRank.PowerSuit; } }

		// Suits with powers may update the stats of the character that wears them.
		public virtual void UpdateCharacterStats(Character character) {}

		// Some Suits come with default hats.
		public void AssignSuitDefaultHat(Character character) {
			if(this.DefaultCosmeticHat != null) {
				this.DefaultCosmeticHat.ApplyHat(character, false);
			}
		}
	}
}
