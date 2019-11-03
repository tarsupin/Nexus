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
		public bool CanFastCast;            // Doubles the rate of attacks.
		public bool ShellMastery;
		public bool SafeVsDamageAbove;		// Protects you against damage from above, such as falling projectiles or spikes.
		public bool InflictDamageAbove;		// Allows you to Inflict Damage above, when jumping.

		// Run Speed
		public FInt RunAcceleration;
		public FInt RunDeceleration;
		public byte RunMaxSpeed;
		public FInt SlowSpeedMult;          // The multiplier for when you're not running (running is considered x1).

		// Jumping & Air Movement
		public FInt AirAcceleration;
		public FInt AirDeceleration;
		public byte JumpMaxTimes;
		public byte JumpDuration;
		public byte JumpStrength;

		// Wall Jumping
		public byte WallJumpDuration;
		public byte WallJumpXStrength;
		public byte WallJumpYStrength;

		// Sliding
		public byte SlideWaitDuration;		// # of ticks the character must wait to continue sliding.
		public byte SlideDuration;
		public FInt SlideStrength;

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
			ShellMastery = false;
			SafeVsDamageAbove = false;
			InflictDamageAbove = false;

			RunAcceleration = FInt.Create(0.3);
			RunDeceleration = 0 - FInt.Create(0.2);
			RunMaxSpeed = 7;
			SlowSpeedMult = FInt.Create(0.65);

			JumpMaxTimes = 1;
			AirAcceleration = FInt.Create(0.45);
			AirDeceleration = 0 - FInt.Create(0.2);
			JumpDuration = 10;
			JumpStrength = 11;

			WallJumpDuration = 8;
			WallJumpXStrength = 8;
			WallJumpYStrength = 8;

			SlideWaitDuration = 36;
			SlideDuration = 12;
			SlideStrength = FInt.Create(12);

			BaseGravity = 0 - FInt.Create(0.7);

			// Reset Wound Settings
			if(this.character.wounds is CharacterWounds) {
				this.character.wounds.ResetWoundSettings();
			}

			// Update Suit Abilities (if applicable)
			if(character.suit is Suit) {
				character.suit.UpdateCharacterStats(character);
			}

			// Update Hat Abilities (if applicable)
			if(character.hat is Hat) {
				character.hat.UpdateCharacterStats(character);
			}

			// TODO: Do below
			// Update Stats by Cheats
			// Update Stats by Game Mode
			// Update Stats by Character Archetype
		}
	}
}
