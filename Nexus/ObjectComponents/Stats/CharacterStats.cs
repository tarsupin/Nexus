using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class CharacterStats {

		// References
		private Character character;

		// Abilities
		public bool CanWallJump;
		public bool CanWallGrab;
		public bool CanWallSlide;
		public bool CanFastCast;			// Doubles the rate of attacks.

		// Run Speed
		public FInt RunAcceleration;
		public FInt RunDeceleration;
		public byte RunMaxSpeed;

		// Jumping
		public byte JumpMaxTimes;
		public FInt JumpAcceleration;
		public FInt JumpDeceleration;
		public byte JumpDuration;
		public byte JumpStrength;

		// Wall Jumping
		public byte WallJumpDuration;
		public byte WallJumpXStrength;
		public byte WallJumpYStrength;

		// Sliding
		public byte SlideWaitDurations;		// # of ticks the character must wait to continue sliding.
		public byte SlideDuration;
		public byte SlideStrength;

		// General Stats
		public FInt BaseGravity;            // The gravity that a character will reset to after landing.

		public CharacterStats( Character character ) {
			this.character = character;
			this.ResetCharacterStats();
		}

		public void ResetCharacterStats() {

			// Reset Base Stats
			CanWallJump = false;
			CanWallGrab = false;
			CanWallSlide = false;
			CanFastCast = false;

			RunAcceleration = FInt.FromParts(0, 300);
			RunDeceleration = 0 - FInt.FromParts(0, 200);
			RunMaxSpeed = 7;

			JumpMaxTimes = 1;
			JumpAcceleration = FInt.FromParts(0, 450);
			JumpDeceleration = 0 - FInt.FromParts(0, 200);
			JumpDuration = 10;
			JumpStrength = 11;

			WallJumpDuration = 8;
			WallJumpXStrength = 8;
			WallJumpYStrength = 8;

			SlideWaitDurations = 36;
			SlideDuration = 12;
			SlideStrength = 12;

			BaseGravity = 0 - FInt.FromParts(0, 700);

			// TODO HIGH PRIORITY: Update Character Stats with Suit and Hat
			//// Update Suit Abilities (if applicable)
			//if(character.Suit is Suit) {
			//	character.Suit.UpdateCharacterStats();
			//}

			//// Update Hat Abilities (if applicable)
			//if(character.Hat is Hat) {
			//	character.Hat.UpdateCharacterStats();
			//}


			// TODO: Do below
			// Update Stats by Cheats

			// Update Stats by Game Mode

			// Update Stats by Character Archetype
		}
	}
}
