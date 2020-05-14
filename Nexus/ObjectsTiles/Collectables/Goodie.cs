using System;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	class Goodie : Collectable {

		public enum GoodieSubType : byte {
			Apple = 0,
			Pear = 1,
			Heart = 2,

			Shield = 3,
			ShieldPlus = 4,
			
			Guard = 5,
			GuardPlus = 6,
			
			Shiny = 7,
			Stars = 8,
			GodMode = 9,
			
			Plus5 = 10,
			Plus10 = 11,
			Plus20 = 12,
			Set5 = 13,
			Set10 = 14,
			Set20 = 15,
			
			Disrupt = 16,
			Explosive = 17,
			Key = 18,
			Blood = 19,
		}

		public Goodie() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.Goodie;
			this.paramSets = new Params[1] { Params.ParamMap["Collectable"] };

			// Helper Texts
			this.titles = new string[20];
			this.titles[(byte)GoodieSubType.Apple] = "Apple";
			this.titles[(byte)GoodieSubType.Pear] = "Pear";
			this.titles[(byte)GoodieSubType.Heart] = "Heart Potion";
			this.titles[(byte)GoodieSubType.Shield] = "Shield";
			this.titles[(byte)GoodieSubType.ShieldPlus] = "Large Shield";

			this.titles[(byte)GoodieSubType.Guard] = "Amulet";
			this.titles[(byte)GoodieSubType.GuardPlus] = "Power Amulet";

			this.titles[(byte)GoodieSubType.Shiny] = "Shiny Potion";
			this.titles[(byte)GoodieSubType.Stars] = "Star Potion";
			this.titles[(byte)GoodieSubType.GodMode] = "God Mode Collectable";

			this.titles[(byte)GoodieSubType.Plus5] = "+5 Timer";
			this.titles[(byte)GoodieSubType.Plus10] = "+10 Timer";
			this.titles[(byte)GoodieSubType.Plus20] = "+20 Timer";
			this.titles[(byte)GoodieSubType.Set5] = "5 Second Countdown";
			this.titles[(byte)GoodieSubType.Set10] = "10 Second Countdown";
			this.titles[(byte)GoodieSubType.Set20] = "20 Second Countdown";

			this.titles[(byte)GoodieSubType.Disrupt] = "Disrupt Potion";
			this.titles[(byte)GoodieSubType.Explosive] = "Explosive Potion";
			this.titles[(byte)GoodieSubType.Key] = "Key";
			this.titles[(byte)GoodieSubType.Blood] = "Blood Potion";

			this.descriptions = new string[20];
			this.descriptions[(byte)GoodieSubType.Apple] = "Grants +1 Health";
			this.descriptions[(byte)GoodieSubType.Pear] = "Grants +1 Health";
			this.descriptions[(byte)GoodieSubType.Heart] = "Grants Full Health";
			this.descriptions[(byte)GoodieSubType.Shield] = "Grants +1 Shield";
			this.descriptions[(byte)GoodieSubType.ShieldPlus] = "Grants Full Shields";

			this.descriptions[(byte)GoodieSubType.Guard] = "Grants a minor shield attachment that protects the character.";
			this.descriptions[(byte)GoodieSubType.GuardPlus] = "Grants a major shield attachment that protects the character.";

			this.descriptions[(byte)GoodieSubType.Shiny] = "Makes the character invulnerable for ten seconds.";
			this.descriptions[(byte)GoodieSubType.Stars] = "Makes the character invulnerable and deadly on contact for ten seconds.";
			this.descriptions[(byte)GoodieSubType.GodMode] = "Makes the character invulnerable.";

			this.descriptions[(byte)GoodieSubType.Plus5] = "Adds +5 seconds to the current timer.";
			this.descriptions[(byte)GoodieSubType.Plus10] = "Adds +10 seconds to the current timer.";
			this.descriptions[(byte)GoodieSubType.Plus20] = "Adds +20 seconds to the current timer.";
			this.descriptions[(byte)GoodieSubType.Set5] = "Sets the timer to exactly five seconds.";
			this.descriptions[(byte)GoodieSubType.Set10] = "Sets the timer to exactly ten seconds.";
			this.descriptions[(byte)GoodieSubType.Set20] = "Sets the timer to exactly twenty seconds.";

			this.descriptions[(byte)GoodieSubType.Disrupt] = "If touched, removes all the character's equipment and powers.";
			this.descriptions[(byte)GoodieSubType.Explosive] = "Damages all enemies on the screen.";
			this.descriptions[(byte)GoodieSubType.Key] = "Used to unlock things including doors, chests, etc.";
			this.descriptions[(byte)GoodieSubType.Blood] = "Unknown.";
		}

		public override void Collect(RoomScene room, Character character, ushort gridX, ushort gridY) {
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);

			switch(subType) {

				// Health
				case (byte)GoodieSubType.Apple: this.GetHealth(character, 1); break;
				case (byte)GoodieSubType.Pear: this.GetHealth(character, 1); break;
				case (byte)GoodieSubType.Heart: this.GetHealth(character, 3); break;

				// Armor
				case (byte)GoodieSubType.Shield: this.GetArmor(character, 1); break;
				case (byte)GoodieSubType.ShieldPlus: this.GetArmor(character, 3); break;

				// Guard Shield
				case (byte)GoodieSubType.Guard: this.GetGuardShield(character, 5); break;
				case (byte)GoodieSubType.GuardPlus: this.GetGuardShield(character, 8); break;

				// Invincibility
				case (byte)GoodieSubType.Shiny: this.GetInvincible(character, 10000); break;
				case (byte)GoodieSubType.Stars: this.GetInvincible(character, 10000); break;
				case (byte)GoodieSubType.GodMode: this.GetInvincible(character, 99999999); break;

				// Timer
				case (byte)GoodieSubType.Plus5: this.SetTime(character, true, 5); break;
				case (byte)GoodieSubType.Plus10: this.SetTime(character, true, 10); break;
				case (byte)GoodieSubType.Plus20: this.SetTime(character, true, 20); break;
				case (byte)GoodieSubType.Set5: this.SetTime(character, false, 5); break;
				case (byte)GoodieSubType.Set10: this.SetTime(character, false, 10); break;
				case (byte)GoodieSubType.Set20: this.SetTime(character, false, 20); break;

				// Disrupt
				case (byte)GoodieSubType.Disrupt: this.RunDisrupt(character); break;

				// Explosive
				case (byte)GoodieSubType.Explosive: this.RunTNTDetonation(character); break;

				// Key
				case (byte)GoodieSubType.Key: this.CollectKey(character); break;
			}

			base.Collect(room, character, gridX, gridY);
		}

		private void GetHealth(Character character, byte health) {
			character.wounds.AddHealth(health);

			// Apply Sound
			if(health == 1) {
				Systems.sounds.food.Play();
			} else {
				Systems.sounds.potion.Play();
			}
		}

		private void GetArmor(Character character, byte armor) {
			character.wounds.AddArmor(armor);
			Systems.sounds.collectSubtle.Play();
		}

		private void GetGuardShield(Character character, byte balls) {
			Systems.sounds.shield.Play();
			throw new NotImplementedException();
		}

		private void GetInvincible(Character character, uint frames) {
			character.wounds.SetInvincible(frames);
			Systems.sounds.collectSubtle.Play();
		}

		private void SetTime(Character character, bool isAdditive, byte timeVal) {
			LevelState levelState = Systems.handler.levelState;

			// If the Collectable ADDS to the timer, rather than SETS.
			if(isAdditive) {

				// Update the Timer
				uint origFramesLeft = levelState.FramesRemaining;
				levelState.timeShift = 0;
				uint trueTimeLeft = levelState.FramesRemaining;
				levelState.timeShift = (int) -trueTimeLeft + (timeVal * 60);

				// Sound of collectable is based on whether it was positive or negative.
				if(origFramesLeft > levelState.FramesRemaining) {
					Systems.sounds.collectDisable.Play();
				} else {
					Systems.sounds.timer2.Play();
				}
			}

			// If the Collectable SETS the timer, rather than ADDS.
			else {
				levelState.timeShift += timeVal * 60;
				Systems.sounds.timer2.Play();
			}
		}

		private void RunDisrupt(Character character) {
			character.DisableAbilities();
			Systems.sounds.collectDisable.Play();
		}

		private void RunTNTDetonation(Character character) {
			throw new NotImplementedException();
		}

		private void CollectKey(Character character) {
			Systems.sounds.collectKey.Play();
			throw new NotImplementedException();
		}

		private void CreateTextures() {
			this.Texture = new string[20];
			
			this.Texture[(byte) GoodieSubType.Apple] = "Goodie/Apple";
			this.Texture[(byte) GoodieSubType.Pear] = "Goodie/Pear";
			this.Texture[(byte) GoodieSubType.Heart] = "Goodie/Heart";
			this.Texture[(byte) GoodieSubType.Shield] = "Goodie/Shield";
			this.Texture[(byte) GoodieSubType.ShieldPlus] = "Goodie/ShieldPlus";

			this.Texture[(byte) GoodieSubType.Guard] = "Goodie/Guard";
			this.Texture[(byte) GoodieSubType.GuardPlus] = "Goodie/GuardPlus";

			this.Texture[(byte) GoodieSubType.Shiny] = "Goodie/Shiny";
			this.Texture[(byte) GoodieSubType.Stars] = "Goodie/Stars";
			this.Texture[(byte) GoodieSubType.GodMode] = "Goodie/GodMode";

			this.Texture[(byte) GoodieSubType.Plus5] = "Timer/Plus5";
			this.Texture[(byte) GoodieSubType.Plus10] = "Timer/Plus10";
			this.Texture[(byte) GoodieSubType.Plus20] = "Timer/Plus20";
			this.Texture[(byte) GoodieSubType.Set5] = "Timer/Set5";
			this.Texture[(byte) GoodieSubType.Set10] = "Timer/Set10";
			this.Texture[(byte) GoodieSubType.Set20] = "Timer/Set20";

			this.Texture[(byte) GoodieSubType.Disrupt] = "Goodie/Disrupt";
			this.Texture[(byte) GoodieSubType.Explosive] = "Goodie/Explosive";
			this.Texture[(byte) GoodieSubType.Key] = "Goodie/Key";
			this.Texture[(byte) GoodieSubType.Blood] = "Goodie/Blood";
		}
	}
}
