using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;
using System.Collections.Generic;

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
		Fedora = 5,
		HardHat = 6,
		RangerHat = 7,
		SpikeyHat = 8,
		TopHat = 9,

		// Random Hat
		RandomPowerHat = 10,

		// Cosmetic Hats
		RandomMagicHat = 50,

		// Wizard Hats
		WizBlack = 51,
		WizBlue = 52,
		WizGreen = 53,
		WizRed = 54,
		WizWhite = 55,

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
		public static readonly Fedora Fedora = new Fedora();
		public static readonly HardHat HardHat = new HardHat();
		public static readonly RangerHat RangerHat = new RangerHat();
		public static readonly SpikeyHat SpikeyHat = new SpikeyHat();
		public static readonly TopHat TopHat = new TopHat();

		// Cosmetic Hats
		public static readonly CosmeticHat WizBlue = new CosmeticHat((byte)HatSubType.WizBlue);
		public static readonly CosmeticHat WizGreen = new CosmeticHat((byte)HatSubType.WizGreen);
		public static readonly CosmeticHat WizRed = new CosmeticHat((byte)HatSubType.WizRed);
		public static readonly CosmeticHat WizWhite = new CosmeticHat((byte)HatSubType.WizWhite);

		// Base Cosmetic Hats
		public static readonly CosmeticHat PooHat = new CosmeticHat((byte)HatSubType.PooHat);
		
		// Miscellaneous Cosmetic Hats
		public static readonly CosmeticHat BaseballHat = new CosmeticHat((byte)HatSubType.BaseballHat);
	}

	public class Hat {

		public static Dictionary<short, string> BaseTextures = new Dictionary<short, string>() {

			// Power Hats
			{ (byte) HatSubType.AngelHat, "AngelHat" },
			{ (byte) HatSubType.BambooHat, "BambooHat" },
			{ (byte) HatSubType.CowboyHat, "CowboyHat" },
			{ (byte) HatSubType.FeatheredHat, "FeatheredHat" },
			{ (byte) HatSubType.Fedora, "Fedora" },
			{ (byte) HatSubType.HardHat, "HardHat" },
			{ (byte) HatSubType.RangerHat, "RangerHat" },
			{ (byte) HatSubType.SpikeyHat, "SpikeyHat" },
			{ (byte) HatSubType.TopHat, "TopHat" },

			// Cosmetic Hats
			{ (byte) HatSubType.WizBlue, "WizBlue" },
			{ (byte) HatSubType.WizGreen, "WizGreen" },
			{ (byte) HatSubType.WizRed, "WizRed" },
			{ (byte) HatSubType.WizWhite, "WizWhite" }
		};

		protected HatRank hatRank;
		protected Atlas atlas;
		protected sbyte yOffset;        // The Y-Offset for placing the hat on the character's head.
		protected string SpriteName;	// Name of the base sprite to draw.
		public byte subType { get; protected set; }
		public string subStr { get; protected set; }

		public Hat( HatRank hatRank = HatRank.BaseHat ) {
			this.hatRank = hatRank;
			this.yOffset = -12;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Objects];
		}

		public static Hat GetHatBySubType(byte subType) {

			// Random Power Hat
			if(subType == (byte)HatSubType.RandomPowerHat) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(1, 9);
			}

			// Random Magic Hat
			else if(subType == (byte)HatSubType.RandomMagicHat) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next((byte)HatSubType.WizBlack, (byte)HatSubType.WizWhite);
			}

			switch(subType) {

				// Power Hats
				case (byte)HatSubType.AngelHat: return HatMap.AngelHat;
				case (byte)HatSubType.BambooHat: return HatMap.BambooHat;
				case (byte)HatSubType.CowboyHat: return HatMap.CowboyHat;
				case (byte)HatSubType.FeatheredHat: return HatMap.FeatheredHat;
				case (byte)HatSubType.Fedora: return HatMap.Fedora;
				case (byte)HatSubType.HardHat: return HatMap.HardHat;
				case (byte)HatSubType.RangerHat: return HatMap.RangerHat;
				case (byte)HatSubType.SpikeyHat: return HatMap.SpikeyHat;
				case (byte)HatSubType.TopHat: return HatMap.TopHat;

				// Cosmetic Hats
				case (byte)HatSubType.WizBlue: return HatMap.WizBlue;
				case (byte)HatSubType.WizGreen: return HatMap.WizGreen;
				case (byte)HatSubType.WizRed: return HatMap.WizRed;
				case (byte)HatSubType.WizWhite: return HatMap.WizWhite;

				// Base Cosmetic Hats
				case (byte)HatSubType.PooHat: return HatMap.PooHat;
			}

			return null;
		}

		public static void AssignToCharacter(Character character, byte subType, bool resetStats) {
			Hat hat = Hat.GetHatBySubType(subType);
			
			if(hat == null) {
				character.hat = null;
				return;
			}

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
			if(!removeCosmetic) {
				if(character.suit.DefaultCosmeticHat is Hat) { character.suit.DefaultCosmeticHat.ApplyHat(character, false); }
				else if(character.head.DefaultCosmeticHat is Hat) { character.head.DefaultCosmeticHat.ApplyHat(character, false); }
			}

			// Create Hat Particle
			EndBounceParticle.SetParticle(character.room, Systems.mapper.atlas[(byte)AtlasGroup.Objects], this.SpriteName + (character.FaceRight ? "" : "Left"), new Vector2(character.posX, character.posY + this.yOffset), Systems.timer.Frame + 40);

			if(resetStats) {
				character.stats.ResetCharacterStats();
			}
		}

		public bool IsBaseHat { get { return this.hatRank == HatRank.BaseHat; } }
		public bool IsCosmeticHat { get { return this.hatRank == HatRank.CosmeticHat || this.hatRank == HatRank.BaseHat; } }
		public bool IsPowerHat { get { return this.hatRank == HatRank.PowerHat; } }

		// Hats with powers may update the stats of the character that wears them.
		public virtual void UpdateCharacterStats(Character character) {}

		// posX and posY should be set to character.posX and .posY respectively.
		public void Draw(bool faceRight, int posX, int posY, int camX, int camY) {
			this.atlas.Draw(this.SpriteName + (faceRight ? "" : "Left"), posX - camX, posY + this.yOffset - camY);
		}
	}
}
