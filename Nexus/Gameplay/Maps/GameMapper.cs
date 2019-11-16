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
			{ (byte) TileEnum.GroundGrass, Type.GetType("Nexus.Objects.GroundGrass") },
			{ (byte) TileEnum.GroundDirt, Type.GetType("Nexus.Objects.GroundDirt") },
			{ (byte) TileEnum.GroundMud, Type.GetType("Nexus.Objects.GroundMud") },
			{ (byte) TileEnum.GroundStone, Type.GetType("Nexus.Objects.GroundStone") },
			{ (byte) TileEnum.GroundSnow, Type.GetType("Nexus.Objects.GroundSnow") },
			{ (byte) TileEnum.GroundSlime, Type.GetType("Nexus.Objects.GroundSlime") },
			{ (byte) TileEnum.GroundCloud, Type.GetType("Nexus.Objects.GroundCloud") },
			
			// Ground-Esque, Immutable (10 - 19)
			{ (byte) TileEnum.Wall, Type.GetType("Nexus.Objects.Wall") },
			{ (byte) TileEnum.Log, Type.GetType("Nexus.Objects.Log") },
			
			// Ledges (20 - 29)
			{ (byte) TileEnum.LedgeGrass, Type.GetType("Nexus.Objects.LedgeGrass") },
			{ (byte) TileEnum.PlatformFixed, Type.GetType("Nexus.Objects.PlatformFixed") },
			{ (byte) TileEnum.PlatformItem, Type.GetType("Nexus.Objects.PlatformItem") },
			
			// Decor, Prompts (30 - 39)
			{ (byte) TileEnum.DecorVeg, Type.GetType("Nexus.Objects.DecorVeg") },
			{ (byte) TileEnum.DecorDesert, Type.GetType("Nexus.Objects.DecorDesert") },
			{ (byte) TileEnum.DecorCave, Type.GetType("Nexus.Objects.DecorCave") },
			{ (byte) TileEnum.DecorWater, Type.GetType("Nexus.Objects.DecorWater") },
			{ (byte) TileEnum.DecorPet, Type.GetType("Nexus.Objects.DecorPet") },
			{ (byte) TileEnum.DecorItems, Type.GetType("Nexus.Objects.DecorItems") },
			{ (byte) TileEnum.PromptArrow, Type.GetType("Nexus.Objects.PromptArrow") },
			{ (byte) TileEnum.PromptIcon, Type.GetType("Nexus.Objects.PromptSign") },

			// Background Interactives (These Collide)
			{ (byte) TileEnum.BGDisable, Type.GetType("Nexus.Objects.BGDisable") },
			{ (byte) TileEnum.BGTap, Type.GetType("Nexus.Objects.BGTap") },
			{ (byte) TileEnum.BGWind, Type.GetType("Nexus.Objects.BGWind") },

			// Fixed, Touch-Effect (40 - 49)
			{ (byte) TileEnum.Brick, Type.GetType("Nexus.Objects.Brick") },
			{ (byte) TileEnum.Box, Type.GetType("Nexus.Objects.Box") },
			{ (byte) TileEnum.Lock, Type.GetType("Nexus.Objects.Lock") },
			{ (byte) TileEnum.Leaf, Type.GetType("Nexus.Objects.Leaf") },
			{ (byte) TileEnum.ExclaimBlock, Type.GetType("Nexus.Objects.ExclaimBlock") },
			// ...
			{ (byte) TileEnum.Spike, Type.GetType("Nexus.Objects.Spike") },
			{ (byte) TileEnum.PuffBlock, Type.GetType("Nexus.Objects.PuffBlock") },
			{ (byte) TileEnum.Conveyor, Type.GetType("Nexus.Objects.Conveyor") },

			// Solid, Toggled (50 - 55)
			{ (byte) TileEnum.ToggleBoxBR, Type.GetType("Nexus.Objects.ToggleBoxBR") },
			{ (byte) TileEnum.ToggleBoxGY, Type.GetType("Nexus.Objects.ToggleBoxGY") },
			{ (byte) TileEnum.ToggleBlockBlue, Type.GetType("Nexus.Objects.ToggleBlockBlue") },
			{ (byte) TileEnum.ToggleBlockRed, Type.GetType("Nexus.Objects.ToggleBlockRed") },
			{ (byte) TileEnum.ToggleBlockGreen, Type.GetType("Nexus.Objects.ToggleBlockGreen") },
			{ (byte) TileEnum.ToggleBlockYellow, Type.GetType("Nexus.Objects.ToggleBlockYellow") },
			
			// Solid, Toggled Platforms (56 - 59)
			{ (byte) TileEnum.TogglePlatBlue, Type.GetType("Nexus.Objects.TogglePlatBlue") },
			{ (byte) TileEnum.TogglePlatRed, Type.GetType("Nexus.Objects.TogglePlatRed") },
			{ (byte) TileEnum.TogglePlatGreen, Type.GetType("Nexus.Objects.TogglePlatGreen") },
			{ (byte) TileEnum.TogglePlatYellow, Type.GetType("Nexus.Objects.TogglePlatYellow") },

			// Generators (60 - 64)
			{ (byte) TileEnum.Cannon, Type.GetType("Nexus.Objects.Cannon") },
			{ (byte) TileEnum.Placer, Type.GetType("Nexus.Objects.Placer") },

			// Reserved (65 - 69)
			
			// Anything below this section has an ObjectID, possibly Update(), and Passive Collision.
			
			// Tile-Based Creatures (70 - 79)
			{ (byte) TileEnum.Plant, Type.GetType("Nexus.Objects.Plant") },
			{ (byte) TileEnum.ChomperGrass, Type.GetType("Nexus.Objects.ChomperGrass") },
			{ (byte) TileEnum.ChomperMetal, Type.GetType("Nexus.Objects.ChomperMetal") },
			{ (byte) TileEnum.ChomperFire, Type.GetType("Nexus.Objects.ChomperFire") },

			// Anything below can only be interacted with by a character:
			// These will have Passive Collision by Character Only.

			// Character Interactives (150 - 159)
			{ (byte) TileEnum.Flag, Type.GetType("Nexus.Objects.Flag") },
			{ (byte) TileEnum.Chest, Type.GetType("Nexus.Objects.Chest") },
			{ (byte) TileEnum.NPC, Type.GetType("Nexus.Objects.NPC") },
			{ (byte) TileEnum.PeekMap, Type.GetType("Nexus.Objects.PeekMap") },

			{ (byte) TileEnum.Door, Type.GetType("Nexus.Objects.Door") },
			{ (byte) TileEnum.DoorLock, Type.GetType("Nexus.Objects.DoorLock") },
			
			// Collectables (160 - 169)
			{ (byte) TileEnum.Coins, Type.GetType("Nexus.Objects.Coins") },
			{ (byte) TileEnum.Goodie, Type.GetType("Nexus.Objects.Goodie") },
			{ (byte) TileEnum.CollectableSuit, Type.GetType("Nexus.Objects.CollectableSuit") },
			{ (byte) TileEnum.CollectableHat, Type.GetType("Nexus.Objects.CollectableHat") },
			{ (byte) TileEnum.CollectablePower, Type.GetType("Nexus.Objects.CollectablePower") },
		};
		
		// List of Game Object Types
		public Dictionary<byte, Type> ObjectMap = new Dictionary<byte, Type>() {

			// Platforms (1 - 4)
			{ (byte) ObjectEnum.PlatformDip, Type.GetType("Nexus.Objects.PlatformDip") },
			{ (byte) ObjectEnum.PlatformDelay, Type.GetType("Nexus.Objects.PlatformDelay") },
			{ (byte) ObjectEnum.PlatformFall, Type.GetType("Nexus.Objects.PlatformFall") },
			{ (byte) ObjectEnum.PlatformMove, Type.GetType("Nexus.Objects.PlatformMove") },
			
			// Tracks (5 - 9)
			{ (byte) ObjectEnum.Cluster, Type.GetType("Nexus.Objects.Cluster") },
			{ (byte) ObjectEnum.Track, Type.GetType("Nexus.Objects.Track") },
			
			// Land & Fixed Enemies (10 - 39)
			{ (byte) ObjectEnum.Moosh, Type.GetType("Nexus.Objects.Moosh") },
			{ (byte) ObjectEnum.Shroom, Type.GetType("Nexus.Objects.Shroom") },
			{ (byte) ObjectEnum.Bug, Type.GetType("Nexus.Objects.Bug") },
			{ (byte) ObjectEnum.Goo, Type.GetType("Nexus.Objects.Goo") },
			{ (byte) ObjectEnum.Liz, Type.GetType("Nexus.Objects.Liz") },
			{ (byte) ObjectEnum.Snek, Type.GetType("Nexus.Objects.Snek") },
			{ (byte) ObjectEnum.Wurm, Type.GetType("Nexus.Objects.Wurm") },
			{ (byte) ObjectEnum.Octo, Type.GetType("Nexus.Objects.Octo") },
			{ (byte) ObjectEnum.Bones, Type.GetType("Nexus.Objects.Bones") },

			{ (byte) ObjectEnum.Turtle, Type.GetType("Nexus.Objects.Turtle") },
			{ (byte) ObjectEnum.Snail, Type.GetType("Nexus.Objects.Snail") },
			{ (byte) ObjectEnum.Boom, Type.GetType("Nexus.Objects.Boom") },

			{ (byte) ObjectEnum.Poke, Type.GetType("Nexus.Objects.Poke") },
			{ (byte) ObjectEnum.Lich, Type.GetType("Nexus.Objects.Lich") },

			{ (byte) ObjectEnum.Chomper, Type.GetType("Nexus.Objects.Chomper") },
			{ (byte) ObjectEnum.ChomperFire, Type.GetType("Nexus.Objects.ChomperFire") },
			{ (byte) ObjectEnum.Plant, Type.GetType("Nexus.Objects.Plant") },

			// Flight Enemies (40 - 69)
			{ (byte) ObjectEnum.Ghost, Type.GetType("Nexus.Objects.Ghost") },
			{ (byte) ObjectEnum.FlairElectric, Type.GetType("Nexus.Objects.FlairElectric") },
			{ (byte) ObjectEnum.FlairFire, Type.GetType("Nexus.Objects.FlairFire") },
			{ (byte) ObjectEnum.FlairMagic, Type.GetType("Nexus.Objects.FlairMagic") },

			{ (byte) ObjectEnum.ElementalAir, Type.GetType("Nexus.Objects.ElementalAir") },
			{ (byte) ObjectEnum.ElementalEarth, Type.GetType("Nexus.Objects.ElementalEarth") },
			{ (byte) ObjectEnum.ElementalFire, Type.GetType("Nexus.Objects.ElementalFire") },

			{ (byte) ObjectEnum.Buzz, Type.GetType("Nexus.Objects.Buzz") },

			{ (byte) ObjectEnum.Saw, Type.GetType("Nexus.Objects.Saw") },
			{ (byte) ObjectEnum.Slammer, Type.GetType("Nexus.Objects.Slammer") },
			{ (byte) ObjectEnum.ElementalEye, Type.GetType("Nexus.Objects.ElementalEye") },
			{ (byte) ObjectEnum.WallBouncer, Type.GetType("Nexus.Objects.WallBouncer") },

			{ (byte) ObjectEnum.Dire, Type.GetType("Nexus.Objects.Dire") },

			// Items, Fixed (70 - 79)
			{ (byte) ObjectEnum.SpringFixed, Type.GetType("Nexus.Objects.SpringFixed") },
			{ (byte) ObjectEnum.ButtonFixed, Type.GetType("Nexus.Objects.ButtonFixed") },

			// Items, Mobile (80 - 99)
			{ (byte) ObjectEnum.Shell, Type.GetType("Nexus.Objects.Shell") },
			{ (byte) ObjectEnum.Boulder, Type.GetType("Nexus.Objects.Boulder") },
			{ (byte) ObjectEnum.Bomb, Type.GetType("Nexus.Objects.Bomb") },

			{ (byte) ObjectEnum.TNT, Type.GetType("Nexus.Objects.TNT") },

			{ (byte) ObjectEnum.SpringStandard, Type.GetType("Nexus.Objects.SpringStandard") },
			{ (byte) ObjectEnum.ButtonStandard, Type.GetType("Nexus.Objects.ButtonStandard") },
			{ (byte) ObjectEnum.ButtonTimed, Type.GetType("Nexus.Objects.ButtonTimed") },

			{ (byte) ObjectEnum.MobileBlockBlue, Type.GetType("Nexus.Objects.MobileBlockBlue") },
			{ (byte) ObjectEnum.MobileBlockRed, Type.GetType("Nexus.Objects.MobileBlockRed") },
			{ (byte) ObjectEnum.MobileBlockGreen, Type.GetType("Nexus.Objects.MobileBlockGreen") },
			{ (byte) ObjectEnum.MobileBlockYellow, Type.GetType("Nexus.Objects.MobileBlockYellow") },

			// Special Flags and Placements (100+)
			{ (byte) ObjectEnum.Character, Type.GetType("Nexus.Objects.Character") },
		};
	}
}
