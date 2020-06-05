using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;
using static Nexus.Objects.Bomb;
using static Nexus.Objects.Boulder;
using static Nexus.Objects.Shell;
using static Nexus.Objects.SpringStandard;
using static Nexus.Objects.TNT;

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
				{  (byte) SawSubType.LethalSmall, "Saw/Lethal" },
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

			// Bouncer
			{ (byte) ObjectEnum.Bouncer, new Dictionary<byte, string> {
				{  (byte) BouncerSubType.Normal, "Bouncer/Norm" },
			}},
			
			// Dire
			{ (byte) ObjectEnum.Dire, new Dictionary<byte, string> {
				{  (byte) DireSubType.Dire, "Dire/Left1" },
			}},

			///////////////////////////////
			////// DYNAMIC PLATFORMS //////
			///////////////////////////////
			
			// PlatformDip
			{ (byte) ObjectEnum.PlatformDip, new Dictionary<byte, string> {
				{  (byte) HorizontalSubTypes.S, "Platform/Dip/S" },
				{  (byte) HorizontalSubTypes.H1, "Platform/Dip/H1" },
				{  (byte) HorizontalSubTypes.H2, "Platform/Dip/H2" },
				{  (byte) HorizontalSubTypes.H3, "Platform/Dip/H3" },
			}},
			
			// PlatformDelay
			{ (byte) ObjectEnum.PlatformDelay, new Dictionary<byte, string> {
				{  (byte) HorizontalSubTypes.S, "Platform/Delay/S" },
				{  (byte) HorizontalSubTypes.H1, "Platform/Delay/H1" },
				{  (byte) HorizontalSubTypes.H2, "Platform/Delay/H2" },
				{  (byte) HorizontalSubTypes.H3, "Platform/Delay/H3" },
			}},
			
			// PlatformFall
			{ (byte) ObjectEnum.PlatformFall, new Dictionary<byte, string> {
				{  (byte) HorizontalSubTypes.S, "Platform/Fall/S" },
				{  (byte) HorizontalSubTypes.H1, "Platform/Fall/H1" },
				{  (byte) HorizontalSubTypes.H2, "Platform/Fall/H2" },
				{  (byte) HorizontalSubTypes.H3, "Platform/Fall/H3" },
			}},
			
			// PlatformMove
			{ (byte) ObjectEnum.PlatformMove, new Dictionary<byte, string> {
				{  (byte) HorizontalSubTypes.S, "Platform/Move/S" },
				{  (byte) HorizontalSubTypes.H1, "Platform/Move/H1" },
				{  (byte) HorizontalSubTypes.H2, "Platform/Move/H2" },
				{  (byte) HorizontalSubTypes.H3, "Platform/Move/H3" },
			}},

			///////////////////
			////// ITEMS //////
			///////////////////
			
			// Shell
			{ (byte) ObjectEnum.Shell, new Dictionary<byte, string> {
				{  (byte) ShellSubType.Green, "Shell/Green/Side" },
				{  (byte) ShellSubType.GreenWing, "Shell/GreenWing/Side" },
				{  (byte) ShellSubType.Heavy, "Shell/Heavy/Side" },
				{  (byte) ShellSubType.Red, "Shell/Red/Side" },
			}},
			
			// Boulder
			{ (byte) ObjectEnum.Boulder, new Dictionary<byte, string> {
				{  (byte) BoulderSubType.Boulder, "Items/Boulder" },
			}},
			
			// Bomb
			{ (byte) ObjectEnum.Bomb, new Dictionary<byte, string> {
				{  (byte) BombSubType.Bomb, "Items/Bomb1" },
			}},
			
			// TNT
			{ (byte) ObjectEnum.TNT, new Dictionary<byte, string> {
				{  (byte) TNTSubType.TNT, "Items/TNT" },
			}},
			
			// Spring - Standard
			{ (byte) ObjectEnum.SpringStandard, new Dictionary<byte, string> {
				{  (byte) SpringStandardSubType.Norm, "Spring/Up" },
			}},

			// Button - Standard
			{ (byte) ObjectEnum.ButtonStandard, new Dictionary<byte, string> {
				{  (byte) ButtonSubTypes.BR, "Button/BR" },
				{  (byte) ButtonSubTypes.GY, "Button/GY" },
			}},
			
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
