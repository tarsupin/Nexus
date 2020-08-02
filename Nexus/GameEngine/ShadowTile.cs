using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;
using static Nexus.Objects.Bomb;
using static Nexus.Objects.Boulder;
using static Nexus.Objects.ClusterDot;
using static Nexus.Objects.ElementalAir;
using static Nexus.Objects.ElementalEarth;
using static Nexus.Objects.ElementalFire;
using static Nexus.Objects.OrbItem;
using static Nexus.Objects.Platform;
using static Nexus.Objects.Shell;
using static Nexus.Objects.SportBall;
using static Nexus.Objects.SpringHeld;
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
				{  (byte) GhostSubType.Slimer, "Ghost/Slimer/Left" },
			}},
			
			// FlairElectric
			{ (byte) ObjectEnum.FlairNormal, new Dictionary<byte, string> {
				{  (byte) FlairNormalSubType.Normal, "Flair/Norm/Left2" },
			}},
			
			// FlairElectric
			{ (byte) ObjectEnum.FlairElectric, new Dictionary<byte, string> {
				{  (byte) FlairElectricSubType.Normal, "Flair/Electric/Left2" },
			}},

			// FlairFire
			{ (byte) ObjectEnum.FlairFire, new Dictionary<byte, string> {
				{  (byte) FlairFireSubType.Normal, "Flair/Fire/Left2" },
			}},
			
			// FlairPoison
			{ (byte) ObjectEnum.FlairPoison, new Dictionary<byte, string> {
				{  (byte) FlairPoisonSubType.Normal, "Flair/Poison/Left2" },
			}},
			
			// ElementalAir
			{ (byte) ObjectEnum.ElementalAir, new Dictionary<byte, string> {
				{  (byte) ElementalAirSubType.Left, "Elemental/Air/Left" },
				{  (byte) ElementalAirSubType.Right, "Elemental/Air/Right" },
			}},

			// ElementalEarth
			{ (byte) ObjectEnum.ElementalEarth, new Dictionary<byte, string> {
				{  (byte) ElementalEarthSubType.Left, "Elemental/Earth/Left" },
				{  (byte) ElementalEarthSubType.Right, "Elemental/Earth/Right" },
			}},

			// ElementalFire
			{ (byte) ObjectEnum.ElementalFire, new Dictionary<byte, string> {
				{  (byte) ElementalFireSubType.Left, "Elemental/Fire/Left" },
				{  (byte) ElementalFireSubType.Right, "Elemental/Fire/Right" },
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
				{  (byte) PlatformSubTypes.W1, "Platform/Dip/W1" },
				{  (byte) PlatformSubTypes.W2, "Platform/Dip/W2" },
			}},
			
			// PlatformDelay
			{ (byte) ObjectEnum.PlatformDelay, new Dictionary<byte, string> {
				{  (byte) PlatformSubTypes.W1, "Platform/Delay/W1" },
				{  (byte) PlatformSubTypes.W2, "Platform/Delay/W2" },
			}},
			
			// PlatformFall
			{ (byte) ObjectEnum.PlatformFall, new Dictionary<byte, string> {
				{  (byte) PlatformSubTypes.W1, "Platform/Fall/W1" },
				{  (byte) PlatformSubTypes.W2, "Platform/Fall/W2" },
			}},
			
			// PlatformMove
			{ (byte) ObjectEnum.PlatformMove, new Dictionary<byte, string> {
				{  (byte) PlatformSubTypes.W1, "Platform/Move/W1" },
				{  (byte) PlatformSubTypes.W2, "Platform/Move/W2" },
			}},

			///////////////////
			////// ITEMS //////
			///////////////////
			
			// Shell
			{ (byte) ObjectEnum.Shell, new Dictionary<byte, string> {
				{  (byte) ShellSubType.Green, "Shell/Green/Spin1" },
				{  (byte) ShellSubType.GreenWing, "Shell/GreenWing/Spin1" },
				{  (byte) ShellSubType.Heavy, "Shell/Heavy/Spin1" },
				{  (byte) ShellSubType.Red, "Shell/Red/Spin1" },
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
			
			// Orb
			{ (byte) ObjectEnum.OrbItem, new Dictionary<byte, string> {
				{  (byte) OrbSubType.Magic, "Orb/Magic" },
			}},

			// Sport Ball
			{ (byte) ObjectEnum.SportBall, new Dictionary<byte, string> {
				{  (byte) SportBallSubType.Fire, "Orb/Fire" },
				{  (byte) SportBallSubType.Earth, "Orb/Earth" },
				{  (byte) SportBallSubType.Forest, "Orb/Forest" },
				{  (byte) SportBallSubType.Water, "Orb/Water" },
			}},
			
			// Spring - Standard
			{ (byte) ObjectEnum.SpringHeld, new Dictionary<byte, string> {
				{  (byte) SpringHeldSubType.Norm, "Spring/Up" },
			}},

			// Button - Standard
			{ (byte) ObjectEnum.ButtonHeld, new Dictionary<byte, string> {
				{  (byte) ButtonSubTypes.BR, "Button/BR" },
				{  (byte) ButtonSubTypes.GY, "Button/GY" },
			}},
			
			/////////////////////////////
			////// SPECIAL OBJECTS //////
			/////////////////////////////
			
			{ (byte) ObjectEnum.Character, new Dictionary<byte, string> {
				{  (byte) 0, "Head/Lana/Left" },
			}},

			// Special Objects
			{ (byte) ObjectEnum.ClusterDot, new Dictionary<byte, string> {
				{  (byte) ClusterDotSubType.Basic, "Cluster/Basic" },
			}},
		};

		public static Dictionary<byte, Dictionary<byte, string[]>> ObjHelpText = new Dictionary<byte, Dictionary<byte, string[]>> {

			//////////////////////////
			////// LAND ENEMIES //////
			//////////////////////////

			// Moosh
			{ (byte) ObjectEnum.Moosh, new Dictionary<byte, string[]> {
				{  (byte) MooshSubType.Brown, new string[] { "Brown Moosh", "Small enemy that performs a long jump when it sees a character." } },
				{  (byte) MooshSubType.Purple, new string[] { "Purple Moosh", "Small enemy that performs a tall jump when it sees a character." } },
				{  (byte) MooshSubType.White, new string[] { "White Moosh", "Small enemy that constantly hops." } },
			}},

			// Shroom
			{ (byte) ObjectEnum.Shroom, new Dictionary<byte, string[]> {
				{  (byte) ShroomSubType.Black, new string[2] { "Black Shroom", "Remains in one spot, hopping up and down." }  },
				{  (byte) ShroomSubType.Purple, new string[2] { "Purple Shroom", "Remains still, then leaps when it sees a character." }  },
				{  (byte) ShroomSubType.Red, new string[2] { "Red Shroom", "Walks back and forth." }  },
			}},

			// Bug
			{ (byte) ObjectEnum.Bug, new Dictionary<byte, string[]> {
				{  (byte) BugSubType.Bug, new string[2] { "Bug", "" } },
			}},

			// Goo
			{ (byte) ObjectEnum.Goo, new Dictionary<byte, string[]> {
				{  (byte) GooSubType.Green, new string[2] { "Green Goo", "Walks back and forth. Moves slowly." } },
				{  (byte) GooSubType.Orange, new string[2] { "Orange Goo", "Walks back and forth. Moves quickly." } },
				{  (byte) GooSubType.Blue, new string[2] { "Blue Goo", "Remains in one spot, hopping up and down." } },
			}},

			// Liz
			{ (byte) ObjectEnum.Liz, new Dictionary<byte, string[]> {
				{  (byte) LizSubType.Liz, new string[2] { "Liz", "Walks back and forth. Will charge if it sees a character." } },
			}},

			// Snek
			{ (byte) ObjectEnum.Snek, new Dictionary<byte, string[]> {
				{  (byte) SnekSubType.Snek, new string[2] { "Snek", "Walks back and forth." } },
				{  (byte) SnekSubType.Wurm, new string[2] { "Wurm", "Walks back and forth." } },
			}},

			// Octo
			{ (byte) ObjectEnum.Octo, new Dictionary<byte, string[]> {
				{  (byte) OctoSubType.Octo, new string[2] { "Octo", "Walks back and forth." } },
			}},

			// Bones
			{ (byte) ObjectEnum.Bones, new Dictionary<byte, string[]> {
				{  (byte) BonesSubType.Bones, new string[2] { "Bones", "Walks back and forth. Shatters into bones if jumped on." } },
			}},

			// Turtle
			{ (byte) ObjectEnum.Turtle, new Dictionary<byte, string[]> {
				{  (byte) TurtleSubType.Red, new string[2] { "Turtle", "Walks back and forth. Leaves a shell when jumped on." } },
			}},

			// Snail
			{ (byte) ObjectEnum.Snail, new Dictionary<byte, string[]> {
				{  (byte) SnailSubType.Snail, new string[2] { "Snail", "Walks back and forth. Leaves a shell when jumped on." } },
			}},

			// Boom
			{ (byte) ObjectEnum.Boom, new Dictionary<byte, string[]> {
				{  (byte) BoomSubType.Boom, new string[2] { "Boom", "Walks back and forth. Leaves an active bomb when jumped on." } },
			}},

			// Poke
			{ (byte) ObjectEnum.Poke, new Dictionary<byte, string[]> {
				{  (byte) PokeSubType.Poke, new string[2] { "Poke", "Walks back and forth. Tall enemy." } },
			}},

			// Lich
			{ (byte) ObjectEnum.Lich, new Dictionary<byte, string[]> {
				{  (byte) LichSubType.Lich, new string[2] { "Lich", "Large enemy. Walks back and forth." } },
			}},

			////////////////////////////
			////// FLIGHT ENEMIES //////
			////////////////////////////

			// Buzz
			{ (byte) ObjectEnum.Buzz, new Dictionary<byte, string[]> {
				{  (byte) BuzzSubType.Buzz, new string[2] { "Buzz", "Flying creature. Can jump on top of it." } },
			}},

			// Ghost
			{ (byte) ObjectEnum.Ghost, new Dictionary<byte, string[]> {
				{  (byte) GhostSubType.Norm, new string[2] { "Ghost", "Can chase. Cannot be killed. Causes damage on contact." } },
				{  (byte) GhostSubType.Hide, new string[2] { "Intangible Ghost", "Deals no damage. Typically used as a cluster. Can chase." } },
				{  (byte) GhostSubType.Hat, new string[2] { "Helmet Ghost", "Can chase and damage characters. Characters can safely bounce on top of it." } },
				{  (byte) GhostSubType.Slimer, new string[2] { "Slimer", "A solid ghost." } },
			}},
			
			// FlairElectric
			{ (byte) ObjectEnum.FlairNormal, new Dictionary<byte, string[]> {
				{  (byte) FlairNormalSubType.Normal, new string[2] { "Flair", "Flying creature that can be damaged." } },
			}},
			
			// FlairElectric
			{ (byte) ObjectEnum.FlairElectric, new Dictionary<byte, string[]> {
				{  (byte) FlairElectricSubType.Normal, new string[2] { "Electric Flair", "Flying creature, can be damaged. Shoots electric balls." } },
			}},

			// FlairFire
			{ (byte) ObjectEnum.FlairFire, new Dictionary<byte, string[]> {
				{  (byte) FlairFireSubType.Normal, new string[2] { "Fire Flair", "Flying creature, can be damaged. Shoots fireballs." } },
			}},
			
			// FlairPoison
			{ (byte) ObjectEnum.FlairPoison, new Dictionary<byte, string[]> {
				{  (byte) FlairPoisonSubType.Normal, new string[2] { "Poison Flair", "Flying creature, can be damaged. Shoots poison balls downward." } },
			}},
			
			// ElementalAir
			{ (byte) ObjectEnum.ElementalAir, new Dictionary<byte, string[]> {
				{  (byte) ElementalAirSubType.Left, new string[2] { "Air Elemental", "Flying creature, cannot be harmed." } },
				{  (byte) ElementalAirSubType.Right, new string[2] { "Air Elemental", "Flying creature, cannot be harmed." } },
			}},

			// ElementalEarth
			{ (byte) ObjectEnum.ElementalEarth, new Dictionary<byte, string[]> {
				{  (byte) ElementalEarthSubType.Left, new string[2] { "Earth Elemental", "Flying creature, cannot be harmed. Shoots earth projectiles downward." } },
				{  (byte) ElementalEarthSubType.Right, new string[2] { "Earth Elemental", "Flying creature, cannot be harmed. Shoots earth projectiles downward." } },
			}},

			// ElementalFire
			{ (byte) ObjectEnum.ElementalFire, new Dictionary<byte, string[]> {
				{  (byte) ElementalFireSubType.Left, new string[2] { "Fire Elemental", "Flying creature, cannot be harmed. Shoots fireballs." } },
				{  (byte) ElementalFireSubType.Right, new string[2] { "Fire Elemental", "Flying creature, cannot be harmed. Shoots fireballs." } },
			}},
			
			// Saw
			{ (byte) ObjectEnum.Saw, new Dictionary<byte, string[]> {
				{  (byte) SawSubType.Small, new string[2] { "Small Saw", "Flying movement, damages characters on touch." } },
				{  (byte) SawSubType.Large, new string[2] { "Large Saw", "Flying movement, damages characters on touch." } },
				{  (byte) SawSubType.LethalSmall, new string[2] { "Small Lethal Saw", "Flying movement, kills characters on touch." } },
				{  (byte) SawSubType.LethalLarge, new string[2] { "Large Lethal Saw", "Flying movement, kills characters on touch." } },
			}},
			
			// Slammer
			{ (byte) ObjectEnum.Slammer, new Dictionary<byte, string[]> {
				{  (byte) SlammerSubType.Slammer, new string[2] { "Slammer", "Large enemy. Acts like a solid block but will slam down on characters." } },
			}},
			
			// HoveringEye
			{ (byte) ObjectEnum.HoveringEye, new Dictionary<byte, string[]> {
				{  (byte) HoveringEyeSubType.Eye, new string[2] { "Hovering Eye", "Flying motion. Fires projectiles directly at nearby characters." } },
			}},

			// Bouncer
			{ (byte) ObjectEnum.Bouncer, new Dictionary<byte, string[]> {
				{  (byte) BouncerSubType.Normal, new string[2] { "Bouncer", "Bounces off of walls at 90 degree angles." } },
			}},
			
			// Dire
			{ (byte) ObjectEnum.Dire, new Dictionary<byte, string[]> {
				{  (byte) DireSubType.Dire, new string[2] { "Dire", "A large flying creature." } },
			}},

			///////////////////////////////
			////// DYNAMIC PLATFORMS //////
			///////////////////////////////
			
			// PlatformDip
			{ (byte) ObjectEnum.PlatformDip, new Dictionary<byte, string[]> {
				{  (byte) PlatformSubTypes.W1, new string[2] { "Dip Platform", "Will dip down when used, but returns back to its original position." } },
				{  (byte) PlatformSubTypes.W2, new string[2] { "Dip Platform", "Will dip down when used, but returns back to its original position." } },
			}},
			
			// PlatformDelay
			{ (byte) ObjectEnum.PlatformDelay, new Dictionary<byte, string[]> {
				{  (byte) PlatformSubTypes.W1, new string[2] { "Delay Platform", "Falls after a short delay once landed on." } },
				{  (byte) PlatformSubTypes.W2, new string[2] { "Delay Platform", "Falls after a short delay once landed on." } },
			}},
			
			// PlatformFall
			{ (byte) ObjectEnum.PlatformFall, new Dictionary<byte, string[]> {
				{  (byte) PlatformSubTypes.W1, new string[2] { "Falling Platform", "Begins to fall once landed on." } },
				{  (byte) PlatformSubTypes.W2, new string[2] { "Falling Platform", "Begins to fall once landed on." } },
			}},
			
			// PlatformMove
			{ (byte) ObjectEnum.PlatformMove, new Dictionary<byte, string[]> {
				{  (byte) PlatformSubTypes.W1, new string[2] { "Moving Platform", "Assign flying behaviors, tracks, clusters, and other controls." } },
				{  (byte) PlatformSubTypes.W2, new string[2] { "Moving Platform", "Assign flying behaviors, tracks, clusters, and other controls." } },
			}},

			///////////////////
			////// ITEMS //////
			///////////////////
			
			// Shell
			{ (byte) ObjectEnum.Shell, new Dictionary<byte, string[]> {
				{  (byte) ShellSubType.Green, new string[2] { "Green Shell", "A standard shell. Throw it, kick it, drop it, toss it." } },
				{  (byte) ShellSubType.GreenWing, new string[2] { "Winged Shell", "A light shell. Stays airborne longer, less affected by gravity." } },
				{  (byte) ShellSubType.Heavy, new string[2] { "Heavy Shell", "A heavy shell. Doesn't stay airborne as long." } },
				{  (byte) ShellSubType.Red, new string[2] { "Red Shell", "A standard shell. Throw it, kick it, drop it, toss it." } },
			}},
			
			// Boulder
			{ (byte) ObjectEnum.Boulder, new Dictionary<byte, string[]> {
				{  (byte) BoulderSubType.Boulder, new string[2] { "Boulder", "A solid item that acts like a moveable block." } },
			}},
			
			// Bomb
			{ (byte) ObjectEnum.Bomb, new Dictionary<byte, string[]> {
				{  (byte) BombSubType.Bomb, new string[2] { "Bomb", "An item that can be set to explode for significant damage." } },
			}},
			
			// TNT
			{ (byte) ObjectEnum.TNT, new Dictionary<byte, string[]> {
				{  (byte) TNTSubType.TNT, new string[2] { "TNT", "Damages all enemies on the screen when activated while held." } },
			}},
			
			// Orb
			{ (byte) ObjectEnum.OrbItem, new Dictionary<byte, string[]> {
				{  (byte) OrbSubType.Magic, new string[2] { "Orb", "A mysterious orb with intreguing qualities." } },
			}},

			// Sport Ball
			{ (byte) ObjectEnum.SportBall, new Dictionary<byte, string[]> {
				{  (byte) SportBallSubType.Fire, new string[2] { "Fire Sport Ball", "A very bouncy, very reactive sports ball." } },
				{  (byte) SportBallSubType.Earth, new string[2] { "Earth Sport Ball", "A heavier sports ball, less bouncy." } },
				{  (byte) SportBallSubType.Forest, new string[2] { "Forest Sport Ball", "A bouncy, reactive sports ball." } },
				{  (byte) SportBallSubType.Water, new string[2] { "Water Sport Ball", "A slightly slower sports ball." } },
			}},
			
			// Spring - Standard
			{ (byte) ObjectEnum.SpringHeld, new Dictionary<byte, string[]> {
				{  (byte) SpringHeldSubType.Norm, new string[2] { "Mobile Spring", "A spring that can be picked up and moved." } },
			}},

			// Button - Standard
			{ (byte) ObjectEnum.ButtonHeld, new Dictionary<byte, string[]> {
				{  (byte) ButtonSubTypes.BR, new string[2] { "Mobile Blue-Red Button", "A mobile button that toggles the Blue-Red colors when activated." } },
				{  (byte) ButtonSubTypes.GY, new string[2] { "Mobile Green-Yellow Button", "A mobile button that toggles the Green-Yellow colors when activated." } },
			}},
			
			/////////////////////////////
			////// SPECIAL OBJECTS //////
			/////////////////////////////
			
			{ (byte) ObjectEnum.Character, new Dictionary<byte, string[]> {
				{  (byte) 0, new string[2] { "Character", "The starting location of a character." } },
			}},

			// Special Objects
			{ (byte) ObjectEnum.ClusterDot, new Dictionary<byte, string[]> {
				{  (byte) ClusterDotSubType.Basic, new string[2] { "Mobile Cluster", "An invisible, flying object that ONLY acts as a cluster for other objects." } },
			}},
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
