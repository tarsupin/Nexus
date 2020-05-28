﻿using Microsoft.Xna.Framework.Graphics;
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

	// GameObjectMetaData exists because Game Objects need a reference to their metadata outside of the individual classes (and preferably without reflection).
	// Example: The Wand Tool needs to know what Parameter Set to use for each object.
	public class GameObjectMetaData {
		public readonly Params paramSet;
		public readonly IMetaData meta;

		public GameObjectMetaData(IMetaData meta, Params paramSet) {
			this.meta = meta;
			this.paramSet = paramSet;
		}
	}

	public class GameMapper {

		public readonly Atlas[] atlas;
		public Dictionary<MetaGroup, IMetaData> MetaList = new Dictionary<MetaGroup, IMetaData>();
		public Dictionary<byte, TileObject> TileDict;
		public Dictionary<byte, GameObjectMetaData> ObjectMetaData = new Dictionary<byte, GameObjectMetaData>();

		// World Tiles
		public Dictionary<byte, string> WorldTerrain;
		public Dictionary<byte, string> WorldTerrainCat;
		public Dictionary<byte, string> WorldLayers;
		public Dictionary<byte, string> WorldObjects;

		public GameMapper(GameClient game, SpriteBatch spriteBatch) {

			// Create Atlas List
			this.atlas = new Atlas[3];
			this.atlas[(byte) AtlasGroup.Tiles] = new Atlas(game, spriteBatch, "Atlas/Tiles.png");
			this.atlas[(byte) AtlasGroup.Objects] = new Atlas(game, spriteBatch, "Atlas/Objects.png");
			this.atlas[(byte) AtlasGroup.World] = new Atlas(game, spriteBatch, "Atlas/World.png");

			// Need to assign Object Atlas to Shadow Tile so that it can replicate the Object imagery.
			ShadowTile.atlas = this.atlas[(byte) AtlasGroup.Objects];

			// List of Game Object Metadata
			MetaList[MetaGroup.Character] = new IMetaData(Arch.Character, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Interactives, LayerEnum.main, LoadOrder.Character);
			MetaList[MetaGroup.Ground] = new IMetaData(Arch.Ground, this.atlas[(byte) AtlasGroup.Tiles], SlotGroup.Ground, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Block
				// All Ground, Leaf, Lock, GrowObj
			MetaList[MetaGroup.Ledge] = new IMetaData(Arch.Platform, this.atlas[(byte) AtlasGroup.Tiles], SlotGroup.Blocks, LayerEnum.main, LoadOrder.Platform);
			MetaList[MetaGroup.Decor] = new IMetaData(Arch.Decor, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.Decor, LayerEnum.fg, LoadOrder.Tile); // LoadOrder.Decor
			MetaList[MetaGroup.BGTile] = new IMetaData(Arch.BGTile, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.Blocks, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Block
			MetaList[MetaGroup.Block] = new IMetaData(Arch.Block, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.Blocks, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Block
				// PuffBlock, Exclaim, Box, Brick
			MetaList[MetaGroup.ToggleBlock] = new IMetaData(Arch.ToggleBlock, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.ColorToggles, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Block
				// ToggleBlock, ToggleOnPlat, ToggleOffPlat, ToggleOn, ToggleOff, 
			MetaList[MetaGroup.Conveyor] = new IMetaData(Arch.Block, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.Platforms, LayerEnum.main, LoadOrder.Tile); // LoadOrder.Block
				// Conveyor
			MetaList[MetaGroup.Platform] = new IMetaData(Arch.Platform, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Platforms, LayerEnum.main, LoadOrder.Platform);
				// PlatSolid, PlatMove, PlatFall, PlatDip, PlatDelay
			MetaList[MetaGroup.EnemyFixed] = new IMetaData(Arch.Enemy, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.EnemiesLand, LayerEnum.main, LoadOrder.Enemy);
				// Chomper, Fire Chomper, Plant
			MetaList[MetaGroup.EnemyLand] = new IMetaData(Arch.Enemy, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.EnemiesLand, LayerEnum.obj, LoadOrder.Enemy);
			MetaList[MetaGroup.EnemyFly] = new IMetaData(Arch.Enemy, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.EnemiesFly, LayerEnum.obj, LoadOrder.Enemy);
			MetaList[MetaGroup.BlockMoving] = new IMetaData(Arch.MovingBlock, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.EnemiesFly, LayerEnum.main, LoadOrder.Enemy);
				// Slammer
			MetaList[MetaGroup.Item] = new IMetaData(Arch.Item, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Gadgets, LayerEnum.obj, LoadOrder.Item);
				// Most Items (but not buttons)
			MetaList[MetaGroup.Button] = new IMetaData(Arch.Item, this.atlas[(byte)AtlasGroup.Objects], SlotGroup.Gadgets, LayerEnum.obj, LoadOrder.Item);
			MetaList[MetaGroup.ButtonFixed] = new IMetaData(Arch.Item, this.atlas[(byte)AtlasGroup.Tiles], SlotGroup.ColorToggles, LayerEnum.main, LoadOrder.Item);
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


			// List of Game Objects

			//// Platforms (1 - 4)
			this.ObjectMetaData[(byte)ObjectEnum.PlatformDip] = new GameObjectMetaData(MetaList[MetaGroup.Platform], null);
			this.ObjectMetaData[(byte)ObjectEnum.PlatformDelay] = new GameObjectMetaData(MetaList[MetaGroup.Platform], null);
			this.ObjectMetaData[(byte)ObjectEnum.PlatformFall] = new GameObjectMetaData(MetaList[MetaGroup.Platform], null);
			this.ObjectMetaData[(byte)ObjectEnum.PlatformMove] = new GameObjectMetaData(MetaList[MetaGroup.Platform], Params.ParamMap["Flight"]);

			//// Land & Fixed Enemies (10 - 39)
			this.ObjectMetaData[(byte)ObjectEnum.Moosh] = new GameObjectMetaData(MetaList[MetaGroup.EnemyLand], null);
			this.ObjectMetaData[(byte)ObjectEnum.Shroom] = new GameObjectMetaData(MetaList[MetaGroup.EnemyLand], null);
			this.ObjectMetaData[(byte)ObjectEnum.Bug] = new GameObjectMetaData(MetaList[MetaGroup.EnemyLand], null);
			this.ObjectMetaData[(byte)ObjectEnum.Goo] = new GameObjectMetaData(MetaList[MetaGroup.EnemyLand], null);
			this.ObjectMetaData[(byte)ObjectEnum.Liz] = new GameObjectMetaData(MetaList[MetaGroup.EnemyLand], null);
			this.ObjectMetaData[(byte)ObjectEnum.Snek] = new GameObjectMetaData(MetaList[MetaGroup.EnemyLand], null);
			this.ObjectMetaData[(byte)ObjectEnum.Wurm] = new GameObjectMetaData(MetaList[MetaGroup.EnemyLand], null);
			this.ObjectMetaData[(byte)ObjectEnum.Octo] = new GameObjectMetaData(MetaList[MetaGroup.EnemyLand], null);
			this.ObjectMetaData[(byte)ObjectEnum.Bones] = new GameObjectMetaData(MetaList[MetaGroup.EnemyLand], null);

			this.ObjectMetaData[(byte)ObjectEnum.Turtle] = new GameObjectMetaData(MetaList[MetaGroup.EnemyLand], null);
			this.ObjectMetaData[(byte)ObjectEnum.Snail] = new GameObjectMetaData(MetaList[MetaGroup.EnemyLand], null);
			this.ObjectMetaData[(byte)ObjectEnum.Boom] = new GameObjectMetaData(MetaList[MetaGroup.EnemyLand], null);

			this.ObjectMetaData[(byte)ObjectEnum.Poke] = new GameObjectMetaData(MetaList[MetaGroup.EnemyLand], null);
			this.ObjectMetaData[(byte)ObjectEnum.Lich] = new GameObjectMetaData(MetaList[MetaGroup.EnemyLand], null);

			//// Flight Enemies (40 - 69)
			this.ObjectMetaData[(byte)ObjectEnum.Ghost] = new GameObjectMetaData(MetaList[MetaGroup.EnemyFly], Params.ParamMap["Flight"]);
			this.ObjectMetaData[(byte)ObjectEnum.FlairElectric] = new GameObjectMetaData(MetaList[MetaGroup.EnemyFly], Params.ParamMap["Flight"]);
			this.ObjectMetaData[(byte)ObjectEnum.FlairFire] = new GameObjectMetaData(MetaList[MetaGroup.EnemyFly], Params.ParamMap["Flight"]);
			this.ObjectMetaData[(byte)ObjectEnum.FlairMagic] = new GameObjectMetaData(MetaList[MetaGroup.EnemyFly], Params.ParamMap["Flight"]);

			this.ObjectMetaData[(byte)ObjectEnum.ElementalAir] = new GameObjectMetaData(MetaList[MetaGroup.EnemyFly], Params.ParamMap["Flight"]);
			this.ObjectMetaData[(byte)ObjectEnum.ElementalEarth] = new GameObjectMetaData(MetaList[MetaGroup.EnemyFly], Params.ParamMap["Flight"]);
			this.ObjectMetaData[(byte)ObjectEnum.ElementalFire] = new GameObjectMetaData(MetaList[MetaGroup.EnemyFly], Params.ParamMap["Flight"]);

			this.ObjectMetaData[(byte)ObjectEnum.Buzz] = new GameObjectMetaData(MetaList[MetaGroup.EnemyFly], Params.ParamMap["Flight"]);

			this.ObjectMetaData[(byte)ObjectEnum.Saw] = new GameObjectMetaData(MetaList[MetaGroup.EnemyFly], Params.ParamMap["Flight"]);
			this.ObjectMetaData[(byte)ObjectEnum.Slammer] = new GameObjectMetaData(MetaList[MetaGroup.EnemyFly], Params.ParamMap["Flight"]);
			this.ObjectMetaData[(byte)ObjectEnum.HoveringEye] = new GameObjectMetaData(MetaList[MetaGroup.EnemyFly], Params.ParamMap["Flight"]);
			this.ObjectMetaData[(byte)ObjectEnum.Bouncer] = new GameObjectMetaData(MetaList[MetaGroup.EnemyFly], Params.ParamMap["Flight"]);

			this.ObjectMetaData[(byte)ObjectEnum.Dire] = new GameObjectMetaData(MetaList[MetaGroup.EnemyFly], Params.ParamMap["Flight"]);

			//// Items, Mobile (80 - 99)
			this.ObjectMetaData[(byte)ObjectEnum.Shell] = new GameObjectMetaData(MetaList[MetaGroup.Item], Params.ParamMap["Shell"]);
			this.ObjectMetaData[(byte)ObjectEnum.Boulder] = new GameObjectMetaData(MetaList[MetaGroup.Item], null);
			this.ObjectMetaData[(byte)ObjectEnum.Bomb] = new GameObjectMetaData(MetaList[MetaGroup.Item], null);

			this.ObjectMetaData[(byte)ObjectEnum.TNT] = new GameObjectMetaData(MetaList[MetaGroup.Item], null);

			this.ObjectMetaData[(byte)ObjectEnum.SpringStandard] = new GameObjectMetaData(MetaList[MetaGroup.Item], null);
			this.ObjectMetaData[(byte)ObjectEnum.ButtonStandard] = new GameObjectMetaData(MetaList[MetaGroup.Button], null);

			//// Special Objects
			this.ObjectMetaData[(byte)ObjectEnum.Cluster] = new GameObjectMetaData(MetaList[MetaGroup.EnemyFly], Params.ParamMap["Flight"]);

			//// Special Flags and Placements (150+)
			this.ObjectMetaData[(byte)ObjectEnum.Character] = new GameObjectMetaData(MetaList[MetaGroup.Character], null);
		}

		public void PostLoad() {

			// Map of Tile Classes
			this.TileDict = new Dictionary<byte, TileObject>() {
				
				// Ground, Immutable (0 - 9)
				{ (byte) TileEnum.GroundGrass, new GroundGrass() },
				{ (byte) TileEnum.GroundDirt, new GroundDirt() },
				{ (byte) TileEnum.GroundMud, new GroundMud() },
				{ (byte) TileEnum.GroundStone, new GroundStone() },
				{ (byte) TileEnum.GroundSnow, new GroundSnow() },
				{ (byte) TileEnum.GroundSlime, new GroundSlime() },
				{ (byte) TileEnum.GroundCloud, new GroundCloud() },
				
				// Ground-Esque, Immutable (10 - 19)
				{ (byte) TileEnum.SlabGray, new SlabGray() },
				{ (byte) TileEnum.Log, new Log() },
				{ (byte) TileEnum.SlabBrown, new SlabBrown() },

				// Ledges (20 - 29)
				{ (byte) TileEnum.LedgeGrass, new LedgeGrass() },
				{ (byte) TileEnum.LedgeDecor, new LedgeDecor() },
				{ (byte) TileEnum.LedgeSnow, new LedgeSnow() },
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
				
				// Tile-Based Creatures (70 - 79)
				{ (byte) TileEnum.Plant, new Plant() },
				{ (byte) TileEnum.ChomperGrass, new ChomperGrass() },
				{ (byte) TileEnum.ChomperMetal, new ChomperMetal() },
				{ (byte) TileEnum.ChomperFire, new ChomperFire() },

				// Fixed Items (80 - 90)
				{ (byte) TileEnum.ButtonFixed, new ButtonFixed() },
				{ (byte) TileEnum.ButtonTimed, new ButtonTimed() },
				{ (byte) TileEnum.SpringFixed, new SpringFixed() },
				{ (byte) TileEnum.SpringSide, new SpringSide() },

				// Anything below can only be interacted with by a character:
				// These will have Passive Collision by Character Only.
				
				// Background Interactives (These Collide) (140 - 149)
				{ (byte) TileEnum.BGDisable, new BGDisable() },
				{ (byte) TileEnum.BGTap, new BGTap() },
				//{ (byte) TileEnum.BGWind, new BGWind() },

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

			// World Terrain
			this.WorldTerrain = new Dictionary<byte, string>() {
				{ (byte) OTerrain.Grass, "Grass" },
				{ (byte) OTerrain.Desert, "Desert" },
				{ (byte) OTerrain.Snow, "Snow" },
				{ (byte) OTerrain.WaterDeep, "WaterDeep" },
				{ (byte) OTerrain.Water, "Water" },
				{ (byte) OTerrain.Mud, "Mud" },
				{ (byte) OTerrain.Dirt, "Dirt" },
				{ (byte) OTerrain.Cobble, "Cobble" },
				{ (byte) OTerrain.Road, "Road" },
				{ (byte) OTerrain.Ice, "Ice" },
				{ (byte) OTerrain.DirtDark, "DirtDark" },
			};

			// World Terrain Categories
			this.WorldTerrainCat = new Dictionary<byte, string>() {
				{ (byte) OTerrainCat.Trees, "Trees" },
				{ (byte) OTerrainCat.Mountains, "Mountain" },
				{ (byte) OTerrainCat.Field, "Field" },
				{ (byte) OTerrainCat.Veg, "Veg" },
				{ (byte) OTerrainCat.Water, "Water" },
				{ (byte) OTerrainCat.Field2, "Field2" },
			};

			// World Layers
			this.WorldLayers = new Dictionary<byte, string>() {

				// Default Value
				{ (byte) 0, "b1" },

				// Base Variations
				{ (byte) OLayer.b1, "b1" },
				{ (byte) OLayer.b2, "b2" },
				{ (byte) OLayer.b3, "b3" },
				{ (byte) OLayer.b4, "b4" },
				{ (byte) OLayer.b5, "b5" },
				{ (byte) OLayer.b6, "b6" },
				{ (byte) OLayer.b7, "b7" },
				{ (byte) OLayer.b8, "b8" },
				{ (byte) OLayer.b9, "b9" },
				{ (byte) OLayer.b10, "b10" },

				// Paths
				{ (byte) OLayer.c2, "c2" },
				{ (byte) OLayer.c4, "c4" },
				{ (byte) OLayer.c5, "c5" },
				{ (byte) OLayer.c6, "c6" },
				{ (byte) OLayer.c8, "c8" },

				{ (byte) OLayer.e1, "e1" },
				{ (byte) OLayer.e2, "e2" },
				{ (byte) OLayer.e3, "e3" },
				{ (byte) OLayer.e4, "e4" },
				{ (byte) OLayer.e5, "e5" },
				{ (byte) OLayer.e6, "e6" },
				{ (byte) OLayer.e7, "e7" },
				{ (byte) OLayer.e8, "e8" },
				{ (byte) OLayer.e9, "e9" },
				{ (byte) OLayer.el, "el" },
				{ (byte) OLayer.er, "er" },

				{ (byte) OLayer.l1, "l1" },
				{ (byte) OLayer.l3, "l3" },
				{ (byte) OLayer.l7, "l7" },
				{ (byte) OLayer.l9, "l9" },

				{ (byte) OLayer.p1, "p1" },
				{ (byte) OLayer.p3, "p3" },
				{ (byte) OLayer.p7, "p7" },
				{ (byte) OLayer.p9, "p9" },
				{ (byte) OLayer.ph, "ph" },
				{ (byte) OLayer.pv, "pv" },

				{ (byte) OLayer.r1, "r1" },
				{ (byte) OLayer.r3, "r3" },
				{ (byte) OLayer.r7, "r7" },
				{ (byte) OLayer.r9, "r9" },

				{ (byte) OLayer.s, "s" },
				{ (byte) OLayer.s1, "s1" },
				{ (byte) OLayer.s2, "s2" },
				{ (byte) OLayer.s3, "s3" },
				{ (byte) OLayer.s4, "s4" },
				{ (byte) OLayer.s5, "s5" },
				{ (byte) OLayer.s6, "s6" },
				{ (byte) OLayer.s7, "s7" },
				{ (byte) OLayer.s8, "s8" },
				{ (byte) OLayer.s9, "s9" },

				{ (byte) OLayer.t2, "t2" },
				{ (byte) OLayer.t4, "t4" },
				{ (byte) OLayer.t6, "t6" },
				{ (byte) OLayer.t8, "t8" },

				{ (byte) OLayer.v1, "v1" },
				{ (byte) OLayer.v3, "v3" },
				{ (byte) OLayer.v7, "v7" },
				{ (byte) OLayer.v9, "v9" },
			};

			// World Objects
			this.WorldObjects = new Dictionary<byte, string>() {

				// Ground Objects
				{ (byte) OTerrainObjects.Bones, "Bones" },
				{ (byte) OTerrainObjects.Cactus, "Cactus" },
				{ (byte) OTerrainObjects.Stump, "Stump" },
				{ (byte) OTerrainObjects.Snowman1, "Snowman1" },
				{ (byte) OTerrainObjects.Snowman2, "Snowman2" },

				{ (byte) OTerrainObjects.Tree1, "Tree1" },
				{ (byte) OTerrainObjects.Tree2, "Tree2" },
				{ (byte) OTerrainObjects.Pit, "Pit" },
				{ (byte) OTerrainObjects.Dungeon, "Dungeon" },

				// Nodes
				{ (byte) OTerrainObjects.NodeStrict, "NodeStrict" },
				{ (byte) OTerrainObjects.NodeCasual, "NodeCasual" },
				{ (byte) OTerrainObjects.NodePoint, "NodePoint" },
				{ (byte) OTerrainObjects.NodeMove, "NodeMove" },
				{ (byte) OTerrainObjects.NodeWarp, "NodeWarp" },
				{ (byte) OTerrainObjects.NodeWon, "NodeWon" },
				{ (byte) OTerrainObjects.NodeStart, "NodeStart" },

				// Buildings, Residence
				{ (byte) OTerrainObjects.House1, "House1" },
				{ (byte) OTerrainObjects.House2, "House2" },
				{ (byte) OTerrainObjects.House3, "House3" },
				{ (byte) OTerrainObjects.House4, "House4" },
				{ (byte) OTerrainObjects.House5, "House5" },
				{ (byte) OTerrainObjects.House6, "House6" },
				{ (byte) OTerrainObjects.House7, "House7" },
				{ (byte) OTerrainObjects.House8, "House8" },
				{ (byte) OTerrainObjects.House9, "House9" },
				{ (byte) OTerrainObjects.House10, "House10" },

				// Buildings, Defense
				{ (byte) OTerrainObjects.Castle1, "Castle1" },
				{ (byte) OTerrainObjects.Castle2, "Castle2" },
				{ (byte) OTerrainObjects.Castle3, "Castle3" },
				{ (byte) OTerrainObjects.Castle4, "Castle4" },
				{ (byte) OTerrainObjects.Castle5, "Castle5" },

				{ (byte) OTerrainObjects.Tower1, "Tower1" },
				{ (byte) OTerrainObjects.Tower2, "Tower2" },
				{ (byte) OTerrainObjects.Tower3, "Tower3" },
				{ (byte) OTerrainObjects.Tower4, "Tower4" },

				// Large Buildings
				{ (byte) OTerrainObjects.Town1, "Town1" },
				{ (byte) OTerrainObjects.Town2, "Town2" },
				{ (byte) OTerrainObjects.Town3, "Town3" },

				{ (byte) OTerrainObjects.Stadium, "Stadium" },

				{ (byte) OTerrainObjects.Pyramid1, "Pyramid1" },
				{ (byte) OTerrainObjects.Pyradmi2, "Pyradmi2" },
				{ (byte) OTerrainObjects.Pyramid3, "Pyramid3" },

				// Buildings, Misc
				{ (byte) OTerrainObjects.Tent, "Tent" },

				// Stone Bridge
				{ (byte) OTerrainObjects.StoneBridge2, "sb2" },
				{ (byte) OTerrainObjects.StoneBridge4, "sb4" },
				{ (byte) OTerrainObjects.StoneBridge6, "sb6" },
				{ (byte) OTerrainObjects.StoneBridge8, "sb8" },
				{ (byte) OTerrainObjects.StoneBridgeH, "sbh" },
				{ (byte) OTerrainObjects.StoneBridgeV, "sbv" },

				// Wood Bridge
				{ (byte) OTerrainObjects.WoodBridge2, "wb2" },
				{ (byte) OTerrainObjects.WoodBridge4, "wb4" },
				{ (byte) OTerrainObjects.WoodBridge6, "wb6" },
				{ (byte) OTerrainObjects.WoodBridge8, "wb8" },
				{ (byte) OTerrainObjects.WoodBridgeH, "wbh" },
				{ (byte) OTerrainObjects.WoodBridgeV, "wbv" },
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
			{ (byte) ObjectEnum.HoveringEye, Type.GetType("Nexus.Objects.HoveringEye") },
			{ (byte) ObjectEnum.Bouncer, Type.GetType("Nexus.Objects.Bouncer") },

			{ (byte) ObjectEnum.Dire, Type.GetType("Nexus.Objects.Dire") },

			// Items, Mobile (80 - 99)
			{ (byte) ObjectEnum.Shell, Type.GetType("Nexus.Objects.Shell") },
			{ (byte) ObjectEnum.Boulder, Type.GetType("Nexus.Objects.Boulder") },
			{ (byte) ObjectEnum.Bomb, Type.GetType("Nexus.Objects.Bomb") },

			{ (byte) ObjectEnum.TNT, Type.GetType("Nexus.Objects.TNT") },

			{ (byte) ObjectEnum.SpringStandard, Type.GetType("Nexus.Objects.SpringStandard") },
			{ (byte) ObjectEnum.ButtonStandard, Type.GetType("Nexus.Objects.ButtonStandard") },

			// Special Objects
			{ (byte) ObjectEnum.Cluster, Type.GetType("Nexus.Objects.Cluster") },

			// Special Flags and Placements (150+)
			{ (byte) ObjectEnum.Character, Type.GetType("Nexus.Objects.Character") },

		};
	}
}
