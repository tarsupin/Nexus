using Microsoft.Xna.Framework.Graphics;
using Nexus.Engine;
using System;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class GameMapper {

		public readonly Atlas[] atlas;
		public Dictionary<MetaGroup, IMetaData> MetaList = new Dictionary<MetaGroup, IMetaData>();
		// TileMap, FGTileMap, ObjectMap
		
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
			MetaList[MetaGroup.Ground] = new IMetaData(Arch.Ground, this.atlas[(byte) AtlasGroup.Blocks], SlotGroup.Blocks, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Block
				// All Ground, Leaf, Lock, GrowObj
			MetaList[MetaGroup.Ledge] = new IMetaData(Arch.Platform, this.atlas[(byte) AtlasGroup.Blocks], SlotGroup.Blocks, LayerEnum.Main, LoadOrder.Platform);
			MetaList[MetaGroup.Decor] = new IMetaData(Arch.Decor, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Prompts, LayerEnum.Cosmetic, LoadOrder.Tile); // LoadOrder.Decor
			MetaList[MetaGroup.Block] = new IMetaData(Arch.Block, this.atlas[(byte)AtlasGroup.Blocks], SlotGroup.Blocks, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Block
				// PuffBlock, Exclaim, Box, Brick
			MetaList[MetaGroup.ToggleBlock] = new IMetaData(Arch.ToggleBlock, this.atlas[(byte)AtlasGroup.Blocks], SlotGroup.ColorToggles, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Block
				// ToggleBlock, ToggleOnPlat, ToggleOffPlat, ToggleOn, ToggleOff, 
			MetaList[MetaGroup.Conveyor] = new IMetaData(Arch.Block, this.atlas[(byte)AtlasGroup.Blocks], SlotGroup.Movers, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Block
				// Conveyor
			MetaList[MetaGroup.Platform] = new IMetaData(Arch.Platform, this.atlas[(byte)AtlasGroup.Blocks], SlotGroup.Movers, LayerEnum.Main, LoadOrder.Platform);
				// PlatSolid, PlatMove, PlatFall, PlatDip, PlatDelay
			MetaList[MetaGroup.EnemyFixed] = new IMetaData(Arch.Enemy, this.atlas[(byte)AtlasGroup.Enemies], SlotGroup.Fixed, LayerEnum.Main, LoadOrder.Enemy);
				// Chomper, Fire Chomper, Plant
			MetaList[MetaGroup.EnemyLand] = new IMetaData(Arch.Enemy, this.atlas[(byte)AtlasGroup.Enemies], SlotGroup.EnemyLand, LayerEnum.Main, LoadOrder.Enemy);
			MetaList[MetaGroup.EnemyFly] = new IMetaData(Arch.Enemy, this.atlas[(byte)AtlasGroup.Enemies], SlotGroup.EnemyFly, LayerEnum.Main, LoadOrder.Enemy);
			MetaList[MetaGroup.BlockMoving] = new IMetaData(Arch.MovingBlock, this.atlas[(byte)AtlasGroup.Enemies], SlotGroup.EnemyFly, LayerEnum.Main, LoadOrder.Enemy);
				// Slammer
			MetaList[MetaGroup.Item] = new IMetaData(Arch.Item, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Items, LayerEnum.Main, LoadOrder.Item);
				// Most Items (but not buttons)
			MetaList[MetaGroup.Button] = new IMetaData(Arch.Item, this.atlas[(byte)AtlasGroup.Other], SlotGroup.ColorToggles, LayerEnum.Main, LoadOrder.Item);
				// Buttons
			MetaList[MetaGroup.Generator] = new IMetaData(Arch.Generator, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Fixed, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Block
				// Cannon, Placer
			MetaList[MetaGroup.Collectable] = new IMetaData(Arch.Collectable, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Collectables, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Collectable
				// Collectables
			MetaList[MetaGroup.Track] = new IMetaData(Arch.HiddenObject, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Movers, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Invisible
				// Track, Clusuter
			MetaList[MetaGroup.Door] = new IMetaData(Arch.Portal, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Interactives, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Portal
			MetaList[MetaGroup.Interactives] = new IMetaData(Arch.Interactives, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Interactives, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Interactives
				// Chest, PeekMap
			MetaList[MetaGroup.Flag] = new IMetaData(Arch.Collectable, this.atlas[(byte)AtlasGroup.Other], SlotGroup.Interactives, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Interactives
			MetaList[MetaGroup.NPC] = new IMetaData(Arch.Interactives, this.atlas[(byte)AtlasGroup.Characters], SlotGroup.Interactives, LayerEnum.Main, LoadOrder.Tile); // LoadOrder.Interactives
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
			
			// Reserved (30 - 39)

			// Anything below this section has an ObjectID, Update(), and Passive Collision.

			// Fixed, Touch-Effect (40 - 49)
			{ 40, Type.GetType("Nexus.Objects.Brick") },
			{ 41, Type.GetType("Nexus.Objects.Box") },
			{ 42, Type.GetType("Nexus.Objects.Lock") },
			{ 43, Type.GetType("Nexus.Objects.Leaf") },
			{ 44, Type.GetType("Nexus.Objects.ExclaimBlock") },
			{ 45, Type.GetType("Nexus.Objects.BoxToggle") },
			{ 46, Type.GetType("Nexus.Objects.Spike") },
			{ 47, Type.GetType("Nexus.Objects.PuffBlock") },
			{ 48, Type.GetType("Nexus.Objects.Conveyor") },

			// Solid, Toggled (50 - 54)
			{ 50, Type.GetType("Nexus.Objects.ToggleBlockOn") },
			{ 51, Type.GetType("Nexus.Objects.ToggleBlockOff") },
			{ 52, Type.GetType("Nexus.Objects.PlatformToggleOn") },
			{ 53, Type.GetType("Nexus.Objects.PlatformToggleOff") },
			
			// Generators (55 - 59)
			{ 55, Type.GetType("Nexus.Objects.Cannon") },
			{ 56, Type.GetType("Nexus.Objects.Placer") },
			
			// Reserved (60 - 69)

			// Anything below can only be interacted with by a character:
			// These will have Passive Collision by Character Only.

			// Character Interactives (70 - 79)
			{ 70, Type.GetType("Nexus.Objects.Flag") },
			{ 71, Type.GetType("Nexus.Objects.Chest") },
			{ 72, Type.GetType("Nexus.Objects.NPC") },
			{ 73, Type.GetType("Nexus.Objects.PeekMap") },

			{ 75, Type.GetType("Nexus.Objects.Door") },
			{ 76, Type.GetType("Nexus.Objects.DoorLock") },
			
			// Collectables (80 - 89)
			{ 80, Type.GetType("Nexus.Objects.CollectableCoin") },
			{ 81, Type.GetType("Nexus.Objects.CollectableGoodie") },
			{ 82, Type.GetType("Nexus.Objects.CollectableSuit") },
			{ 83, Type.GetType("Nexus.Objects.CollectableHat") },
			{ 84, Type.GetType("Nexus.Objects.CollectablePower") },
		};
		
		// List of Foreground Tile Types
		// This includes decorations, prompts, or tiles that appear in the front; never collide.
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

			{ 50, Type.GetType("Nexus.Objects.Buzz") },

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

			// Special Flags and Placements (100+)
			{ 100, Type.GetType("Nexus.Objects.Character") },
		};
	}
}
