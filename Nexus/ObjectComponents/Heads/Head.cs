using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Head {

		protected readonly string SpriteName;   // Name of the sprite to draw.
		public readonly Hat DefaultCosmeticHat; // A default, Cosmetic Hat associated with the Head (such as Wizard Hats for Wizards).

		public Head( string headName, Hat defaultCosmeticHat = null) {
			this.SpriteName = "Head/" + headName + "/";
			this.DefaultCosmeticHat = defaultCosmeticHat;
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

		public void Draw(Character character, int camX, int camY) {
			Systems.mapper.atlas[(byte) AtlasGroup.Objects].Draw(this.SpriteName + (character.FaceRight ? "Right" : "Left"), character.posX - camX, character.posY - camY);
		}
	}
}
