﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	class CollectablePower : Collectable {

		public CollectablePower() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.CollectablePower;
			this.paramSets = new Params[1] { Params.ParamMap["Collectable"] };

			// Helper Texts
			this.titles = new string[64];

			// Collectable Powers - Mobility
			this.titles[(byte)PowerSubType.RandBook] = "Random Book Collectable";
			this.titles[(byte)PowerSubType.SlowFall] = "Slow Fall Powerup";
			this.titles[(byte)PowerSubType.Hover] = "Hover Powerup";
			this.titles[(byte)PowerSubType.Levitate] = "Levitation Powerup";
			this.titles[(byte)PowerSubType.Flight] = "Flight Powerup";
			this.titles[(byte)PowerSubType.Athlete] = "Athlete Powerup";
			this.titles[(byte)PowerSubType.Leap] = "Leap Powerup";
			this.titles[(byte)PowerSubType.Slam] = "Slam Powerup";
			this.titles[(byte)PowerSubType.Burst] = "Burst Powerup";
			this.titles[(byte)PowerSubType.Air] = "Air Burst Powerup";
			this.titles[(byte)PowerSubType.Phase] = "Phase Powerup";
			this.titles[(byte)PowerSubType.Teleport] = "Teleport Powerup";

			// Collectable Powers - Weapon
			this.titles[(byte)PowerSubType.RandWeapon] = "Random Weapon Collectable";
			this.titles[(byte)PowerSubType.BoxingRed] = "Red Boxing Glove";
			this.titles[(byte)PowerSubType.BoxingWhite] = "White Boxing Glove";
			this.titles[(byte)PowerSubType.Dagger] = "Dagger";
			this.titles[(byte)PowerSubType.DaggerGreen] = "Green Dagger";
			this.titles[(byte)PowerSubType.Spear] = "Spear";
			this.titles[(byte)PowerSubType.Sword] = "Sword";

			// Collectable Powers - Potion
			this.titles[(byte)PowerSubType.RandPot] = "Random Potion Collectable";
			this.titles[(byte)PowerSubType.Electric] = "Electric Potion";
			this.titles[(byte)PowerSubType.Fire] = "Fireball Potion";
			this.titles[(byte)PowerSubType.Frost] = "Frost Potion";
			this.titles[(byte)PowerSubType.Rock] = "Rock Potion";
			this.titles[(byte)PowerSubType.Water] = "Waterball Potion";
			this.titles[(byte)PowerSubType.Slime] = "Slimeball Potion";
			//this.titles[(byte)PowerSubType.Ball] = "Ball Potion";

			// Collectable Powers - Thrown
			this.titles[(byte)PowerSubType.RandThrown] = "Random Thrown Collectable";
			this.titles[(byte)PowerSubType.Axe] = "Throwing Axe";
			this.titles[(byte)PowerSubType.Hammer] = "Throwing Hammer";
			this.titles[(byte)PowerSubType.Shuriken] = "Shuriken";

			// Power Collectable - Bolts
			this.titles[(byte)PowerSubType.RandBolt] = "Random Staff Collectable";
			this.titles[(byte)PowerSubType.BoltBlue] = "Blue Staff";
			this.titles[(byte)PowerSubType.BoltGold] = "Gold Staff";
			this.titles[(byte)PowerSubType.BoltGreen] = "Green Staff";
			//this.titles[(byte)PowerSubType.BoltNecro] = "Necro Staff";
			//this.titles[(byte)PowerSubType.Necro1] = "Necro1 Powerup";
			//this.titles[(byte)PowerSubType.Necro2] = "Necro2 Powerup";

			// Collectable Powers - Stack
			this.titles[(byte)PowerSubType.Chakram] = "Chakram";
			this.titles[(byte)PowerSubType.ChakramPack] = "Chakram Pack";
			this.titles[(byte)PowerSubType.Grenade] = "Grenade";
			this.titles[(byte)PowerSubType.GrenadePack] = "Grenade Pack";

			// Helper Descriptions
			this.descriptions = new string[64];

			// Collectable Powers - Mobility
			this.descriptions[(byte)PowerSubType.RandBook] = "Acquire a random mobility powerup from the 'book' selections.";
			this.descriptions[(byte)PowerSubType.SlowFall] = "Grants the ability to fall slower.";
			this.descriptions[(byte)PowerSubType.Hover] = "Grants the ability to hover in mid-air for a short duration.";
			this.descriptions[(byte)PowerSubType.Levitate] = "Grants the ability to levitate in any direction for a short duration.";
			this.descriptions[(byte)PowerSubType.Flight] = "Grants the power to fly.";
			this.descriptions[(byte)PowerSubType.Athlete] = "Gain improved athletic abilities.";
			this.descriptions[(byte)PowerSubType.Leap] = "Grants a special jump that can be used while in mid-air.";
			this.descriptions[(byte)PowerSubType.Slam] = "Grants the ability to slam the ground.";
			this.descriptions[(byte)PowerSubType.Burst] = "Grants a burst of momentum.";
			this.descriptions[(byte)PowerSubType.Air] = "Grants an overpowering burst of momentum to the intended direction.";
			this.descriptions[(byte)PowerSubType.Phase] = "Grants the power to 'blink' a few tiles in distance, often bypassing obstacles.";
			this.descriptions[(byte)PowerSubType.Teleport] = "Grants the power to set a teleport spot and return to it.";

			// Collectable Powers - Weapon
			this.descriptions[(byte)PowerSubType.RandWeapon] = "Acquire a random held weapon.";
			this.descriptions[(byte)PowerSubType.BoxingRed] = "Damages enemies, can break through boxes and bricks.";
			this.descriptions[(byte)PowerSubType.BoxingWhite] = "Damages enemies, can break through boxes and bricks.";
			this.descriptions[(byte)PowerSubType.Dagger] = "Short range, very fast, use in any direction.";
			this.descriptions[(byte)PowerSubType.DaggerGreen] = "Damages enemies, short range, use in any direction.";
			this.descriptions[(byte)PowerSubType.Spear] = "Long range, slow recharge. Horizontal strikes only.";
			this.descriptions[(byte)PowerSubType.Sword] = "Medium range, fast attack. Horizontal strikes only.";

			// Collectable Powers - Potion
			this.descriptions[(byte)PowerSubType.RandPot] = "Acquire a random potion.";
			this.descriptions[(byte)PowerSubType.Electric] = "Cast balls of electricity. Fast, bounce on ground, unwieldy.";
			this.descriptions[(byte)PowerSubType.Fire] = "Cast fireballs. Bounce on ground.";
			this.descriptions[(byte)PowerSubType.Frost] = "Cast frost balls. Long range, fast, collide with ground.";
			this.descriptions[(byte)PowerSubType.Rock] = "Throw rocks, which quickly fall below.";
			this.descriptions[(byte)PowerSubType.Water] = "Cast water balls. Multiple casts at once.";
			this.descriptions[(byte)PowerSubType.Slime] = "Cast slime balls.";
			//this.descriptions[(byte)PowerSubType.Ball] = "Unknown power.";

			// Collectable Powers - Thrown
			this.descriptions[(byte)PowerSubType.RandThrown] = "Acquire a random throwing weapon.";
			this.descriptions[(byte)PowerSubType.Axe] = "A large arcing weapon. Very deadly. Slow recharge.";
			this.descriptions[(byte)PowerSubType.Hammer] = "Throws multiple attacks with random arcs.";
			this.descriptions[(byte)PowerSubType.Shuriken] = "Throws a fast attack in chosen direction.";

			// Power Collectable - Bolts
			this.descriptions[(byte)PowerSubType.RandBolt] = "Acquire a random staff collectable.";
			this.descriptions[(byte)PowerSubType.BoltBlue] = "Cast a blue bolt. Straight path, collides with objects, angled attacks.";
			this.descriptions[(byte)PowerSubType.BoltGold] = "Cast a gold bolt. Straight path, ignores ground, angled attacks.";
			this.descriptions[(byte)PowerSubType.BoltGreen] = "Cast a green bolt. Wavy random path, ignores ground.";
			//this.descriptions[(byte)PowerSubType.BoltNecro] = "Power/BoltNecro";
			//this.descriptions[(byte)PowerSubType.Necro1] = "Power/Necro1";
			//this.descriptions[(byte)PowerSubType.Necro2] = "Power/Necro2";

			// Collectable Powers - Stack
			this.descriptions[(byte)PowerSubType.Chakram] = "Very deadly single-use attack. Straight path, ignores walls.";
			this.descriptions[(byte)PowerSubType.ChakramPack] = "A pack of three chakrams.";
			this.descriptions[(byte)PowerSubType.Grenade] = "Very deadly, single-use, area of effect attack. Damages enemies and certain tiles.";
			this.descriptions[(byte)PowerSubType.GrenadePack] = "A pack of three grenades.";
		}

		public override void Collect( RoomScene room, Character character, ushort gridX, ushort gridY ) {

			byte subType = room.tilemap.GetMainSubType(gridX, gridY);
			Power.AssignToCharacter(character, subType);

			Systems.sounds.collectSubtle.Play();
			base.Collect(room, character, gridX, gridY);
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
