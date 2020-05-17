using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	// This class is designed to hold DynamicObjects for the Editor, allowing them to be treated like tiles for the purposes of editing.
	public static class ShadowTile {

		public static Atlas atlas;

		public static Dictionary<byte, Dictionary<byte, string>> Textures = new Dictionary<byte, Dictionary<byte, string>> {

			//////////////////////////
			////// LAND ENEMIES //////
			//////////////////////////

			// Moosh
			{ (byte) ObjectEnum.Moosh, new Dictionary<byte, string> {
				{  (byte) MooshSubType.Brown, "Moosh/Brown/Left2" },
				{  (byte) MooshSubType.Purple, "Moosh/Purple/Left2" },
				{  (byte) MooshSubType.White, "Moosh/White/Left2" },
			}},

			// Shroom
			{ (byte) ObjectEnum.Shroom, new Dictionary<byte, string> {
				{  (byte) ShroomSubType.Black, "Shroom/Black/Left2" },
				{  (byte) ShroomSubType.Purple, "Shroom/Purple/Left2" },
				{  (byte) ShroomSubType.Red, "Shroom/Red/Left2" },
			}},

			// Bug
			{ (byte) ObjectEnum.Bug, new Dictionary<byte, string> {
				{  (byte) BugSubType.Bug, "Bug/Left1" },
			}},

			// Goo
			{ (byte) ObjectEnum.Goo, new Dictionary<byte, string> {
				{  (byte) GooSubType.Green, "Goo/Green/Left1" },
				{  (byte) GooSubType.Orange, "Goo/Orange/Left1" },
				{  (byte) GooSubType.Blue, "Goo/Blue/Left1" },
			}},

			// Liz
			{ (byte) ObjectEnum.Liz, new Dictionary<byte, string> {
				{  (byte) LizSubType.Liz, "Liz/Left1" },
			}},

			// Snek
			{ (byte) ObjectEnum.Snek, new Dictionary<byte, string> {
				{  (byte) SnekSubType.Snek, "Snek/Left1" },
				{  (byte) SnekSubType.Wurm, "Wurm/Left1" },
			}},

			// Octo
			{ (byte) ObjectEnum.Octo, new Dictionary<byte, string> {
				{  (byte) OctoSubType.Octo, "Octo/Left1" },
			}},

			// Bones
			{ (byte) ObjectEnum.Bones, new Dictionary<byte, string> {
				{  (byte) BonesSubType.Bones, "Bones/Left1" },
			}},

			// Turtle
			{ (byte) ObjectEnum.Turtle, new Dictionary<byte, string> {
				{  (byte) TurtleSubType.Red, "Turtle/Left2" },
			}},

			// Snail
			{ (byte) ObjectEnum.Snail, new Dictionary<byte, string> {
				{  (byte) SnailSubType.Snail, "Snail/Left2" },
			}},

			// Boom
			{ (byte) ObjectEnum.Boom, new Dictionary<byte, string> {
				{  (byte) BoomSubType.Boom, "Boom/Left2" },
			}},

			// Poke
			{ (byte) ObjectEnum.Poke, new Dictionary<byte, string> {
				{  (byte) PokeSubType.Poke, "Poke/Left1" },
			}},

			// Lich
			{ (byte) ObjectEnum.Lich, new Dictionary<byte, string> {
				{  (byte) LichSubType.Lich, "Lich/Left1" },
			}},

			////////////////////////////
			////// FLIGHT ENEMIES //////
			////////////////////////////

			// Buzz
			{ (byte) ObjectEnum.Buzz, new Dictionary<byte, string> {
				{  (byte) BuzzSubType.Buzz, "Buzz/Left2" },
			}},

			// Ghost
			{ (byte) ObjectEnum.Ghost, new Dictionary<byte, string> {
				{  (byte) GhostSubType.Norm, "Ghost/Norm/Left" },
				{  (byte) GhostSubType.Hide, "Ghost/Hide/Left" },
				{  (byte) GhostSubType.Hat, "Ghost/Hat/Left" },
			}},
			
			// FlairElectric
			{ (byte) ObjectEnum.FlairElectric, new Dictionary<byte, string> {
				{  (byte) FlairElectricSubType.Normal, "Flair/Electric/Left2" },
			}},

			// FlairFire
			{ (byte) ObjectEnum.FlairFire, new Dictionary<byte, string> {
				{  (byte) FlairFireSubType.Normal, "Flair/Fire/Left2" },
			}},
			
			// FlairMagic
			{ (byte) ObjectEnum.FlairMagic, new Dictionary<byte, string> {
				{  (byte) FlairMagicSubType.Normal, "Flair/Magic/Left2" },
			}},
			
			// ElementalAir
			{ (byte) ObjectEnum.ElementalAir, new Dictionary<byte, string> {
				{  (byte) ElementalAirSubType.Normal, "Elemental/Air/Left" },
			}},

			// ElementalEarth
			{ (byte) ObjectEnum.ElementalEarth, new Dictionary<byte, string> {
				{  (byte) ElementalEarthSubType.Normal, "Elemental/Earth/Left" },
			}},

			// ElementalFire
			{ (byte) ObjectEnum.ElementalFire, new Dictionary<byte, string> {
				{  (byte) ElementalFireSubType.Normal, "Elemental/Fire/Left" },
			}},
			
			// Saw
			{ (byte) ObjectEnum.Saw, new Dictionary<byte, string> {
				{  (byte) SawSubType.Small, "Saw/Small" },
				{  (byte) SawSubType.Large, "Saw/Large" },
				{  (byte) SawSubType.LethalSmall, "Saw/LethalSmall" },
				{  (byte) SawSubType.LethalLarge, "Saw/LethalLarge" },
			}},
			
			// Slammer
			{ (byte) ObjectEnum.Slammer, new Dictionary<byte, string> {
				{  (byte) SlammerSubType.Slammer, "Slammer/Standard" },
			}},
			
			// HoveringEye
			{ (byte) ObjectEnum.HoveringEye, new Dictionary<byte, string> {
				{  (byte) HoveringEyeSubType.Eye, "Eye/Eye" },
			}},

			// WallBouncer
			{ (byte) ObjectEnum.WallBouncer, new Dictionary<byte, string> {
				{  (byte) WallBouncerSubType.Normal, "Bouncer/Norm" },
			}},
			
			// Dire
			{ (byte) ObjectEnum.Dire, new Dictionary<byte, string> {
				{  (byte) DireSubType.Dire, "Dire/Left1" },
			}},

			///////////////////////////////
			////// DYNAMIC PLATFORMS //////
			///////////////////////////////
			
			//// Platforms (1 - 4)
			//{ (byte) ObjectEnum.PlatformDip, Type.GetType("Nexus.Objects.PlatformDip") },
			//{ (byte) ObjectEnum.PlatformDelay, Type.GetType("Nexus.Objects.PlatformDelay") },
			//{ (byte) ObjectEnum.PlatformFall, Type.GetType("Nexus.Objects.PlatformFall") },
			//{ (byte) ObjectEnum.PlatformMove, Type.GetType("Nexus.Objects.PlatformMove") },
			
			///////////////////
			////// ITEMS //////
			///////////////////
			
			//// Items, Fixed (70 - 79)
			//{ (byte) ObjectEnum.SpringFixed, Type.GetType("Nexus.Objects.SpringFixed") },
			//{ (byte) ObjectEnum.ButtonFixed, Type.GetType("Nexus.Objects.ButtonFixed") },

			//// Items, Mobile (80 - 99)
			//{ (byte) ObjectEnum.Shell, Type.GetType("Nexus.Objects.Shell") },
			//{ (byte) ObjectEnum.Boulder, Type.GetType("Nexus.Objects.Boulder") },
			//{ (byte) ObjectEnum.Bomb, Type.GetType("Nexus.Objects.Bomb") },

			//{ (byte) ObjectEnum.TNT, Type.GetType("Nexus.Objects.TNT") },

			//{ (byte) ObjectEnum.SpringStandard, Type.GetType("Nexus.Objects.SpringStandard") },
			//{ (byte) ObjectEnum.ButtonStandard, Type.GetType("Nexus.Objects.ButtonStandard") },
			//{ (byte) ObjectEnum.ButtonTimed, Type.GetType("Nexus.Objects.ButtonTimed") },

			//{ (byte) ObjectEnum.MobileBlockBlue, Type.GetType("Nexus.Objects.MobileBlockBlue") },
			//{ (byte) ObjectEnum.MobileBlockRed, Type.GetType("Nexus.Objects.MobileBlockRed") },
			//{ (byte) ObjectEnum.MobileBlockGreen, Type.GetType("Nexus.Objects.MobileBlockGreen") },
			//{ (byte) ObjectEnum.MobileBlockYellow, Type.GetType("Nexus.Objects.MobileBlockYellow") },

			/////////////////////////////
			////// SPECIAL OBJECTS //////
			/////////////////////////////
			
			//// Special Objects
			//{ (byte) ObjectEnum.Cluster, Type.GetType("Nexus.Objects.Cluster") },

			//// Special Flags and Placements (150+)
			//{ (byte) ObjectEnum.Character, Type.GetType("Nexus.Objects.Character") },

		};
		
		public static void Draw(byte index, byte subType, Dictionary<string, short> paramList, int posX, int posY) {

			// Make sure the index is valid; otherwise the draw will crash:
			if(ShadowTile.Textures.ContainsKey(index)) {

				// Special Draw (some indexes may have special behavior)
				// TODO: if(index is of type x) { do special draw }

				if(ShadowTile.Textures[index].ContainsKey(subType)) {

					// Standard Draw
					ShadowTile.atlas.Draw(ShadowTile.Textures[index][subType], posX, posY);
				}
			}
		}
	}
}
