using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;

namespace Nexus.ObjectComponents {

	public enum HeadSubType : byte {
		None = 0,

		// Standard Heads
		RyuHead = 5,
		PooHead = 6,
		CarlHead = 7,

		// Random Heads
		RandomHead = 90,
		RandomStandard = 91,
	}

	public static class HeadMap {
		public static readonly RyuHead RyuHead = new RyuHead();
		public static readonly PooHead PooHead = new PooHead();
		public static readonly CarlHead CarlHead = new CarlHead();
	}

	public class Head {

		protected readonly Atlas atlas;
		protected readonly string SpriteName;		// Name of the base sprite to draw.
		public readonly Hat DefaultCosmeticHat;		// A default, Cosmetic Hat associated with the Head (such as Wizard Hats for Wizards).

		public Head( string headName, Hat defaultCosmeticHat = null) {
			this.SpriteName = "Head/" + headName + "/";
			this.DefaultCosmeticHat = defaultCosmeticHat;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Objects];
		}

		public static Head GetHeadBySubType(byte subType) {

			// Random Head
			if(subType == (byte)HeadSubType.RandomHead) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(5, 7);
			} else if(subType == (byte)HeadSubType.RandomStandard) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(5, 7);
			}

			switch(subType) {

				// Standard Heads
				case (byte)HeadSubType.RyuHead: return HeadMap.RyuHead;
				case (byte)HeadSubType.PooHead: return HeadMap.PooHead;
				case (byte)HeadSubType.CarlHead: return HeadMap.CarlHead;
			}

			return HeadMap.RyuHead;
		}

		public static void AssignToCharacter(Character character, byte subType, bool resetStats) {
			Head head = Head.GetHeadBySubType(subType);
			head.ApplyHead(character, resetStats);
		}

		public void ApplyHead(Character character, bool resetStats) {
			character.head = this; // Apply Head to Character

			// Apply the Head's Default Hat if the character has no hat OR a cosmetic hat.
			if(character.hat == null || (character.hat is Hat && character.hat.IsCosmeticHat)) {
				this.AssignHeadDefaultHat(character);
			}

			// Reset Character Stats
			if(resetStats && character.stats != null) { character.stats.ResetCharacterStats(); }
		}

		// Some Heads come with default hats.
		public void AssignHeadDefaultHat(Character character) {
			if(this.DefaultCosmeticHat != null) {
				this.DefaultCosmeticHat.ApplyHat(character, false);
			}
		}

		public void Draw(bool faceRight, int posX, int posY, int camX, int camY) {
			this.atlas.Draw(this.SpriteName + (faceRight ? "Right" : "Left"), posX - camX, posY - camY);
		}
	}
}
