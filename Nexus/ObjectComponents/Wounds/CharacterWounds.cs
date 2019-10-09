using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;

namespace Nexus.ObjectComponents {

	public class CharacterWounds {

		// References
		private Character character;
		private TimerGlobal timer;

		// Wound Settings
		public byte InvincibleDuration;     // The # of ticks after getting damaged the character is invicible.
		public byte WoundMaximum;           // The max number of wound levels (health + armor) a character can possess.
		public byte ArmorMaximum;           // The max number of armor a character can possess.
		public byte HealthAfterDeath;       // The number of health restored after a death.
		public byte ArmorAfterDeath;        // The number of armor restored after a death.

		// Wound Values
		protected uint Invincible;             // If >= time.frame, the character is invincible to standard damage.
		protected byte Armor;                  // Each health soaks one damage, only after equipment is lost.
		protected byte Health;                 // Each armor soaks one damage, prior to equipment loss (thus preventing Suit and Hat loss).

		public CharacterWounds(Character character, TimerGlobal timer) {
			this.character = character;
			this.timer = timer;
			this.ResetWoundSettings();
		}

		public bool IsInvincible {
			get { return this.Invincible >= this.timer.frame; }
		}

		public void ResetWoundSettings() {
			InvincibleDuration = 60;
			WoundMaximum = 3;
			ArmorMaximum = 3;
			HealthAfterDeath = 0;
			ArmorAfterDeath = 0;

			// TODO LOW PRIORITY: Update Character Wounds with: Suit, Hat, Cheats, Game Mode, Character Archetype, etc...

			this.WoundsDeathReset();
		}

		public void WoundsDeathReset() {
			this.Invincible = this.timer.frame + this.InvincibleDuration;
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

		public void SetInvincible( uint duration ) {
			this.Invincible = this.timer.frame + duration;
		}

		public bool ReceiveWoundDamage( DamageStrength damageStrength ) {

			// If the Character took Instant-Kill Wounds:
			if((byte) damageStrength > (byte) DamageStrength.Lethal) { return this.Death(); }

			// If the Character is Invincible, no damage taken.
			if(this.IsInvincible) { return false; }

			// TODO SOUND: this.scene.soundList.wound.play();

			// Damage will be soaked by Armor, if available. Occurs before damaging equipment (e.g. Suit, Hat, etc.)
			if(this.Armor > 0) {
				this.Armor--;
				// TODO UI: Update ARMOR UI
				// this.scene.healthIcons.updateIcons( this.status.health, this.status.armor );
				this.SetInvincible(this.InvincibleDuration);
				return true;
			}

			// Damage will be soaked by destroying Hat, if one is available.
			// TODO HIGH PRIORITY: Character Hat, Protective
			//if(this.character.Hat && this.Hat.IsProtective) {
			//	this.SetInvincible(this.InvincibleDuration);
			//  this.character.Hat.DestroyHat();
			//	return true;
			//}

			// Damage will be soaked by destroying Suit, if one is available.
			// TODO HIGH PRIORITY: Character Suit, Protective
			//if(this.character.Suit && this.Suit.IsProtective) {
			//	this.SetInvincible(this.InvincibleDuration);
			//	this.character.Suit.DestroySuit();
			//	return true;
			//}

			// Damage will be soaked by Health, if available. Last attempt to soak damage.
			if(this.Health > 0) {
				this.Health--;
				// TODO UI
				// this.scene.healthIcons.updateIcons( this.status.health, this.status.armor );
				this.SetInvincible(this.InvincibleDuration);
				return true;
			}

			// Damage could not be soaked. The character dies.
			return this.Death();
		}

		// The Character has died. Run all death functions.
		public bool Death() {
			LevelState levelState = this.character.scene.systems.handler.levelState;

			// Check if there is a Retry Flag available. If so, use that.
			if(levelState.retryFlag.active) {
				levelState.retryFlag.active = false;
				this.character.physics.MoveToPos(FVector.Create(levelState.retryFlag.gridX * (byte)TilemapEnum.TileWidth, levelState.retryFlag.gridY * (byte)TilemapEnum.TileHeight));
				return false;
			}

			// Reset Mechanics, Status, etc.
			this.character.DisableAbilities();      // Lose all Equipment and Powers
			this.character.ResetCharacter();		// Reset Character Stats, Status, Attachments, Items, etc.

			if(this.Invincible > 0) { this.Invincible = 0; }

			// TODO HIGH PRIORITY: Add Character Actions
			//if(this.action is Action) { this.action = null; }

			// TODO SOUND: Play Death Sound
			// this.scene.soundList.crack.play();

			// Run Level Death
			levelState.Die();
			this.character.scene.RunCharacterDeath( this.character );

			return true;
		}
	}
}
