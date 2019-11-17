using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	class CollectablePower : Collectable {

		public CollectablePower() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.CollectablePower;
		}

		public override void Collect( RoomScene room, Character character, uint gridId ) {

			byte[] tileData = room.tilemap.GetTileDataAtGridID(gridId);
			Power.AssignToCharacter(character, tileData[1]);

			Systems.sounds.collectSubtle.Play();
			base.Collect(room, character, gridId);
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
