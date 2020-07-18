﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	class Goodie : Collectable {

		public enum GoodieSubType : byte {

			// Health
			Apple = 0,
			Pear = 1,
			Pack1 = 2,

			// Shields
			ShieldWood = 3,
			ShieldBlue = 4,
			ShieldWhite = 23,

			// Invincibility
			Shiny = 7,
			Stars = 8,
			GodMode = 9,
			
			// Timers
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

			Melon = 20,
			Soup = 21,
			Pack2 = 22,

			// Jewelry
			RingMagic = 5,      // Magi
			AmuletMagic = 6,    // Magi 2

			RingFire = 24,			// Fire
			NeckFire = 25,			// Fire
			RingPoison = 26,		// Poison
			NeckElectric = 27,		// Fire/Frost
			RingElements = 28,		// Fire/Frost
			NeckHeart = 29,			// Reserved
			RingHawk = 30,          // Reserved
			RingDruid = 31,         // Reserved
			RingEye = 32,           // Reserved

			// Shoes
			DashingShoe = 33,
			SpikeShoe = 34,
			WingShoe = 35,
		}

		public Goodie() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.Goodie;
			this.moveParamSet = Params.ParamMap["Collectable"];

			// Helper Texts
			this.titles = new string[36];

			// Health
			this.titles[(byte)GoodieSubType.Apple] = "Apple";
			this.titles[(byte)GoodieSubType.Pear] = "Pear";
			this.titles[(byte)GoodieSubType.Melon] = "Melon";
			this.titles[(byte)GoodieSubType.Soup] = "Soup";
			this.titles[(byte)GoodieSubType.Pack1] = "Health Pack";
			this.titles[(byte)GoodieSubType.Pack2] = "Health Pack";

			// Shields
			this.titles[(byte)GoodieSubType.ShieldWood] = "Wood Shield";
			this.titles[(byte)GoodieSubType.ShieldWhite] = "White Shield";
			this.titles[(byte)GoodieSubType.ShieldBlue] = "Power Shield";

			// Jewelry
			this.titles[(byte)GoodieSubType.RingMagic] = "Magic Ring";
			this.titles[(byte)GoodieSubType.AmuletMagic] = "Magic Amulet";
			this.titles[(byte)GoodieSubType.RingFire] = "Fire Ring";
			this.titles[(byte)GoodieSubType.NeckFire] = "Fire Amulet";
			this.titles[(byte)GoodieSubType.RingPoison] = "Poison Charm";
			this.titles[(byte)GoodieSubType.NeckElectric] = "Electric Amulet";
			this.titles[(byte)GoodieSubType.RingElements] = "Elemental Ring";
			//this.titles[(byte)GoodieSubType.NeckHeart] = "Heart Ring";
			//this.titles[(byte)GoodieSubType.RingHawk] = "Hawk Ring";
			//this.titles[(byte)GoodieSubType.RingDruid] = "Ring";
			//this.titles[(byte)GoodieSubType.RingEye] = "Ring";
			
			// Invincibility
			this.titles[(byte)GoodieSubType.Shiny] = "Shiny Potion";
			this.titles[(byte)GoodieSubType.Stars] = "Star Potion";
			this.titles[(byte)GoodieSubType.GodMode] = "God Mode Collectable";

			// Timers
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

			// Shoes
			this.titles[(byte)GoodieSubType.DashingShoe] = "Dashing Shoe";
			this.titles[(byte)GoodieSubType.SpikeShoe] = "Spike Shoe";
			this.titles[(byte)GoodieSubType.WingShoe] = "Wing Shoe";

			this.descriptions = new string[36];

			// Health
			this.descriptions[(byte)GoodieSubType.Apple] = "Grants +1 Health";
			this.descriptions[(byte)GoodieSubType.Pear] = "Grants +1 Health";
			this.descriptions[(byte)GoodieSubType.Melon] = "Grants +2 Health";
			this.descriptions[(byte)GoodieSubType.Soup] = "Grants +2 Health";
			this.descriptions[(byte)GoodieSubType.Pack1] = "Grants Full Health";
			this.descriptions[(byte)GoodieSubType.Pack2] = "Grants Full Health";
			
			// Shields
			this.descriptions[(byte)GoodieSubType.ShieldWood] = "Grants +1 Shield";
			this.descriptions[(byte)GoodieSubType.ShieldWhite] = "Grants +2 Shields";
			this.descriptions[(byte)GoodieSubType.ShieldBlue] = "Grants Full Shields";

			// Jewelry
			this.descriptions[(byte)GoodieSubType.RingMagic] = "Grants a magical shield.";
			this.descriptions[(byte)GoodieSubType.AmuletMagic] = "Grants a powerful magical shield.";
			this.descriptions[(byte)GoodieSubType.RingFire] = "Grants a magical fire shield.";
			this.descriptions[(byte)GoodieSubType.NeckFire] = "Grants a powerful magical fire shield.";
			this.descriptions[(byte)GoodieSubType.RingPoison] = "Grants a magical poison shield.";
			this.descriptions[(byte)GoodieSubType.NeckElectric] = "Grants a magical electric shield.";
			this.descriptions[(byte)GoodieSubType.RingElements] = "Grants a magical elemental shield.";
			//this.descriptions[(byte)GoodieSubType.NeckHeart] = ".";
			//this.descriptions[(byte)GoodieSubType.RingHawk] = ".";
			//this.descriptions[(byte)GoodieSubType.RingDruid] = ".";
			//this.descriptions[(byte)GoodieSubType.RingEye] = ".";

			// Invincibility
			this.descriptions[(byte)GoodieSubType.Shiny] = "Makes the character invulnerable for ten seconds.";
			this.descriptions[(byte)GoodieSubType.Stars] = "Makes the character invulnerable and deadly on contact for ten seconds.";
			this.descriptions[(byte)GoodieSubType.GodMode] = "Makes the character invulnerable.";

			// Timers
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

			// Shoes
			this.descriptions[(byte)GoodieSubType.DashingShoe] = "Character always runs and gains the dash maneuver.";
			this.descriptions[(byte)GoodieSubType.SpikeShoe] = "Character always runs, gains a dash maneuver, and can grab walls.";
			this.descriptions[(byte)GoodieSubType.WingShoe] = "Character always runs and gains a longer dash maneuver.";
		}

		public override void Collect(RoomScene room, Character character, short gridX, short gridY) {
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);

			switch(subType) {

				// Inner Magi Shields
				case (byte)GoodieSubType.RingMagic:
					this.GetMagiShield(character, (byte)ProjectileMagiSubType.Magi, 3, 60, 480);
					character.magiShield.SetIconTexture(this.Texture[subType]);
					break;

				case (byte)GoodieSubType.RingFire:
					this.GetMagiShield(character, (byte)ProjectileMagiSubType.Fire, 3, 60);
					character.magiShield.SetIconTexture(this.Texture[subType]);
					break;

				case (byte)GoodieSubType.RingPoison:
					this.GetMagiShield(character, (byte)ProjectileMagiSubType.Poison, 4, 60);
					character.magiShield.SetIconTexture(this.Texture[subType]);
					break;

				case (byte)GoodieSubType.RingElements:
					this.GetMagiShield(character, (byte)ProjectileMagiSubType.Frost, 3, 55, 0);
					character.magiShield.SetIconTexture(this.Texture[subType]);
					break;

				//case (byte)GoodieSubType.RingHawk: this.GetMagiShield(character, (byte)ProjectileMagiSubType.Magi, 7, 60); break;
				//case (byte)GoodieSubType.RingDruid: this.GetMagiShield(character, (byte)ProjectileMagiSubType.Magi, 7, 60); break;
				//case (byte)GoodieSubType.RingEye: this.GetMagiShield(character, (byte)ProjectileMagiSubType.Magi, 7, 60); break;

				// Outer Magi Shields
				case (byte)GoodieSubType.AmuletMagic:
					this.GetMagiShield(character, (byte) ProjectileMagiSubType.Magi, 5, 70, 720);
					character.magiShield.SetIconTexture(this.Texture[subType]);
					break;

				case (byte)GoodieSubType.NeckElectric:
					this.GetMagiShield(character, (byte)ProjectileMagiSubType.Electric, 8, 80);
					character.magiShield.SetIconTexture(this.Texture[subType]);
					break;

				case (byte)GoodieSubType.NeckFire:
					this.GetMagiShield(character, (byte)ProjectileMagiSubType.Fire, 6, 70);
					character.magiShield.SetIconTexture(this.Texture[subType]);
					break;

				//case (byte)GoodieSubType.NeckHeart:
				//this.GetMagiShield(character, (byte)ProjectileMagiSubType.Magi, 7, 60);
				//character.magiShield.SetIconTexture(this.Texture[subType]);
				//break;

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
				case (byte)GoodieSubType.Explosive: TNT.DetonateTNT(character); break;

				// Shoes
				case (byte)GoodieSubType.DashingShoe: this.CollectShoe(character, ShoeSubType.Dashing); break;
				case (byte)GoodieSubType.SpikeShoe: this.CollectShoe(character, ShoeSubType.Spike); break;
				case (byte)GoodieSubType.WingShoe: this.CollectShoe(character, ShoeSubType.Wing); break;
			}

			// SubTypes that can be maxed out, and therefore not collected at times.
			switch(subType) {

				// Key Collection
				case (byte)GoodieSubType.Key:
					if(character.trailKeys.HasMaxKeys) { return; }
					this.CollectKey(character);
					break;

				// Health
				case (byte)GoodieSubType.Apple:
				case (byte)GoodieSubType.Pear:
					if(character.wounds.HasMaxHealth) { return; }
					this.GetHealth(character, 1);
					break;

				case (byte)GoodieSubType.Melon:
				case (byte)GoodieSubType.Soup:
					if(character.wounds.HasMaxHealth) { return; }
					this.GetHealth(character, 2);
					break;

				case (byte)GoodieSubType.Pack1:
				case (byte)GoodieSubType.Pack2:
					if(character.wounds.HasMaxHealth) { return; }
					this.GetHealth(character, 99);
					break;

				// Armor
				case (byte)GoodieSubType.ShieldWood:
					if(character.wounds.HasMaxArmor) { return; }
					this.GetArmor(character, 1);
					break;

				case (byte)GoodieSubType.ShieldWhite:
					if(character.wounds.HasMaxArmor) { return; }
					this.GetArmor(character, 3);
					break;

				case (byte)GoodieSubType.ShieldBlue:
					if(character.wounds.HasMaxArmor) { return; }
					this.GetArmor(character, 3);
					break;
			}

			base.Collect(room, character, gridX, gridY);
		}

		private void GetHealth(Character character, byte health) {
			character.wounds.AddHealth(health);
			character.room.PlaySound(Systems.sounds.food, 1f, character.posX + 16, character.posY + 16);
		}

		private void GetArmor(Character character, byte armor) {
			character.wounds.AddArmor(armor);
			character.room.PlaySound(Systems.sounds.collectSubtle, 1f, character.posX + 16, character.posY + 16);
		}

		private void GetMagiShield(Character character, byte subType, byte ballCount, byte radius, short regenFrames = 0) {
			character.magiShield.SetShield(subType, ballCount, radius, regenFrames);
			character.room.PlaySound(Systems.sounds.shield, 1f, character.posX + 16, character.posY + 16);
		}
		
		private void GetInvincible(Character character, int frames) {
			character.wounds.SetInvincible(frames);
			//Systems.sounds.collectSubtle.Play();
			character.room.PlaySound(Systems.sounds.potion, 1f, character.posX + 16, character.posY + 16);
		}

		private void SetTime(Character character, bool isAdditive, byte timeVal) {
			LevelState levelState = Systems.handler.levelState;

			// If the Collectable ADDS to the timer, rather than SETS.
			if(!isAdditive) {

				// Update the Timer
				int origFramesLeft = levelState.FramesRemaining;
				levelState.timeShift = 0;
				int trueTimeLeft = levelState.FramesRemaining;
				levelState.timeShift = -trueTimeLeft + (timeVal * 60);

				// Sound of collectable is based on whether it was positive or negative.
				if(origFramesLeft > levelState.FramesRemaining) {
					character.room.PlaySound(Systems.sounds.collectDisable, 1f, character.posX + 16, character.posY + 16);
				} else {
					character.room.PlaySound(Systems.sounds.timer2, 1f, character.posX + 16, character.posY + 16);
				}
			}

			// If the Collectable SETS the timer, rather than ADDS.
			else {
				levelState.timeShift += timeVal * 60;
				character.room.PlaySound(Systems.sounds.timer2, 1f, character.posX + 16, character.posY + 16);
			}
		}

		private void RunDisrupt(Character character) {
			character.DisableAbilities();
			character.room.PlaySound(Systems.sounds.collectDisable, 1f, character.posX + 16, character.posY + 16);
		}

		private void CollectShoe(Character character, ShoeSubType shoeSub) {
			Shoes.AssignShoe(character, (byte) shoeSub);
			character.room.PlaySound(Systems.sounds.collectSubtle, 1f, character.posX + 16, character.posY + 16);
		}
		
		private void CollectKey(Character character) {
			character.trailKeys.AddKey();
			character.room.PlaySound(Systems.sounds.collectKey, 1f, character.posX + 16, character.posY + 16);
		}

		private void CreateTextures() {
			this.Texture = new string[36];
			
			// Health Goodies
			this.Texture[(byte) GoodieSubType.Apple] = "Health/Apple";
			this.Texture[(byte) GoodieSubType.Pear] = "Health/Pear";
			this.Texture[(byte) GoodieSubType.Melon] = "Health/Melon";
			this.Texture[(byte) GoodieSubType.Soup] = "Health/Soup";
			this.Texture[(byte) GoodieSubType.Pack1] = "Health/Pack1";
			this.Texture[(byte) GoodieSubType.Pack2] = "Health/Pack2";

			// Shield Goodies
			this.Texture[(byte) GoodieSubType.ShieldWood] = "Shield/Wood";
			this.Texture[(byte) GoodieSubType.ShieldWhite] = "Shield/White";
			this.Texture[(byte) GoodieSubType.ShieldBlue] = "Shield/Blue";

			// Jewelry Goodies
			this.Texture[(byte) GoodieSubType.RingMagic] = "Jewelry/Magic";
			this.Texture[(byte) GoodieSubType.AmuletMagic] = "Jewelry/NeckMagic";
			this.Texture[(byte) GoodieSubType.RingFire] = "Jewelry/Fire";
			this.Texture[(byte) GoodieSubType.NeckFire] = "Jewelry/NeckFire";
			this.Texture[(byte) GoodieSubType.RingPoison] = "Jewelry/Poison";
			this.Texture[(byte) GoodieSubType.NeckElectric] = "Jewelry/NeckElectric";
			this.Texture[(byte) GoodieSubType.RingElements] = "Jewelry/Elements";
			this.Texture[(byte) GoodieSubType.NeckHeart] = "Jewelry/NeckHeart";
			this.Texture[(byte) GoodieSubType.RingHawk] = "Jewelry/Hawk";
			this.Texture[(byte) GoodieSubType.RingDruid] = "Jewelry/Druid";
			this.Texture[(byte) GoodieSubType.RingEye] = "Jewelry/Eye";

			// Invincibility Goodies
			this.Texture[(byte) GoodieSubType.Shiny] = "Goodie/Shiny";
			this.Texture[(byte) GoodieSubType.Stars] = "Goodie/Stars";
			this.Texture[(byte) GoodieSubType.GodMode] = "Goodie/GodMode";

			// Timer Goodies
			this.Texture[(byte) GoodieSubType.Plus5] = "Timer/Plus5";
			this.Texture[(byte) GoodieSubType.Plus10] = "Timer/Plus10";
			this.Texture[(byte) GoodieSubType.Plus20] = "Timer/Plus20";
			this.Texture[(byte) GoodieSubType.Set5] = "Timer/Set5";
			this.Texture[(byte) GoodieSubType.Set10] = "Timer/Set10";
			this.Texture[(byte) GoodieSubType.Set20] = "Timer/Set20";

			// Misc Goodies
			this.Texture[(byte) GoodieSubType.Disrupt] = "Goodie/Disrupt";
			this.Texture[(byte) GoodieSubType.Explosive] = "Goodie/Explosive";
			this.Texture[(byte) GoodieSubType.Key] = "Goodie/Key";
			this.Texture[(byte) GoodieSubType.Blood] = "Goodie/Blood";

			// Shoe Goodies
			this.Texture[(byte) GoodieSubType.DashingShoe] = "Shoes/Red";
			this.Texture[(byte) GoodieSubType.SpikeShoe] = "Shoes/Spike";
			this.Texture[(byte) GoodieSubType.WingShoe] = "Shoes/Wing";
		}
	}
}
