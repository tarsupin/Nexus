using Microsoft.Xna.Framework.Graphics;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class GameMapper {

		public readonly Atlas[] atlas;
		public Dictionary<MetaGroup, IMetaData> MetaList = new Dictionary<MetaGroup, IMetaData>();
		
		public GameMapper(GameClient game, SpriteBatch spriteBatch) {

			// Create Atlas List
			this.atlas = new Atlas[6];
			this.atlas[(byte)AtlasGroup.Blocks] = new Atlas(game, spriteBatch, "Atlas/Blocks.png");
			this.atlas[(byte)AtlasGroup.Characters] = new Atlas(game, spriteBatch, "Atlas/Characters.png");
			this.atlas[(byte)AtlasGroup.Enemies] = new Atlas(game, spriteBatch, "Atlas/Enemies.png");
			this.atlas[(byte)AtlasGroup.Icons] = new Atlas(game, spriteBatch, "Atlas/Icons.png");
			this.atlas[(byte)AtlasGroup.Other] = new Atlas(game, spriteBatch, "Atlas/Other.png");
			this.atlas[(byte)AtlasGroup.World] = new Atlas(game, spriteBatch, "Atlas/World.png");

			// List of Game Object Metadata
			MetaList[MetaGroup.Ground] = new IMetaData(Arch.Ground, LoadOrder.Block, this.atlas[(byte) AtlasGroup.Blocks], SlotGroup.Blocks, LayerEnum.Main);
				// All Ground, Leaf, Lock, GrowObj
			MetaList[MetaGroup.Ledge] = new IMetaData(Arch.Platform, LoadOrder.Platform, this.atlas[(byte) AtlasGroup.Blocks], SlotGroup.Blocks, LayerEnum.Main);
			MetaList[MetaGroup.Decor] = new IMetaData(Arch.Decor, LoadOrder.Decor, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Prompts, LayerEnum.Cosmetic);
			MetaList[MetaGroup.Block] = new IMetaData(Arch.Block, LoadOrder.Block, this.atlas[(byte)AtlasGroup.Blocks], SlotGroup.Blocks, LayerEnum.Main);
				// PuffBlock, Exclaim, Box, Brick
			MetaList[MetaGroup.ToggleBlock] = new IMetaData(Arch.ToggleBlock, LoadOrder.Block, this.atlas[(byte)AtlasGroup.Blocks], SlotGroup.ColorToggles, LayerEnum.Main);
				// ToggleBlock, ToggleOnPlat, ToggleOffPlat, ToggleOn, ToggleOff, 
			MetaList[MetaGroup.Conveyor] = new IMetaData(Arch.Block, LoadOrder.Block, this.atlas[(byte)AtlasGroup.Blocks], SlotGroup.Movers, LayerEnum.Main);
				// Conveyor
			MetaList[MetaGroup.Platform] = new IMetaData(Arch.Platform, LoadOrder.Platform, this.atlas[(byte)AtlasGroup.Blocks], SlotGroup.Movers, LayerEnum.Main);
				// PlatSolid, PlatMove, PlatFall, PlatDip, PlatDelay
			MetaList[MetaGroup.EnemyFixed] = new IMetaData(Arch.Enemy, LoadOrder.Enemy, this.atlas[(byte)AtlasGroup.Enemies], SlotGroup.Fixed, LayerEnum.Main);
				// Chomper, Fire Chomper, Plant
			MetaList[MetaGroup.EnemyLand] = new IMetaData(Arch.Enemy, LoadOrder.Enemy, this.atlas[(byte)AtlasGroup.Enemies], SlotGroup.EnemyLand, LayerEnum.Main);
			MetaList[MetaGroup.EnemyFly] = new IMetaData(Arch.Enemy, LoadOrder.Enemy, this.atlas[(byte)AtlasGroup.Enemies], SlotGroup.EnemyFly, LayerEnum.Main);
			MetaList[MetaGroup.BlockMoving] = new IMetaData(Arch.MovingBlock, LoadOrder.Enemy, this.atlas[(byte)AtlasGroup.Enemies], SlotGroup.EnemyFly, LayerEnum.Main);
				// Slammer
			MetaList[MetaGroup.Item] = new IMetaData(Arch.Item, LoadOrder.Item, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Items, LayerEnum.Main);
				// Most Items (but not buttons)
			MetaList[MetaGroup.Button] = new IMetaData(Arch.Item, LoadOrder.Item, this.atlas[(byte)AtlasGroup.Other], SlotGroup.ColorToggles, LayerEnum.Main);
				// Buttons
			MetaList[MetaGroup.Generator] = new IMetaData(Arch.Generator, LoadOrder.Block, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Fixed, LayerEnum.Main);
				// Cannon, Placer
			MetaList[MetaGroup.Collectable] = new IMetaData(Arch.Collectable, LoadOrder.Collectable, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Collectables, LayerEnum.Main);
				// Collectables
			MetaList[MetaGroup.Track] = new IMetaData(Arch.HiddenObject, LoadOrder.Invisible, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Movers, LayerEnum.Main);
				// Track, Clusuter
			MetaList[MetaGroup.Door] = new IMetaData(Arch.Portal, LoadOrder.Portal, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Interactives, LayerEnum.Main);
			MetaList[MetaGroup.Interactives] = new IMetaData(Arch.Interactives, LoadOrder.Interactives, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Interactives, LayerEnum.Main);
				// Chest, PeekMap
			MetaList[MetaGroup.Flag] = new IMetaData(Arch.Collectable, LoadOrder.Interactives, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Interactives, LayerEnum.Main);
			MetaList[MetaGroup.NPC] = new IMetaData(Arch.Interactives, LoadOrder.Interactives, this.atlas[(byte)AtlasGroup.Characters], SlotGroup.Interactives, LayerEnum.Main);
		}

		// List of Tile Types
		// This includes any tile that isn't "foreground", including full-space tiles.
		// Also includes fixed objects that can accept tile behavior for collision detection.
		public Dictionary<ushort, Type> TileMap = new Dictionary<ushort, Type>() {
			
			// Ground, Immutable (0 - 9)
			{ 1, Type.GetType("Nexus.Objects.GroundGrass") },
			{ 2, Type.GetType("Nexus.Objects.GroundDirt") },
			{ 3, Type.GetType("Nexus.Objects.GroundMud") },
			{ 4, Type.GetType("Nexus.Objects.GroundStone") },
			{ 5, Type.GetType("Nexus.Objects.GroundSnow") },
			{ 6, Type.GetType("Nexus.Objects.GroundSlime") },
			{ 7, Type.GetType("Nexus.Objects.GroundCloud") },

			// Ledges (10 - 19)
			{ 10, Type.GetType("Nexus.Objects.LedgeGrass") },

			// Fixed, Immutable (20 - 29)
			{ 20, Type.GetType("Nexus.Objects.GroundWall") },
			{ 21, Type.GetType("Nexus.Objects.GroundLog") },
			
			// Pseudo-Tiles, Fixed, Unmoveable (50 - 79)
			{ 50, Type.GetType("Nexus.Objects.Spike") },
			{ 55, Type.GetType("Nexus.Objects.ExclaimBlock") },
			{ 56, Type.GetType("Nexus.Objects.BoxToggle") },
			{ 60, Type.GetType("Nexus.Objects.PuffBlock") },
			{ 61, Type.GetType("Nexus.Objects.Conveyor") },
			{ 70, Type.GetType("Nexus.Objects.PlatformFixed") },
			{ 75, Type.GetType("Nexus.Objects.PlatformToggleOn") },
			{ 76, Type.GetType("Nexus.Objects.PlatformToggleOff") },
			
			// Solid, but Moveable (100 - 149)
			{ 100, Type.GetType("Nexus.Objects.FixedBox") },
			{ 101, Type.GetType("Nexus.Objects.FixedBrick") },
			{ 105, Type.GetType("Nexus.Objects.FixedLeaf") },
			{ 110, Type.GetType("Nexus.Objects.FixedLock") },
			{ 130, Type.GetType("Nexus.Objects.ToggleBlockOn") },
			{ 131, Type.GetType("Nexus.Objects.ToggleBlockOff") },
			
			// Generators (270 - 279)
			{ 270, Type.GetType("Nexus.Objects.Cannon") },
			{ 271, Type.GetType("Nexus.Objects.Placer") },
			
			// Interactives (280 - 289)
			{ 280, Type.GetType("Nexus.Objects.Flag") },
			{ 281, Type.GetType("Nexus.Objects.Chest") },
			{ 282, Type.GetType("Nexus.Objects.NPC") },
			{ 283, Type.GetType("Nexus.Objects.PeekMap") },
			{ 285, Type.GetType("Nexus.Objects.Door") },
			{ 286, Type.GetType("Nexus.Objects.DoorLock") },
			
			// Collectables (300 - 309)
			{ 300, Type.GetType("Nexus.Objects.CollectableCoin") },
			{ 301, Type.GetType("Nexus.Objects.CollectableGoodie") },
			{ 302, Type.GetType("Nexus.Objects.CollectableSuit") },
			{ 303, Type.GetType("Nexus.Objects.CollectableHat") },
			{ 304, Type.GetType("Nexus.Objects.CollectablePower") },
		};
		
		// List of Foreground Tile Types
		// This includes decorations, prompts, or tiles that appear in the front; never collide.
		public Dictionary<ushort, Type> FGTileMap = new Dictionary<ushort, Type>() {
			
			// Decor, Immutable (30 - 49)
			{ 30, Type.GetType("Nexus.Objects.DecorVeg") },
			{ 31, Type.GetType("Nexus.Objects.DecorDesert") },
			{ 32, Type.GetType("Nexus.Objects.DecorCave") },
			{ 33, Type.GetType("Nexus.Objects.DecorWater") },

			{ 40, Type.GetType("Nexus.Objects.DecorPet") },
			{ 41, Type.GetType("Nexus.Objects.DecorItems") },

			{ 42, Type.GetType("Nexus.Objects.PromptArrow") },
			{ 43, Type.GetType("Nexus.Objects.PromptSign") },
		};

		// List of Game Object Types
		public Dictionary<ushort, Type> ObjectMap = new Dictionary<ushort, Type>() {

			// Special Flags and Placements (95 - 99)
			{ 95, Type.GetType("Nexus.Objects.Character") },

			// Platforms (100 - 149)
			{ 120, Type.GetType("Nexus.Objects.PlatformDip") },
			{ 121, Type.GetType("Nexus.Objects.PlatformDelay") },
			{ 122, Type.GetType("Nexus.Objects.PlatformFall") },
			{ 123, Type.GetType("Nexus.Objects.PlatformMove") },
			
			// Land & Fixed Enemies (150 - 199)
			{ 150, Type.GetType("Nexus.Objects.FixedChomper") },
			{ 151, Type.GetType("Nexus.Objects.FixedChomperFire") },
			{ 152, Type.GetType("Nexus.Objects.FixedPlant") },

			{ 160, Type.GetType("Nexus.Objects.LandMoosh") },
			{ 161, Type.GetType("Nexus.Objects.LandShroom") },
			{ 162, Type.GetType("Nexus.Objects.LandBug") },
			{ 163, Type.GetType("Nexus.Objects.LandGoo") },
			{ 164, Type.GetType("Nexus.Objects.LandLiz") },
			{ 167, Type.GetType("Nexus.Objects.LandSnek") },
			{ 168, Type.GetType("Nexus.Objects.LandWurm") },

			{ 170, Type.GetType("Nexus.Objects.LandTurtle") },
			{ 171, Type.GetType("Nexus.Objects.LandSnail") },
			{ 175, Type.GetType("Nexus.Objects.LandBoom") },
			{ 180, Type.GetType("Nexus.Objects.LandOcto") },
			{ 181, Type.GetType("Nexus.Objects.LandBones") },
			{ 182, Type.GetType("Nexus.Objects.LandPoke") },
			{ 185, Type.GetType("Nexus.Objects.LandLich") },
			
			// Flight Enemies (200 - 229)
			{ 200, Type.GetType("Nexus.Objects.FlightBuzz") },
			{ 201, Type.GetType("Nexus.Objects.Saw") },
			{ 205, Type.GetType("Nexus.Objects.FlightDire") },
			{ 210, Type.GetType("Nexus.Objects.ElementalAir") },
			{ 211, Type.GetType("Nexus.Objects.ElementalEarth") },
			{ 212, Type.GetType("Nexus.Objects.ElementalFire") },
			{ 215, Type.GetType("Nexus.Objects.Ghost") },
			{ 216, Type.GetType("Nexus.Objects.FlairElectric") },
			{ 217, Type.GetType("Nexus.Objects.FlairFire") },
			{ 218, Type.GetType("Nexus.Objects.FlairMagic") },
			{ 220, Type.GetType("Nexus.Objects.ElementalEye") },
			{ 221, Type.GetType("Nexus.Objects.ElementalMini") },
			{ 225, Type.GetType("Nexus.Objects.Slammer") },
			
			// Items, Handheld (230 - 269)
			{ 230, Type.GetType("Nexus.Objects.Shell") },
			{ 231, Type.GetType("Nexus.Objects.Boulder") },
			{ 235, Type.GetType("Nexus.Objects.TNT") },
			{ 236, Type.GetType("Nexus.Objects.Bomb") },
			{ 240, Type.GetType("Nexus.Objects.ButtonStandard") },
			{ 241, Type.GetType("Nexus.Objects.ButtonFixed") },
			{ 242, Type.GetType("Nexus.Objects.ButtonTimed") },
			{ 245, Type.GetType("Nexus.Objects.SpringFixed") },
			{ 246, Type.GetType("Nexus.Objects.SpringStandard") },
			{ 250, Type.GetType("Nexus.Objects.Handheld") },

			// Special (290 - 299)
			{ 290, Type.GetType("Nexus.Objects.Cluster") },
			{ 291, Type.GetType("Nexus.Objects.Track") },
			{ 292, Type.GetType("Nexus.Objects.GrowObj") },
		};
	}
}
