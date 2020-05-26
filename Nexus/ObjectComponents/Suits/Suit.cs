using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;

namespace Nexus.ObjectComponents {

	public enum SuitRank : byte {
		BaseSuit = 0,           // A base-level suit with no mechanical advantages or powers.
		CosmeticSuit = 1,       // A cosmetic suit; has no mechanical advantages, but is not a base suit.
		PowerSuit = 2,          // A power suit; grants some sort of advantage when equipped, can soak damage.
	}

	public enum SuitSubType : byte {

		// Random Suits
		RandomSuit = 0,
		RandomNinja = 1,
		RandomWizard = 2,
		RandomBasic = 50,

		// Ninja Suits
		BlackNinja = 3,
		BlueNinja = 4,
		GreenNinja = 5,
		RedNinja = 6,
		WhiteNinja = 7,

		// Wizard Suits
		BlueWizard = 8,
		GreenWizard = 9,
		RedWizard = 10,
		WhiteWizard = 11,

		// Basic Suits
		RedBasic = 51,
	}

	public static class SuitMap {

		// Ninjas
		public static readonly BlackNinja BlackNinja = new BlackNinja();
		public static readonly BlueNinja BlueNinja = new BlueNinja();
		public static readonly GreenNinja GreenNinja = new GreenNinja();
		public static readonly RedNinja RedNinja = new RedNinja();
		public static readonly WhiteNinja WhiteNinja = new WhiteNinja();

		// Wizards
		public static readonly BlueWizard BlueWizard = new BlueWizard();
		public static readonly GreenWizard GreenWizard = new GreenWizard();
		public static readonly RedWizard RedWizard = new RedWizard();
		public static readonly WhiteWizard WhiteWizard = new WhiteWizard();

		// Base Suits
		public static readonly BasicRedSuit BasicRedSuit = new BasicRedSuit();
	}

	public class Suit {

		protected SuitRank suitRank;
		protected Atlas atlas;
		public readonly string texture;
		public readonly Hat DefaultCosmeticHat; // A default, Cosmetic Hat associated with the Suit (such as Wizard Hats for Wizards).

		public Suit(SuitRank suitRank, string texture, Hat defaultCosmeticHat = null) {
			this.suitRank = suitRank;
			this.texture = texture;
			this.DefaultCosmeticHat = defaultCosmeticHat;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Objects];
		}

		public static Suit GetSuitBySubType(byte subType) {

			// Random Suit
			if(subType == (byte)SuitSubType.RandomSuit) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(3, 11);
			} else if(subType == (byte)SuitSubType.RandomNinja) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(3, 7);
			} else if(subType == (byte)SuitSubType.RandomWizard) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(8, 11);
			} else if(subType == (byte)SuitSubType.RandomBasic) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(51, 51);
			}

			switch(subType) {

				// Ninjas
				case (byte)SuitSubType.BlackNinja: return SuitMap.BlackNinja;
				case (byte)SuitSubType.BlueNinja: return SuitMap.BlueNinja;
				case (byte)SuitSubType.GreenNinja: return SuitMap.GreenNinja;
				case (byte)SuitSubType.RedNinja: return SuitMap.RedNinja;
				case (byte)SuitSubType.WhiteNinja: return SuitMap.WhiteNinja;

				// Wizards
				case (byte)SuitSubType.BlueWizard: return SuitMap.BlueWizard;
				case (byte)SuitSubType.GreenWizard: return SuitMap.GreenWizard;
				case (byte)SuitSubType.RedWizard: return SuitMap.RedWizard;
				case (byte)SuitSubType.WhiteWizard: return SuitMap.WhiteWizard;

				// Cosmetic Suits
				case (byte)SuitSubType.RedBasic: return SuitMap.BasicRedSuit;
			}

			return SuitMap.BasicRedSuit;
		}

		public static void AssignToCharacter(Character character, byte subType, bool resetStats) {
			Suit suit = Suit.GetSuitBySubType(subType);
			suit.ApplySuit(character, resetStats);
		}

		public void ApplySuit(Character character, bool resetStats) {
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

		public virtual void DestroySuit(Character character, bool resetStats) {

			// Default Suit, Default Head
			Suit.AssignToCharacter(character, (byte)SuitSubType.RedBasic, false);

			if(resetStats) {
				character.stats.ResetCharacterStats();
			}
		}

		// Suits with powers may update the stats of the character that wears them.
		public virtual void UpdateCharacterStats(Character character) { }

		// Some Suits come with default hats.
		public void AssignSuitDefaultHat(Character character) {
			if(this.DefaultCosmeticHat != null) {
				this.DefaultCosmeticHat.ApplyHat(character, false);
			}
		}
	}
}
