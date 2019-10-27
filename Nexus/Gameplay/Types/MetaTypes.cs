using Nexus.Engine;

namespace Nexus.Gameplay {

	public class IMetaData {
		public Arch Archetype { get; }
		public LoadOrder LoadOrder { get; }
		public Atlas Atlas { get; }
		public LayerEnum Layer { get; }
		public SlotGroup SlotGroup { get; }

		public IMetaData( Arch arch, Atlas atlas, SlotGroup slotGroup, LayerEnum layer, LoadOrder loadOrder ) {
			this.Archetype = arch;
			this.LoadOrder = loadOrder;
			this.Atlas = atlas;
			this.SlotGroup = slotGroup;
			this.Layer = layer;
		}
	}

	public enum MetaGroup : byte {
		Ground,
		Ledge,
		Decor,
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
		BGObject,
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

	public enum AtlasGroup : byte {
		Tiles = 0,
		Objects = 1,
		World = 2,
	}

	public enum LayerEnum : byte {
		Background = 0,
		Main = 1,
		Cosmetic = 2,
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

	public enum SlotGroup : byte {
		None,
		Blocks,
		Collectables,
		ColorToggles,
		EnemyLand,
		EnemyFly,
		Fixed,
		Interactives,
		Items,
		Movers,
		Prompts,
	}
}
