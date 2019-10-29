using System;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

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
			BoltBlue = 51,
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

			//case (byte)PowerSubType.RandPot: this.Something(); break; // mobility
			//case (byte)PowerSubType.RandBook: this.Something(); break; //projectiles
			//case (byte)PowerSubType.RandWeapon: this.Something(); break;
			//case (byte)PowerSubType.RandThrown: this.Something(); break;
			//case (byte)PowerSubType.RandBolt: this.Something(); break;

			switch(subType) {

				// Collectable Powers - Mobility
				case (byte)PowerSubType.SlowFall: character.mobilityPower = new SlowFallMobility(character); break;
				case (byte)PowerSubType.Hover: character.mobilityPower = new HoverMobility(character); break;
				case (byte)PowerSubType.Levitate: character.mobilityPower = new LevitateMobility(character); break;
				case (byte)PowerSubType.Flight: character.mobilityPower = new FlightMobility(character); break;
				case (byte)PowerSubType.Athlete: character.mobilityPower = new AthleteMobility(character); break;
				case (byte)PowerSubType.Leap: character.mobilityPower = new LeapMobility(character); break;
				case (byte)PowerSubType.Slam: character.mobilityPower = new SlamMobility(character); break;
				case (byte)PowerSubType.Burst: character.mobilityPower = new BurstMobility(character); break;
				case (byte)PowerSubType.Air: character.mobilityPower = new AirMobility(character); break;
				case (byte)PowerSubType.Phase: character.mobilityPower = new PhaseMobility(character); break;
				case (byte)PowerSubType.Teleport: character.mobilityPower = new TeleportMobility(character); break;
				
				// Collectable Powers - Weapon
				case (byte)PowerSubType.BoxingRed: this.Something(); break;
				case (byte)PowerSubType.BoxingWhite: this.Something(); break;
				case (byte)PowerSubType.Dagger: this.Something(); break;
				case (byte)PowerSubType.DaggerGreen: this.Something(); break;
				case (byte)PowerSubType.Spear: this.Something(); break;
				case (byte)PowerSubType.Sword: this.Something(); break;
				
				// Collectable Powers - Potion
				case (byte)PowerSubType.Electric: character.attackPower = new ElectricBall(character); break;
				case (byte)PowerSubType.Fire: character.attackPower = new FireBall(character); break;
				case (byte)PowerSubType.Frost: character.attackPower = new FrostBall(character); break;
				case (byte)PowerSubType.Rock: character.attackPower = new RockBall(character); break;
				case (byte)PowerSubType.Water: character.attackPower = new WaterBall(character); break;
				case (byte)PowerSubType.Slime: character.attackPower = new SlimeBall(character); break;
				
				// Collectable Powers - Thrown
				case (byte)PowerSubType.Axe: character.attackPower = new Axe(character, WeaponAxeSubType.Axe); break;
				case (byte)PowerSubType.Hammer: character.attackPower = new Hammer(character, WeaponHammerSubType.Hammer); break;
				case (byte)PowerSubType.Shuriken: character.attackPower = new Shuriken(character, WeaponShurikenSubType.Shuriken); break;
				
				// Power Collectable - Bolts
				case (byte)PowerSubType.BoltBlue: character.attackPower = new Bolt(character, ProjectileBoltSubType.Blue); break;
				case (byte)PowerSubType.BoltGold: character.attackPower = new Bolt(character, ProjectileBoltSubType.Gold); break;
				case (byte)PowerSubType.BoltGreen: character.attackPower = new Bolt(character, ProjectileBoltSubType.Green); break;
				//case (byte)PowerSubType.BoltNecro: this.Something(); break;
				//case (byte)PowerSubType.Necro1: this.Something(); break;
				//case (byte)PowerSubType.Necro2: this.Something(); break;
				
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
			//this.Texture[(byte)PowerSubType.Ball] = "Power/Ball";

			// Collectable Powers - Thrown
			this.Texture[(byte)PowerSubType.RandThrown] = "Power/RandThrown";
			this.Texture[(byte)PowerSubType.Axe] = "Power/Axe";
			this.Texture[(byte)PowerSubType.Hammer] = "Power/Hammer";
			this.Texture[(byte)PowerSubType.Shuriken] = "Power/Shuriken";

			// Power Collectable - Bolts
			this.Texture[(byte)PowerSubType.RandBolt] = "Power/RandBolt";
			this.Texture[(byte)PowerSubType.BoltBlue] = "Power/Bolt";
			this.Texture[(byte)PowerSubType.BoltGold] = "Power/BoltGold";
			this.Texture[(byte)PowerSubType.BoltGreen] = "Power/BoltGreen";
			//this.Texture[(byte)PowerSubType.BoltNecro] = "Power/BoltNecro";
			//this.Texture[(byte)PowerSubType.Necro1] = "Power/Necro1";
			//this.Texture[(byte)PowerSubType.Necro2] = "Power/Necro2";

			// Collectable Powers - Stack
			this.Texture[(byte)PowerSubType.Chakram] = "Power/Chakram";
			this.Texture[(byte)PowerSubType.ChakramPack] = "Power/ChakramPack";
			this.Texture[(byte)PowerSubType.Grenade] = "Power/Grenade";
			this.Texture[(byte)PowerSubType.GrenadePack] = "Power/GrenadePack";
		}
	}
}
