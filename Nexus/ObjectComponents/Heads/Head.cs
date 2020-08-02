﻿using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;

namespace Nexus.ObjectComponents {

	public enum HeadSubType : byte {
		None = 0,

		// Generic Heads
		LanaHead = 1,

		// Assigned Heads
		RyuHead = 5,
		PooHead = 6,
		CarlHead = 7,
		KirbsHead = 8,
		PandaHead = 9,

		// Fun Heads
		NeoHead = 50,

		// Random Heads
		RandomHead = 90,
		RandomStandard = 91,
	}

	public static class HeadMap {
		public static readonly LanaHead LanaHead = new LanaHead();
		public static readonly RyuHead RyuHead = new RyuHead();
		public static readonly PooHead PooHead = new PooHead();
		public static readonly CarlHead CarlHead = new CarlHead();
		public static readonly KirbsHead KirbsHead = new KirbsHead();
		public static readonly PandaHead PandaHead = new PandaHead();
		public static readonly NeoHead NeoHead = new NeoHead();
	}

	public class Head {

		protected readonly Atlas atlas;
		public readonly string SpriteName;       // Name of the base sprite to draw.
		public string subStr { get; protected set; }
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

				// Generic Heads
				case (byte)HeadSubType.LanaHead: return HeadMap.LanaHead;

				// Assigned Heads
				case (byte)HeadSubType.RyuHead: return HeadMap.RyuHead;
				case (byte)HeadSubType.PooHead: return HeadMap.PooHead;
				case (byte)HeadSubType.CarlHead: return HeadMap.CarlHead;
			}

			return HeadMap.LanaHead;
		}

		public static void AssignToCharacter(Character character, byte subType, bool resetStats) {
			Head head = Head.GetHeadBySubType(subType);
			head.ApplyHead(character, resetStats);
		}

		public void ApplyHead(Character character, bool resetStats) {
			character.head = this; // Apply Head to Character

			// Remove cosmetic hats for the new head, but then apply the Head's Default Hat if the character has no hat OR a cosmetic hat.
			if(character.hat == null || (character.hat is Hat && character.hat.IsCosmeticHat)) {
				character.hat = null;
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
