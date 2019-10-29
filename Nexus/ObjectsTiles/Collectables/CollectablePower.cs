﻿using System;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	class CollectablePower : Collectable {

		public enum PowerSubType : byte {

			// Collectable Powers - Mobility
			RandBook = 0,
			SlowFall = 1,
			Hover = 2,
			Levitate = 3,
			Flight = 4,
			Athlete = 5,
			Leap = 6,
			Slam = 7,
			Burst = 8,
			Air = 9,
			Phase = 10,
			Teleport = 11,
			
			// Collectable Powers - Weapon
			RandWeapon = 20,
			BoxingRed = 21,
			BoxingWhite = 22,
			Dagger = 23,
			DaggerGreen = 24,
			Spear = 25,
			Sword = 26,
			
			// Collectable Powers - Potion
			RandPot = 30,
			Electric = 31,
			Fire = 32,
			Frost = 33,
			Rock = 34,
			Water = 35,
			Slime = 36,
			Ball = 37,
			
			// Collectable Powers - Thrown
			RandThrown = 40,
			Axe = 41,
			Hammer = 42,
			Shuriken = 43,
			
			// Power Collectable - Bolts
			RandBolt = 50,
			Bolt = 51,
			BoltGold = 52,
			BoltGreen = 53,
			BoltNecro = 54,
			Necro1 = 55,
			Necro2 = 56,
			
			// Collectable Powers - Stack
			Chakram = 60,
			ChakramPack = 61,
			Grenade = 62,
			GrenadePack = 63,
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.CollectablePower)) {
				new CollectablePower(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.CollectablePower, subTypeId);
		}

		public CollectablePower(LevelScene scene) : base(scene, TileGameObjectId.CollectablePower) {
			this.CreateTextures();
		}

		public override void Collect( Character character, uint gridId ) {
			byte subType = this.scene.tilemap.GetSubTypeAtGridID(gridId);

			// Random Throwing Weapon
			if(subType == (byte) PowerSubType.RandThrown) {
				
			}

			switch(subType) {

				// Collectable Powers - Mobility
				case (byte)PowerSubType.RandBook: this.Something(); break;
				case (byte)PowerSubType.SlowFall: this.Something(); break;
				case (byte)PowerSubType.Hover: this.Something(); break;
				case (byte)PowerSubType.Levitate: this.Something(); break;
				case (byte)PowerSubType.Flight: this.Something(); break;
				case (byte)PowerSubType.Athlete: this.Something(); break;
				case (byte)PowerSubType.Leap: this.Something(); break;
				case (byte)PowerSubType.Slam: this.Something(); break;
				case (byte)PowerSubType.Burst: this.Something(); break;
				case (byte)PowerSubType.Air: this.Something(); break;
				case (byte)PowerSubType.Phase: this.Something(); break;
				case (byte)PowerSubType.Teleport: this.Something(); break;
				
				// Collectable Powers - Weapon
				case (byte)PowerSubType.RandWeapon: this.Something(); break;
				case (byte)PowerSubType.BoxingRed: this.Something(); break;
				case (byte)PowerSubType.BoxingWhite: this.Something(); break;
				case (byte)PowerSubType.Dagger: this.Something(); break;
				case (byte)PowerSubType.DaggerGreen: this.Something(); break;
				case (byte)PowerSubType.Spear: this.Something(); break;
				case (byte)PowerSubType.Sword: this.Something(); break;
				
				// Collectable Powers - Potion
				case (byte)PowerSubType.RandPot: this.Something(); break;
				case (byte)PowerSubType.Electric: this.Something(); break;
				case (byte)PowerSubType.Fire: this.Something(); break;
				case (byte)PowerSubType.Frost: this.Something(); break;
				case (byte)PowerSubType.Rock: this.Something(); break;
				case (byte)PowerSubType.Water: this.Something(); break;
				case (byte)PowerSubType.Slime: this.Something(); break;
				case (byte)PowerSubType.Ball: this.Something(); break;
				
				// Collectable Powers - Thrown
				case (byte)PowerSubType.RandThrown: this.Something(); break;
				case (byte)PowerSubType.Axe: this.Something(); break;
				case (byte)PowerSubType.Hammer: this.Something(); break;
				case (byte)PowerSubType.Shuriken: this.Something(); break;
				
				// Power Collectable - Bolts
				case (byte)PowerSubType.RandBolt: this.Something(); break;
				case (byte)PowerSubType.Bolt: this.Something(); break;
				case (byte)PowerSubType.BoltGold: this.Something(); break;
				case (byte)PowerSubType.BoltGreen: this.Something(); break;
				case (byte)PowerSubType.BoltNecro: this.Something(); break;
				case (byte)PowerSubType.Necro1: this.Something(); break;
				case (byte)PowerSubType.Necro2: this.Something(); break;
				
				// Collectable Powers - Stack
				case (byte)PowerSubType.Chakram: this.Something(); break;
				case (byte)PowerSubType.ChakramPack: this.Something(); break;
				case (byte)PowerSubType.Grenade: this.Something(); break;
				case (byte)PowerSubType.GrenadePack: this.Something(); break;
			}

			Systems.sounds.collectSubtle.Play();
			base.Collect(character, gridId);
		}

		private void Something() {
			throw new NotImplementedException();
		}

		private void CreateTextures() {
			this.Texture = new string[64];
			
			// Collectable Powers - Mobility
			this.Texture[(byte)PowerSubType.RandBook] = "Power/RandBook";
			this.Texture[(byte)PowerSubType.SlowFall] = "Power/SlowFall";
			this.Texture[(byte)PowerSubType.Hover] = "Power/Hover";
			this.Texture[(byte)PowerSubType.Levitate] = "Power/Levitate";
			this.Texture[(byte)PowerSubType.Flight] = "Power/Flight";
			this.Texture[(byte)PowerSubType.Athlete] = "Power/Athlete";
			this.Texture[(byte)PowerSubType.Leap] = "Power/Leap";
			this.Texture[(byte)PowerSubType.Slam] = "Power/Slam";
			this.Texture[(byte)PowerSubType.Burst] = "Power/Burst";
			this.Texture[(byte)PowerSubType.Air] = "Power/Air";
			this.Texture[(byte)PowerSubType.Phase] = "Power/Phase";
			this.Texture[(byte)PowerSubType.Teleport] = "Power/Teleport";

			// Collectable Powers - Weapon
			this.Texture[(byte)PowerSubType.RandWeapon] = "Power/RandWeapon";
			this.Texture[(byte)PowerSubType.BoxingRed] = "Power/BoxingRed";
			this.Texture[(byte)PowerSubType.BoxingWhite] = "Power/BoxingWhite";
			this.Texture[(byte)PowerSubType.Dagger] = "Power/Dagger";
			this.Texture[(byte)PowerSubType.DaggerGreen] = "Power/DaggerGreen";
			this.Texture[(byte)PowerSubType.Spear] = "Power/Spear";
			this.Texture[(byte)PowerSubType.Sword] = "Power/Sword";

			// Collectable Powers - Potion
			this.Texture[(byte)PowerSubType.RandPot] = "Power/RandPot";
			this.Texture[(byte)PowerSubType.Electric] = "Power/Electric";
			this.Texture[(byte)PowerSubType.Fire] = "Power/Fire";
			this.Texture[(byte)PowerSubType.Frost] = "Power/Frost";
			this.Texture[(byte)PowerSubType.Rock] = "Power/Rock";
			this.Texture[(byte)PowerSubType.Water] = "Power/Water";
			this.Texture[(byte)PowerSubType.Slime] = "Power/Slime";
			this.Texture[(byte)PowerSubType.Ball] = "Power/Ball";

			// Collectable Powers - Thrown
			this.Texture[(byte)PowerSubType.RandThrown] = "Power/RandThrown";
			this.Texture[(byte)PowerSubType.Axe] = "Power/Axe";
			this.Texture[(byte)PowerSubType.Hammer] = "Power/Hammer";
			this.Texture[(byte)PowerSubType.Shuriken] = "Power/Shuriken";

			// Power Collectable - Bolts
			this.Texture[(byte)PowerSubType.RandBolt] = "Power/RandBolt";
			this.Texture[(byte)PowerSubType.Bolt] = "Power/Bolt";
			this.Texture[(byte)PowerSubType.BoltGold] = "Power/BoltGold";
			this.Texture[(byte)PowerSubType.BoltGreen] = "Power/BoltGreen";
			this.Texture[(byte)PowerSubType.BoltNecro] = "Power/BoltNecro";
			this.Texture[(byte)PowerSubType.Necro1] = "Power/Necro1";
			this.Texture[(byte)PowerSubType.Necro2] = "Power/Necro2";

			// Collectable Powers - Stack
			this.Texture[(byte)PowerSubType.Chakram] = "Power/Chakram";
			this.Texture[(byte)PowerSubType.ChakramPack] = "Power/ChakramPack";
			this.Texture[(byte)PowerSubType.Grenade] = "Power/Grenade";
			this.Texture[(byte)PowerSubType.GrenadePack] = "Power/GrenadePack";
		}
	}
}
