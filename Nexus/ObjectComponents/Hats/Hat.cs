using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;

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
		RandomHat = 0,

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

		// Cosmetic Hats
		WizardBlueHat = 51,
		WizardGreenHat = 52,
		WizardRedHat = 53,
		WizardWhiteHat = 54,
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
		public static readonly WizardBlueHat WizardBlueHat = new WizardBlueHat();
		public static readonly WizardGreenHat WizardGreenHat = new WizardGreenHat();
		public static readonly WizardRedHat WizardRedHat = new WizardRedHat();
		public static readonly WizardWhiteHat WizardWhiteHat = new WizardWhiteHat();
	}

	public class Hat {

		protected HatRank hatRank;
		protected Atlas atlas;
		protected sbyte yOffset;        // The Y-Offset for placing the hat on the character's head.
		protected string SpriteName;	// Name of the base sprite to draw.

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
				case (byte)HatSubType.WizardBlueHat: return HatMap.WizardBlueHat;
				case (byte)HatSubType.WizardGreenHat: return HatMap.WizardGreenHat;
				case (byte)HatSubType.WizardRedHat: return HatMap.WizardRedHat;
				case (byte)HatSubType.WizardWhiteHat: return HatMap.WizardWhiteHat;
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

		public virtual void DestroyHat(Character character, bool resetStats) {
			character.hat = null;

			// Reset Default Hat to Suit's Cosmetic Version (if applicable)
			if(character.suit.DefaultCosmeticHat is Hat) {
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
