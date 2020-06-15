using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;

namespace Nexus.ObjectComponents {

	public class CharacterWounds {

		// References
		private readonly Character character;
		private readonly TimerGlobal timer;

		// Wound Settings
		public byte InvincibleDuration;     // The # of ticks after getting damaged the character is invicible.
		public byte WoundMaximum;           // The max number of wound levels (health + armor) a character can possess.
		public byte ArmorMaximum;           // The max number of armor a character can possess.
		public byte HealthAfterDeath;       // The number of health restored after a death.
		public byte ArmorAfterDeath;        // The number of armor restored after a death.

		// Wound Values
		protected uint Invincible;					// If >= time.frame, the character is invincible to standard damage.
		public byte Armor { get; protected set; }	// Each health soaks one damage, only after equipment is lost.
		public byte Health { get; protected set; }	// Each armor soaks one damage, prior to equipment loss (thus preventing Suit and Hat loss).

		public CharacterWounds(Character character, TimerGlobal timer) {
			this.character = character;
			this.timer = timer;
			this.ResetWoundSettings();
		}

		public bool IsInvincible {
			get { return this.Invincible >= this.timer.Frame; }
		}

		public void ResetWoundSettings() {
			InvincibleDuration = 60;
			WoundMaximum = 3;
			ArmorMaximum = 3;
			HealthAfterDeath = 0;
			ArmorAfterDeath = 0;

			// TODO LOW PRIORITY: Add wound settings from: Suit, Hat, Cheats, Game Mode, Character Archetype, etc...
		}

		public void WoundsDeathReset() {
			this.Invincible = this.timer.Frame + this.InvincibleDuration;
			this.Armor = ArmorAfterDeath;
			this.Health = HealthAfterDeath;
		}

		public void AddHealth( byte health ) {
			this.Health += health;
			if(this.Health > this.WoundMaximum - this.Armor) { this.Health = (byte) (this.WoundMaximum - this.Armor); }
		}

		public void AddArmor( byte armor ) {
			byte allowed = (byte) Math.Min(this.Armor + armor, this.ArmorMaximum);
			this.Armor = allowed;
			if(this.Health > this.WoundMaximum - this.Armor) { this.Health = (byte) (this.WoundMaximum - this.Armor); }
		}
		
		public void SetHealth( byte health ) {
			this.Health = health;
			if(this.Health > this.WoundMaximum - this.Armor) { this.Health = (byte) (this.WoundMaximum - this.Armor); }
		}

		public void SetArmor( byte armor ) {
			byte allowed = (byte) Math.Min(armor, this.ArmorMaximum);
			this.Armor = allowed;
			if(this.Health > this.WoundMaximum - this.Armor) { this.Health = (byte) (this.WoundMaximum - this.Armor); }
		}

		public void SetInvincible( uint duration ) {
			this.Invincible = this.timer.Frame + duration;
		}

		public bool ReceiveWoundDamage( DamageStrength damageStrength ) {

			// If insufficient damage was created.
			if(damageStrength < DamageStrength.Trivial) { return false; }

			// If the Character took Instant-Kill Wounds:
			if((byte) damageStrength > (byte) DamageStrength.Lethal) { return this.Death(); }

			// If the Character is Invincible, no damage taken.
			if(this.IsInvincible) { return false; }

			// Wound Sound
			Systems.sounds.wound.Play();

			// Damage will be soaked by Armor, if available. Occurs before damaging equipment (e.g. Suit, Hat, etc.)
			if(this.Armor > 0) {
				this.Armor--;
				this.SetInvincible(this.InvincibleDuration);
				return true;
			}

			// Damage will be soaked by destroying Hat, if one is available.
			if(this.character.hat is Hat && this.character.hat.IsPowerHat) {
				this.SetInvincible(this.InvincibleDuration);
				this.character.hat.DestroyHat(this.character, true);
				return true;
			}

			// Damage will be soaked by destroying Suit, if one is available.
			if(this.character.suit is Suit && this.character.suit.IsPowerSuit) {
				this.SetInvincible(this.InvincibleDuration);
				this.character.suit.DestroySuit(this.character, true);
				return true;
			}

			// Damage will be soaked by Health, if available. Last attempt to soak damage.
			if(this.Health > 0) {
				this.Health--;
				this.SetInvincible(this.InvincibleDuration);
				return true;
			}

			// Damage could not be soaked. The character dies.
			return this.Death();
		}

		// The Character has died. Run all death functions.
		public bool Death() {
			LevelState levelState = Systems.handler.levelState;

			// Check if there is a Retry Flag available. If so, use that.
			if(levelState.retryFlag.active) {
				levelState.retryFlag.active = false;
				this.character.physics.MoveToPos(levelState.retryFlag.gridX * (byte)TilemapEnum.TileWidth, levelState.retryFlag.gridY * (byte)TilemapEnum.TileHeight);
				return false;
			}

			// Reset Mechanics, Status, etc.
			this.character.DisableAbilities();      // Lose all Equipment and Powers
			this.character.ResetCharacter();		// Reset Character Stats, Status, Attachments, Items, etc.

			this.Invincible = 0;

			// Nullify Action, if applicable.
			if(this.character.status.action is Action) { this.character.status.action = null; }

			// Play Death Sound
			Systems.sounds.crack.Play();

			// Assign the character's death frame. This will affect things different in Single Player and Multiplayer.
			this.character.deathFrame = Systems.timer.Frame;

			return true;
		}
	}
}
