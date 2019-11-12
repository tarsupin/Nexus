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
		protected sbyte yOffset;        // The Y-Offset for placing the hat on the character's head.
		protected string SpriteName;	// Name of the sprite to draw.

		public Hat( HatRank hatRank = HatRank.BaseHat ) {
			this.hatRank = hatRank;
			this.yOffset = -12;
		}

		public static void AssignToCharacter(Character character, byte subType, bool resetStats) {

			// Random Hat
			if(subType == (byte) HatSubType.RandomHat) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(1, 9);
			}

			switch(subType) {

				// Power Hats
				case (byte) HatSubType.AngelHat: HatMap.AngelHat.ApplyHat(character, resetStats); break;
				case (byte) HatSubType.BambooHat: HatMap.BambooHat.ApplyHat(character, resetStats); break;
				case (byte) HatSubType.CowboyHat: HatMap.CowboyHat.ApplyHat(character, resetStats); break;
				case (byte) HatSubType.FeatheredHat: HatMap.FeatheredHat.ApplyHat(character, resetStats); break;
				case (byte) HatSubType.FedoraHat: HatMap.FedoraHat.ApplyHat(character, resetStats); break;
				case (byte) HatSubType.HardHat: HatMap.HardHat.ApplyHat(character, resetStats); break;
				case (byte) HatSubType.RangerHat: HatMap.RangerHat.ApplyHat(character, resetStats); break;
				case (byte) HatSubType.SpikeyHat: HatMap.SpikeyHat.ApplyHat(character, resetStats); break;
				case (byte) HatSubType.TopHat: HatMap.TopHat.ApplyHat(character, resetStats); break;

				// Cosmetic Hats
				case (byte) HatSubType.WizardBlueHat: HatMap.WizardBlueHat.ApplyHat(character, resetStats); break;
				case (byte) HatSubType.WizardGreenHat: HatMap.WizardGreenHat.ApplyHat(character, resetStats); break;
				case (byte) HatSubType.WizardRedHat: HatMap.WizardRedHat.ApplyHat(character, resetStats); break;
				case (byte) HatSubType.WizardWhiteHat: HatMap.WizardWhiteHat.ApplyHat(character, resetStats); break;
			}
		}

		public void ApplyHat(Character character, bool resetStats) {
			character.hat = this; // Apply Hat to Character

			// Reset Character Stats
			if(resetStats && character.stats != null) { character.stats.ResetCharacterStats(); }
		}

		public virtual void DestroyHat(Character character, bool resetStats) {

			// Reset Default Hat to Suit's Cosmetic Version (if applicable)
			if(character.suit.DefaultCosmeticHat is Hat) {
				character.suit.DefaultCosmeticHat.ApplyHat(character, false);
			}
		}

		public bool IsCosmeticHat { get { return this.hatRank == HatRank.CosmeticHat; } }
		public bool IsPowerHat { get { return this.hatRank == HatRank.PowerHat; } }

		public void Draw(Character character, int camX, int camY) {
			Systems.mapper.atlas[(byte) AtlasGroup.Objects].Draw(this.SpriteName + (character.FaceRight ? "" : "Left"), character.posX - camX, character.posY + this.yOffset - camY);
		}

		// Hats with powers may update the stats of the character that wears them.
		public virtual void UpdateCharacterStats(Character character) {}
	}
}
