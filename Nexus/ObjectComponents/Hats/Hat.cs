using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;
using static Nexus.ObjectComponents.CosmeticHat;

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

	public enum HatSubType : byte {
		None = 0,

		// Power Hats
		AngelHat = 1,
		BambooHat = 2,
		CowboyHat = 3,
		FeatheredHat = 4,
		FedoraHat = 5,
		HardHat = 6,
		RangerHat = 7,
		SpikeyHat = 8,
		TopHat = 9,

		// Random Hat
		RandomHat = 10,

		// Cosmetic Hats

		// Wizard Hats
		WizBlack = 51,
		WizBlue = 52,
		WizGreen = 53,
		WizRed = 54,
		WizWhite = 55,

		// Mage Hats
		MageBlack = 56,
		MageBlue = 57,
		MageGreen = 58,
		MageRed = 59,
		MageWhite = 60,

		// Base Cosmetic Hats
		PooHat = 61,

		// Miscellaneous Hats
		BaseballHat = 91,
	}

	public static class HatMap {

		// Power Hats
		public static readonly AngelHat AngelHat = new AngelHat();
		public static readonly BambooHat BambooHat = new BambooHat();
		public static readonly CowboyHat CowboyHat = new CowboyHat();
		public static readonly FeatheredHat FeatheredHat = new FeatheredHat();
		public static readonly FedoraHat FedoraHat = new FedoraHat();
		public static readonly HardHat HardHat = new HardHat();
		public static readonly RangerHat RangerHat = new RangerHat();
		public static readonly SpikeyHat SpikeyHat = new SpikeyHat();
		public static readonly TopHat TopHat = new TopHat();

		// Cosmetic Hats
		public static readonly CosmeticHat WizBlue = new CosmeticHat((byte)HatSubType.WizBlue);
		public static readonly CosmeticHat WizGreen = new CosmeticHat((byte)HatSubType.WizGreen);
		public static readonly CosmeticHat WizRed = new CosmeticHat((byte)HatSubType.WizRed);
		public static readonly CosmeticHat WizWhite = new CosmeticHat((byte)HatSubType.WizWhite);

		public static readonly CosmeticHat MageBlack = new CosmeticHat((byte)HatSubType.MageBlack);
		public static readonly CosmeticHat MageBlue = new CosmeticHat((byte)HatSubType.MageBlue);
		public static readonly CosmeticHat MageGreen = new CosmeticHat((byte)HatSubType.MageGreen);
		public static readonly CosmeticHat MageRed = new CosmeticHat((byte)HatSubType.MageRed);
		public static readonly CosmeticHat MageWhite = new CosmeticHat((byte)HatSubType.MageWhite);

		// Base Cosmetic Hats
		public static readonly CosmeticHat PooHat = new CosmeticHat((byte)HatSubType.PooHat);
		
		// Miscellaneous Cosmetic Hats
		public static readonly CosmeticHat BaseballHat = new CosmeticHat((byte)HatSubType.MageWhite);
	}

	public class Hat {

		protected HatRank hatRank;
		protected Atlas atlas;
		protected sbyte yOffset;        // The Y-Offset for placing the hat on the character's head.
		protected string SpriteName;	// Name of the base sprite to draw.
		public string subStr { get; protected set; }

		public Hat( HatRank hatRank = HatRank.BaseHat ) {
			this.hatRank = hatRank;
			this.yOffset = -12;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Objects];
		}

		public static Hat GetHatBySubType(byte subType) {

			// Random Hat
			if(subType == (byte)HatSubType.RandomHat) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(1, 9);
			}

			switch(subType) {

				// Power Hats
				case (byte)HatSubType.AngelHat: return HatMap.AngelHat;
				case (byte)HatSubType.BambooHat: return HatMap.BambooHat;
				case (byte)HatSubType.CowboyHat: return HatMap.CowboyHat;
				case (byte)HatSubType.FeatheredHat: return HatMap.FeatheredHat;
				case (byte)HatSubType.FedoraHat: return HatMap.FedoraHat;
				case (byte)HatSubType.HardHat: return HatMap.HardHat;
				case (byte)HatSubType.RangerHat: return HatMap.RangerHat;
				case (byte)HatSubType.SpikeyHat: return HatMap.SpikeyHat;
				case (byte)HatSubType.TopHat: return HatMap.TopHat;

				// Cosmetic Hats
				case (byte)HatSubType.WizBlue: return HatMap.WizBlue;
				case (byte)HatSubType.WizGreen: return HatMap.WizGreen;
				case (byte)HatSubType.WizRed: return HatMap.WizRed;
				case (byte)HatSubType.WizWhite: return HatMap.WizWhite;

				case (byte)HatSubType.MageBlack: return HatMap.MageBlack;
				case (byte)HatSubType.MageBlue: return HatMap.MageBlue;
				case (byte)HatSubType.MageGreen: return HatMap.MageGreen;
				case (byte)HatSubType.MageRed: return HatMap.MageRed;
				case (byte)HatSubType.MageWhite: return HatMap.MageWhite;
			}

			return null;
		}

		public static void AssignToCharacter(Character character, byte subType, bool resetStats) {
			Hat hat = Hat.GetHatBySubType(subType);
			if(hat == null) { return; }
			hat.ApplyHat(character, resetStats);
		}

		public void ApplyHat(Character character, bool resetStats) {
			character.hat = this; // Apply Hat to Character

			// Reset Character Stats
			if(resetStats && character.stats != null) { character.stats.ResetCharacterStats(); }
		}

		public virtual void DestroyHat(Character character, bool resetStats, bool removeCosmetic = false) {
			character.hat = null;

			// Reset Default Hat to Suit's Cosmetic Version (if applicable)
			if(!removeCosmetic && character.suit.DefaultCosmeticHat is Hat) {
				character.suit.DefaultCosmeticHat.ApplyHat(character, false);
			}

			if(resetStats) {
				character.stats.ResetCharacterStats();
			}
		}

		public bool IsCosmeticHat { get { return this.hatRank == HatRank.CosmeticHat; } }
		public bool IsPowerHat { get { return this.hatRank == HatRank.PowerHat; } }

		// Hats with powers may update the stats of the character that wears them.
		public virtual void UpdateCharacterStats(Character character) {}

		// posX and posY should be set to character.posX and .posY respectively.
		public void Draw(bool faceRight, int posX, int posY, int camX, int camY) {
			this.atlas.Draw(this.SpriteName + (faceRight ? "" : "Left"), posX - camX, posY + this.yOffset - camY);
		}
	}
}
