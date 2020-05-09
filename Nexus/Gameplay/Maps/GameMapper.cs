using Microsoft.Xna.Framework.Graphics;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.ObjectComponents;
using Nexus.Objects;
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
		public Dictionary<byte, TileGameObject> TileDict;

		public GameMapper(GameClient game, SpriteBatch spriteBatch) {

			// Create Atlas List
			this.atlas = new Atlas[3];
			this.atlas[(byte)AtlasGroup.Tiles] = new Atlas(game, spriteBatch, "Atlas/Tiles.png");
			this.atlas[(byte)AtlasGroup.Objects] = new Atlas(game, spriteBatch, "Atlas/Objects.png");
			this.atlas[(byte)AtlasGroup.World] = new Atlas(game, spriteBatch, "Atlas/World.png");

			// List of Game Object Metadata
			MetaList[MetaGroup.Character] = new IMetaData(Arch.Character, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Interactives, LayerEnum.main, LoadOrder.Character);
			MetaList[MetaGroup.Ground] = new IMetaData(Arch.Ground, this.atlas[(byte) AtlasGroup.Tiles], SlotGroup.Blocks, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Block
				// All Ground, Leaf, Lock, GrowObj
			MetaList[MetaGroup.Ledge] = new IMetaData(Arch.Platform, this.atlas[(byte) AtlasGroup.Tiles], SlotGroup.Blocks, LayerEnum.main, LoadOrder.Platform);
			MetaList[MetaGroup.Decor] = new IMetaData(Arch.Decor, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.Decor, LayerEnum.fg, LoadOrder.Tile); // LoadOrder.Decor
			MetaList[MetaGroup.BGTile] = new IMetaData(Arch.BGTile, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.Blocks, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Block
			MetaList[MetaGroup.Block] = new IMetaData(Arch.Block, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.Blocks, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Block
				// PuffBlock, Exclaim, Box, Brick
			MetaList[MetaGroup.ToggleBlock] = new IMetaData(Arch.ToggleBlock, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.Blocks, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Block
				// ToggleBlock, ToggleOnPlat, ToggleOffPlat, ToggleOn, ToggleOff, 
			MetaList[MetaGroup.Conveyor] = new IMetaData(Arch.Block, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.Platforms, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Block
				// Conveyor
			MetaList[MetaGroup.Platform] = new IMetaData(Arch.Platform, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Platforms, LayerEnum.main, LoadOrder.Platform);
				// PlatSolid, PlatMove, PlatFall, PlatDip, PlatDelay
			MetaList[MetaGroup.EnemyFixed] = new IMetaData(Arch.Enemy, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.EnemiesLand, LayerEnum.main, LoadOrder.Enemy);
				// Chomper, Fire Chomper, Plant
			MetaList[MetaGroup.EnemyLand] = new IMetaData(Arch.Enemy, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.EnemiesLand, LayerEnum.main, LoadOrder.Enemy);
			MetaList[MetaGroup.EnemyFly] = new IMetaData(Arch.Enemy, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.EnemiesFly, LayerEnum.main, LoadOrder.Enemy);
			MetaList[MetaGroup.BlockMoving] = new IMetaData(Arch.MovingBlock, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.EnemiesFly, LayerEnum.main, LoadOrder.Enemy);
				// Slammer
			MetaList[MetaGroup.Item] = new IMetaData(Arch.Item, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Gadgets, LayerEnum.main, LoadOrder.Item);
				// Most Items (but not buttons)
			MetaList[MetaGroup.Button] = new IMetaData(Arch.Item, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Gadgets, LayerEnum.main, LoadOrder.Item);
				// Buttons
			MetaList[MetaGroup.Generator] = new IMetaData(Arch.Generator, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Gadgets, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Block
				// Cannon, Placer
			MetaList[MetaGroup.Collectable] = new IMetaData(Arch.Collectable, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.Collectables, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Collectable
				// Collectables
			MetaList[MetaGroup.Track] = new IMetaData(Arch.HiddenObject, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Scripting, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Invisible
				// Track, Clusuter
			MetaList[MetaGroup.Door] = new IMetaData(Arch.Portal, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Interactives, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Portal
			MetaList[MetaGroup.Interactives] = new IMetaData(Arch.Interactives, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Interactives, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Interactives
				// Chest, PeekMap
			MetaList[MetaGroup.Flag] = new IMetaData(Arch.Collectable, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Interactives, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Interactives
			MetaList[MetaGroup.NPC] = new IMetaData(Arch.Interactives, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Interactives, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Interactives
			MetaList[MetaGroup.Projectile] = new IMetaData(Arch.Projectile, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.None, LayerEnum.main, LoadOrder.Projectile); // LoadOrder.Interactives
		}

		public void PostLoad() {

			// Map of Tile Classes
			this.TileDict = new Dictionary<byte, TileGameObject>() {
				
				// Ground, Immutable (0 - 9)
				{ (byte) TileEnum.GroundGrass, new GroundGrass() },
				{ (byte) TileEnum.GroundDirt, new GroundDirt() },
				{ (byte) TileEnum.GroundMud, new GroundMud() },
				{ (byte) TileEnum.GroundStone, new GroundStone() },
				{ (byte) TileEnum.GroundSnow, new GroundSnow() },
				{ (byte) TileEnum.GroundSlime, new GroundSlime() },
				{ (byte) TileEnum.GroundCloud, new GroundCloud() },
				
				// Ground-Esque, Immutable (10 - 19)
				{ (byte) TileEnum.Wall, new Wall() },
				{ (byte) TileEnum.Log, new Log() },

				// Ledges (20 - 29)
				{ (byte) TileEnum.LedgeGrass, new LedgeGrass() },
				{ (byte) TileEnum.LedgeGrassDecor, new LedgeGrassDecor() },
				{ (byte) TileEnum.PlatformFixed, new PlatformFixed() },
				//{ (byte) TileEnum.PlatformItem, new PlatformItem() },
				
				// Decor, Prompts (30 - 39)
				{ (byte) TileEnum.DecorVeg, new DecorVeg() },
				//{ (byte) TileEnum.DecorDesert, new DecorDesert() },
				{ (byte) TileEnum.DecorCave, new DecorCave() },
				//{ (byte) TileEnum.DecorWater, new DecorWater() },
				{ (byte) TileEnum.DecorPet, new DecorPet() },
				{ (byte) TileEnum.DecorItems, new DecorItems() },
				{ (byte) TileEnum.PromptArrow, new PromptArrow() },
				{ (byte) TileEnum.PromptIcon, new PromptIcon() },

				// Background Interactives (These Collide)
				{ (byte) TileEnum.BGDisable, new BGDisable() },
				{ (byte) TileEnum.BGTap, new BGTap() },
				//{ (byte) TileEnum.BGWind, new BGWind() },

				// Fixed, Touch-Effect (40 - 49)
				{ (byte) TileEnum.Brick, new Brick() },
				{ (byte) TileEnum.Box, new Box() },
				{ (byte) TileEnum.Lock, new Lock() },
				{ (byte) TileEnum.Leaf, new Leaf() },
				{ (byte) TileEnum.ExclaimBlock, new ExclaimBlock() },
				// ...
				{ (byte) TileEnum.Spike, new Spike() },
				{ (byte) TileEnum.PuffBlock, new PuffBlock() },
				{ (byte) TileEnum.Conveyor, new Conveyor() },

				// Solid, Toggled (50 - 55)
				{ (byte) TileEnum.ToggleBoxBR, new ToggleBoxBR() },
				{ (byte) TileEnum.ToggleBoxGY, new ToggleBoxGY() },
				{ (byte) TileEnum.ToggleBlockBlue, new ToggleBlockBlue() },
				{ (byte) TileEnum.ToggleBlockRed, new ToggleBlockRed() },
				{ (byte) TileEnum.ToggleBlockGreen, new ToggleBlockGreen() },
				{ (byte) TileEnum.ToggleBlockYellow, new ToggleBlockYellow() },
				
				// Solid, Toggled Platforms (56 - 59)
				{ (byte) TileEnum.TogglePlatBlue, new TogglePlatBlue() },
				{ (byte) TileEnum.TogglePlatRed, new TogglePlatRed() },
				{ (byte) TileEnum.TogglePlatGreen, new TogglePlatGreen() },
				{ (byte) TileEnum.TogglePlatYellow, new TogglePlatYellow() },

				// Generators (60 - 64)
				{ (byte) TileEnum.CannonHorizontal, new CannonHor() },
				{ (byte) TileEnum.CannonVertical, new CannonVert() },
				{ (byte) TileEnum.CannonDiagonal, new CannonDiag() },
				{ (byte) TileEnum.Placer, new Placer() },
				
				// Hidden Objects (65 - 69)
				{ (byte) TileEnum.TrackDot, new TrackDot() },
				
				// Anything below this section has an ObjectID, possibly Update(), and Passive Collision.
				
				// Tile-Based Creatures (70 - 79)
				{ (byte) TileEnum.Plant, new Plant() },
				{ (byte) TileEnum.ChomperGrass, new ChomperGrass() },
				{ (byte) TileEnum.ChomperMetal, new ChomperMetal() },
				{ (byte) TileEnum.ChomperFire, new ChomperFire() },

				// Anything below can only be interacted with by a character:
				// These will have Passive Collision by Character Only.

				// Character Interactives (150 - 159)
				{ (byte) TileEnum.Chest, new Chest() },
				{ (byte) TileEnum.NPC, new NPC() },
				//{ (byte) TileEnum.PeekMap, new PeekMap() },

				{ (byte) TileEnum.Door, new Door() },
				{ (byte) TileEnum.DoorLock, new DoorLock() },
				
				// Collectables (160 - 169)
				{ (byte) TileEnum.Coins, new Coins() },
				{ (byte) TileEnum.Goodie, new Goodie() },
				{ (byte) TileEnum.CollectableSuit, new CollectableSuit() },
				{ (byte) TileEnum.CollectableHat, new CollectableHat() },
				{ (byte) TileEnum.CollectablePower, new CollectablePower() },

				// Flags (170 - 174)
				{ (byte) TileEnum.CheckFlagFinish, new CheckFlagFinish() },
				{ (byte) TileEnum.CheckFlagCheckpoint, new CheckFlagCheckpoint() },
				{ (byte) TileEnum.CheckFlagPass, new CheckFlagPass() },
				{ (byte) TileEnum.CheckFlagRetry, new CheckFlagRetry() },
			};
		}

		// List of Game Object Types
		public Dictionary<byte, Type> ObjectTypeDict = new Dictionary<byte, Type>() {

			// Platforms (1 - 4)
			{ (byte) ObjectEnum.PlatformDip, Type.GetType("Nexus.Objects.PlatformDip") },
			{ (byte) ObjectEnum.PlatformDelay, Type.GetType("Nexus.Objects.PlatformDelay") },
			{ (byte) ObjectEnum.PlatformFall, Type.GetType("Nexus.Objects.PlatformFall") },
			{ (byte) ObjectEnum.PlatformMove, Type.GetType("Nexus.Objects.PlatformMove") },
			
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

			// Special Objects
			{ (byte) ObjectEnum.Cluster, Type.GetType("Nexus.Objects.Cluster") },

			// Special Flags and Placements (150+)
			{ (byte) ObjectEnum.Character, Type.GetType("Nexus.Objects.Character") },
		};
	}
}
