using Microsoft.Xna.Framework.Graphics;
using Nexus.Engine;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public static class ActionMap {

		// Character Actions
		public static readonly AirBurst AirBurst = new AirBurst();
		public static readonly FlightAction Flight = new FlightAction();
		public static readonly WallGrabAction WallGrab = new WallGrabAction();
		public static readonly HoverAction Hover = new HoverAction();
		public static readonly JumpAction Jump = new JumpAction();
		public static readonly SlideAction Slide = new SlideAction();
		public static readonly WallJumpAction WallJump = new WallJumpAction();
	}

	public class AnimationMap {
		public AnimGlobal AnimationGlobal = new AnimGlobal();
	}

	public static class AnimCycleMap {
		public static readonly string[] NoAnimation = new string[0] {};
		public static readonly string[] Cycle2 = new string[2] { "1", "2" };
		public static readonly string[] Cycle3 = new string[3] { "1", "2", "3" };
		public static readonly string[] Cycle3Reverse = new string[4] { "1", "2", "3", "2" };
		public static readonly string[] Cycle4 = new string[4] { "1", "2", "3", "4" };

		// Character-Specific Animations
		public static readonly string[] CharacterRunLeft = new string[2] { "RunLeft", "StandRunLeft" };
		public static readonly string[] CharacterRunRight = new string[2] { "Run", "StandRun" };
		public static readonly string[] CharacterWalkLeft = new string[2] { "WalkLeft", "StandLeft" };
		public static readonly string[] CharacterWalkRight = new string[2] { "Walk", "Stand" };
	}

	public static class HeadMap {
		public static readonly RyuHead RyuHead = new RyuHead();
	}

	public class GameMapper {

		public readonly Atlas[] atlas;
		public Dictionary<MetaGroup, IMetaData> MetaList = new Dictionary<MetaGroup, IMetaData>();
		// TileMap, FGTileMap, ObjectMap
		
		public GameMapper(GameClient game, SpriteBatch spriteBatch) {

			// Create Atlas List
			this.atlas = new Atlas[3];
			this.atlas[(byte)AtlasGroup.Tiles] = new Atlas(game, spriteBatch, "Atlas/Tiles.png");
			this.atlas[(byte)AtlasGroup.Objects] = new Atlas(game, spriteBatch, "Atlas/Objects.png");
			this.atlas[(byte)AtlasGroup.World] = new Atlas(game, spriteBatch, "Atlas/World.png");

			// List of Game Object Metadata
			MetaList[MetaGroup.Character] = new IMetaData(Arch.Character, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Interactives, LayerEnum.Main, LoadOrder.Character);
			MetaList[MetaGroup.Ground] = new IMetaData(Arch.Ground, this.atlas[(byte) AtlasGroup.Tiles], SlotGroup.Blocks, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Block
				// All Ground, Leaf, Lock, GrowObj
			MetaList[MetaGroup.Ledge] = new IMetaData(Arch.Platform, this.atlas[(byte) AtlasGroup.Tiles], SlotGroup.Blocks, LayerEnum.Main, LoadOrder.Platform);
			MetaList[MetaGroup.Decor] = new IMetaData(Arch.Decor, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.Prompts, LayerEnum.Cosmetic, LoadOrder.Tile); // LoadOrder.Decor
			MetaList[MetaGroup.Block] = new IMetaData(Arch.Block, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.Blocks, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Block
				// PuffBlock, Exclaim, Box, Brick
			MetaList[MetaGroup.ToggleBlock] = new IMetaData(Arch.ToggleBlock, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.ColorToggles, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Block
				// ToggleBlock, ToggleOnPlat, ToggleOffPlat, ToggleOn, ToggleOff, 
			MetaList[MetaGroup.Conveyor] = new IMetaData(Arch.Block, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.Movers, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Block
				// Conveyor
			MetaList[MetaGroup.Platform] = new IMetaData(Arch.Platform, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Movers, LayerEnum.Main, LoadOrder.Platform);
				// PlatSolid, PlatMove, PlatFall, PlatDip, PlatDelay
			MetaList[MetaGroup.EnemyFixed] = new IMetaData(Arch.Enemy, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Fixed, LayerEnum.Main, LoadOrder.Enemy);
				// Chomper, Fire Chomper, Plant
			MetaList[MetaGroup.EnemyLand] = new IMetaData(Arch.Enemy, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.EnemyLand, LayerEnum.Main, LoadOrder.Enemy);
			MetaList[MetaGroup.EnemyFly] = new IMetaData(Arch.Enemy, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.EnemyFly, LayerEnum.Main, LoadOrder.Enemy);
			MetaList[MetaGroup.BlockMoving] = new IMetaData(Arch.MovingBlock, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.EnemyFly, LayerEnum.Main, LoadOrder.Enemy);
				// Slammer
			MetaList[MetaGroup.Item] = new IMetaData(Arch.Item, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Items, LayerEnum.Main, LoadOrder.Item);
				// Most Items (but not buttons)
			MetaList[MetaGroup.Button] = new IMetaData(Arch.Item, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.ColorToggles, LayerEnum.Main, LoadOrder.Item);
				// Buttons
			MetaList[MetaGroup.Generator] = new IMetaData(Arch.Generator, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Fixed, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Block
				// Cannon, Placer
			MetaList[MetaGroup.Collectable] = new IMetaData(Arch.Collectable, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.Collectables, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Collectable
				// Collectables
			MetaList[MetaGroup.Track] = new IMetaData(Arch.HiddenObject, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Movers, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Invisible
				// Track, Clusuter
			MetaList[MetaGroup.Door] = new IMetaData(Arch.Portal, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Interactives, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Portal
			MetaList[MetaGroup.Interactives] = new IMetaData(Arch.Interactives, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Interactives, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Interactives
				// Chest, PeekMap
			MetaList[MetaGroup.Flag] = new IMetaData(Arch.Collectable, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Interactives, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Interactives
			MetaList[MetaGroup.NPC] = new IMetaData(Arch.Interactives, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Interactives, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Interactives
			MetaList[MetaGroup.Projectile] = new IMetaData(Arch.Projectile, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.None, LayerEnum.Main, LoadOrder.Projectile); // LoadOrder.Interactives
		}

		// List of Tile Types
		// This includes any tile that isn't "foreground", including full-space tiles.
		// Also includes fixed objects that can accept tile behavior for collision detection.
		public Dictionary<byte, Type> TileMap = new Dictionary<byte, Type>() {
			
			// Ground, Immutable (0 - 9)
			{ 1, Type.GetType("Nexus.Objects.GroundGrass") },
			{ 2, Type.GetType("Nexus.Objects.GroundDirt") },
			{ 3, Type.GetType("Nexus.Objects.GroundMud") },
			{ 4, Type.GetType("Nexus.Objects.GroundStone") },
			{ 5, Type.GetType("Nexus.Objects.GroundSnow") },
			{ 6, Type.GetType("Nexus.Objects.GroundSlime") },
			{ 7, Type.GetType("Nexus.Objects.GroundCloud") },
			
			// Ground-Esque, Immutable (10 - 19)
			{ 10, Type.GetType("Nexus.Objects.Wall") },
			{ 11, Type.GetType("Nexus.Objects.Log") },
			
			// Ledges (20 - 29)
			{ 20, Type.GetType("Nexus.Objects.LedgeGrass") },
			{ 25, Type.GetType("Nexus.Objects.PlatformFixed") },
			{ 26, Type.GetType("Nexus.Objects.PlatformItem") },
			
			// Reserved (30 - 39)

			// Fixed, Touch-Effect (40 - 49)
			{ 40, Type.GetType("Nexus.Objects.Brick") },
			{ 41, Type.GetType("Nexus.Objects.Box") },
			{ 42, Type.GetType("Nexus.Objects.Lock") },
			{ 43, Type.GetType("Nexus.Objects.Leaf") },
			{ 44, Type.GetType("Nexus.Objects.ExclaimBlock") },
			// ...
			{ 46, Type.GetType("Nexus.Objects.Spike") },
			{ 47, Type.GetType("Nexus.Objects.PuffBlock") },
			{ 48, Type.GetType("Nexus.Objects.Conveyor") },

			// Solid, Toggled (50 - 55)
			{ 50, Type.GetType("Nexus.Objects.ToggleBoxBR") },
			{ 51, Type.GetType("Nexus.Objects.ToggleBoxGY") },
			{ 52, Type.GetType("Nexus.Objects.ToggleBlockBlue") },
			{ 53, Type.GetType("Nexus.Objects.ToggleBlockRed") },
			{ 54, Type.GetType("Nexus.Objects.ToggleBlockGreen") },
			{ 55, Type.GetType("Nexus.Objects.ToggleBlockYellow") },
			
			// Solid, Toggled Platforms (56 - 59)
			{ 56, Type.GetType("Nexus.Objects.TogglePlatBlue") },
			{ 57, Type.GetType("Nexus.Objects.TogglePlatRed") },
			{ 58, Type.GetType("Nexus.Objects.TogglePlatGreen") },
			{ 59, Type.GetType("Nexus.Objects.TogglePlatYellow") },

			// Generators (60 - 64)
			{ 60, Type.GetType("Nexus.Objects.Cannon") },
			{ 61, Type.GetType("Nexus.Objects.Placer") },

			// Reserved (65 - 69)
			
			// Anything below this section has an ObjectID, possibly Update(), and Passive Collision.
			
			// Tile-Based Creatures (70 - 79)
			{ 70, Type.GetType("Nexus.Objects.Plant") },
			{ 71, Type.GetType("Nexus.Objects.ChomperGrass") },
			{ 72, Type.GetType("Nexus.Objects.ChomperMetal") },
			{ 73, Type.GetType("Nexus.Objects.ChomperFire") },

			// Anything below can only be interacted with by a character:
			// These will have Passive Collision by Character Only.

			// Character Interactives (150 - 159)
			{ 150, Type.GetType("Nexus.Objects.Flag") },
			{ 151, Type.GetType("Nexus.Objects.Chest") },
			{ 152, Type.GetType("Nexus.Objects.NPC") },
			{ 153, Type.GetType("Nexus.Objects.PeekMap") },

			{ 155, Type.GetType("Nexus.Objects.Door") },
			{ 156, Type.GetType("Nexus.Objects.DoorLock") },
			
			// Collectables (160 - 169)
			{ 160, Type.GetType("Nexus.Objects.Coins") },
			{ 161, Type.GetType("Nexus.Objects.Goodie") },
			{ 162, Type.GetType("Nexus.Objects.CollectableSuit") },
			{ 163, Type.GetType("Nexus.Objects.CollectableHat") },
			{ 164, Type.GetType("Nexus.Objects.CollectablePower") },
			
		};
		
		// List of Foreground Tile Types
		// This includes decorations, prompts, or tiles that appear in the front.
		public Dictionary<byte, Type> FGTileMap = new Dictionary<byte, Type>() {
			
			// Decor, Terrain (1 - 19)
			{ 1, Type.GetType("Nexus.Objects.DecorVeg") },
			{ 2, Type.GetType("Nexus.Objects.DecorDesert") },
			{ 3, Type.GetType("Nexus.Objects.DecorCave") },
			{ 4, Type.GetType("Nexus.Objects.DecorWater") },

			// Decor, Misc (20 - 29)
			{ 20, Type.GetType("Nexus.Objects.DecorPet") },
			{ 21, Type.GetType("Nexus.Objects.DecorItems") },

			// Prompts (30 - 39)
			{ 30, Type.GetType("Nexus.Objects.PromptArrow") },
			{ 31, Type.GetType("Nexus.Objects.PromptSign") },

			// Background Interactives (These Collide)
			{ 40, Type.GetType("Nexus.Objects.BGDisable") },
			{ 41, Type.GetType("Nexus.Objects.BGTap") },
			{ 42, Type.GetType("Nexus.Objects.BGWind") },
		};

		// List of Game Object Types
		public Dictionary<byte, Type> ObjectMap = new Dictionary<byte, Type>() {

			// Platforms (1 - 4)
			{ 1, Type.GetType("Nexus.Objects.PlatformDip") },
			{ 2, Type.GetType("Nexus.Objects.PlatformDelay") },
			{ 3, Type.GetType("Nexus.Objects.PlatformFall") },
			{ 4, Type.GetType("Nexus.Objects.PlatformMove") },
			
			// Tracks (5 - 9)
			{ 5, Type.GetType("Nexus.Objects.Cluster") },
			{ 6, Type.GetType("Nexus.Objects.Track") },
			
			// Land & Fixed Enemies (10 - 39)
			{ 10, Type.GetType("Nexus.Objects.Moosh") },
			{ 11, Type.GetType("Nexus.Objects.Shroom") },
			{ 12, Type.GetType("Nexus.Objects.Bug") },
			{ 13, Type.GetType("Nexus.Objects.Goo") },
			{ 14, Type.GetType("Nexus.Objects.Liz") },
			{ 15, Type.GetType("Nexus.Objects.Snek") },
			{ 16, Type.GetType("Nexus.Objects.Wurm") },
			{ 17, Type.GetType("Nexus.Objects.Octo") },
			{ 18, Type.GetType("Nexus.Objects.Bones") },

			{ 20, Type.GetType("Nexus.Objects.Turtle") },
			{ 21, Type.GetType("Nexus.Objects.Snail") },
			{ 22, Type.GetType("Nexus.Objects.Boom") },

			{ 25, Type.GetType("Nexus.Objects.Poke") },
			{ 26, Type.GetType("Nexus.Objects.Lich") },

			{ 30, Type.GetType("Nexus.Objects.Chomper") },
			{ 31, Type.GetType("Nexus.Objects.ChomperFire") },
			{ 32, Type.GetType("Nexus.Objects.Plant") },

			// Flight Enemies (40 - 69)
			{ 40, Type.GetType("Nexus.Objects.Ghost") },
			{ 41, Type.GetType("Nexus.Objects.FlairElectric") },
			{ 42, Type.GetType("Nexus.Objects.FlairFire") },
			{ 43, Type.GetType("Nexus.Objects.FlairMagic") },

			{ 45, Type.GetType("Nexus.Objects.ElementalAir") },
			{ 46, Type.GetType("Nexus.Objects.ElementalEarth") },
			{ 47, Type.GetType("Nexus.Objects.ElementalFire") },

			{ 48, Type.GetType("Nexus.Objects.Buzz") },

			{ 60, Type.GetType("Nexus.Objects.Saw") },
			{ 61, Type.GetType("Nexus.Objects.Slammer") },
			{ 62, Type.GetType("Nexus.Objects.ElementalEye") },
			{ 63, Type.GetType("Nexus.Objects.WallBouncer") },

			{ 65, Type.GetType("Nexus.Objects.Dire") },

			// Items, Fixed (70 - 79)
			{ 70, Type.GetType("Nexus.Objects.SpringFixed") },
			{ 71, Type.GetType("Nexus.Objects.ButtonFixed") },

			// Items, Mobile (80 - 99)
			{ 80, Type.GetType("Nexus.Objects.Shell") },
			{ 81, Type.GetType("Nexus.Objects.Boulder") },
			{ 82, Type.GetType("Nexus.Objects.Bomb") },

			{ 85, Type.GetType("Nexus.Objects.TNT") },

			{ 90, Type.GetType("Nexus.Objects.SpringStandard") },
			{ 91, Type.GetType("Nexus.Objects.ButtonStandard") },
			{ 92, Type.GetType("Nexus.Objects.ButtonTimed") },

			{ 95, Type.GetType("Nexus.Objects.MobileBlockBlue") },
			{ 96, Type.GetType("Nexus.Objects.MobileBlockRed") },
			{ 97, Type.GetType("Nexus.Objects.MobileBlockGreen") },
			{ 98, Type.GetType("Nexus.Objects.MobileBlockYellow") },

			// Special Flags and Placements (100+)
			{ 100, Type.GetType("Nexus.Objects.Character") },
		};
	}
}
