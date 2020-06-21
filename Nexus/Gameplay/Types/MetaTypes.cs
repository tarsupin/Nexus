using Nexus.Engine;

namespace Nexus.Gameplay {

	public class IMetaData {
		public readonly Arch Archetype;
		public readonly LoadOrder LoadOrder;
		public readonly Atlas Atlas;
		public readonly LayerEnum Layer;
		public readonly SlotGroup SlotGroup;

		public IMetaData( Arch arch, Atlas atlas, SlotGroup slotGroup, LayerEnum layer, LoadOrder loadOrder ) {
			this.Archetype = arch;
			this.LoadOrder = loadOrder;
			this.Atlas = atlas;
			this.SlotGroup = slotGroup;
			this.Layer = layer;
		}
	}

	public enum MetaGroup : byte {
		Detect,
		Ground,
		Ledge,
		Decor,
		BGTile,
		Block,
		ToggleBlock,
		Conveyor,
		Platform,
		Character,
		EnemyFixed,
		EnemyLand,
		EnemyFly,
		BlockMoving,
		Item,
		Button,
		ButtonFixed,
		Generator,
		Collectable,
		Track,
		Door,
		Interactives,
		Flag,
		NPC,
		Projectile,
	}

	public enum Arch : byte {
		BGTile,
		Block,
		Character,
		Collectable,
		Enemy,
		Generator,
		Ground,
		HiddenObject,
		Interactives,
		Item,
		MovingBlock,
		Platform,
		Portal,
		Projectile,
		Decor,
		Special,
		ToggleBlock,
		ToggleBlockMobile,
		TrailingItem,
		Static,
	}

	public enum SetupRules : byte {
		NoSpecialRules = 0,
		SetupTile = 1,
		PreSetupOnly = 2,
	}

	public enum AtlasGroup : byte {
		Tiles = 0,
		Objects = 1,
		World = 2,
	}

	public enum LayerEnum : byte {
		main = 0,
		obj = 1,
		fg = 2,
		bg = 3,
	}

	// LoadOrder is used to determine rendering layers.
	public enum LoadOrder : byte {
		Tile,
		//Static,         // This is here so that Game.objects[] can contain 'Static' values. Side-effect of original code before enum.
		//Invisible,
		//Background,
		//Block,
		//MovingBlock,
		//ToggleBlock,
		//Decor,
		//Portal,
		//Interactives,
		Platform,
		//Collectable,
		Enemy,
		//ToggleBlockMobile,			// TODO: This should be in "Item" LoadOrder, and otherwise handled in a Tile Game Object, like with Toggle Block.
		Item,
		TrailingItem,
		Character,
		Projectile,
	}

	// The Tile Slot Groups (for Editing Levels)
	public enum SlotGroup : byte {
		None,
		Ground,
		Blocks,
		ColorToggles,
		Platforms,
		EnemiesLand,
		EnemiesFly,
		Interactives,
		Upgrades,
		Collectables,
		Decor,
		Prompts,
		Gadgets,
		Scripting,
	}

	// The World TIle Slot Groups
	public enum WorldSlotGroup : byte {
		None,
		Terrain,
		Detail,
		Coverage,
		Objects,
		Nodes,
	}
}
