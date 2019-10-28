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
		
		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.CollectableGoodie)) {
				new Goodie(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.CollectableGoodie, subTypeId);
		}

		public Goodie(LevelScene scene) : base(scene, TileGameObjectId.CollectableGoodie) {
			this.CreateTextures();
		}

		public override void Collect(Character character, uint gridId) {
			byte subType = this.scene.tilemap.GetSubTypeAtGridID(gridId);

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
				case (byte)GoodieSubType.Plus5: this.GetTime(character, true, 5); break;
				case (byte)GoodieSubType.Plus10: this.GetTime(character, true, 10); break;
				case (byte)GoodieSubType.Plus20: this.GetTime(character, true, 20); break;
				case (byte)GoodieSubType.Set5: this.GetTime(character, false, 5); break;
				case (byte)GoodieSubType.Set10: this.GetTime(character, false, 10); break;
				case (byte)GoodieSubType.Set20: this.GetTime(character, false, 20); break;

				// Disrupt
				case (byte)GoodieSubType.Disrupt: this.RunDisrupt(character); break;

				// Explosive
				case (byte)GoodieSubType.Explosive: this.RunTNTDetonation(character); break;

				// Key
				case (byte)GoodieSubType.Key: this.CollectKey(character); break;
			}

			base.Collect(character, gridId);
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

		private void GetTime(Character character, bool isAdditive, byte timeVal) {
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

			this.Texture[(byte) GoodieSubType.Plus5] = "Goodie/Plus5";
			this.Texture[(byte) GoodieSubType.Plus10] = "Goodie/Plus10";
			this.Texture[(byte) GoodieSubType.Plus20] = "Goodie/Plus20";
			this.Texture[(byte) GoodieSubType.Set5] = "Goodie/Set5";
			this.Texture[(byte) GoodieSubType.Set10] = "Goodie/Set10";
			this.Texture[(byte) GoodieSubType.Set20] = "Goodie/Set20";

			this.Texture[(byte) GoodieSubType.Disrupt] = "Goodie/Disrupt";
			this.Texture[(byte) GoodieSubType.Explosive] = "Goodie/Explosive";
			this.Texture[(byte) GoodieSubType.Key] = "Goodie/Key";
			this.Texture[(byte) GoodieSubType.Blood] = "Goodie/Blood";
		}
	}
}
