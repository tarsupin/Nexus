using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	class CollectablePower : Collectable {

		public enum PowerSubType {

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

		public override void Collect(uint gridId) {
			// TODO SOUND: Collect Power
			base.Collect(gridId);
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
