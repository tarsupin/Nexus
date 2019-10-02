
using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Gameplay {

	public class IMetaData {
		public Arch Archetype { get; }
		public LoadOrder LoadOrder { get; }
		public Atlas Atlas { get; }
		public LayerEnum Layer { get; }
		public SlotGroup SlotGroup { get; }

		public IMetaData( Arch arch, LoadOrder loadOrder, Atlas atlas, SlotGroup slotGroup, LayerEnum layer ) {
			this.Archetype = arch;
			this.LoadOrder = loadOrder;
			this.Atlas = atlas;
			this.SlotGroup = slotGroup;
			this.Layer = layer;
		}
	}

	public enum MetaGroup {
		Ground,
		Ledge,
		Decor,
		Block,
		ToggleBlock,
		Conveyor,
		Platform,
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
		NPC
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
		Blocks = 0,
		Characters = 1,
		Enemies = 2,
		Icons = 3,
		Other = 4,
		World = 5,
	}

	public enum LayerEnum : byte {
		Background = 0,
		Main = 1,
		Cosmetic = 2,
	}

	// LoadOrder is used to determine rendering layers.
	public enum LoadOrder : byte {
		Static,         // This is here so that Game.objects[] can contain 'Static' values. Side-effect of original code before enum.
		Invisible,
		Background,
		Block,
		MovingBlock,
		ToggleBlock,
		Decor,
		Portal,
		Interactives,
		Platform,
		Collectable,
		Enemy,
		ToggleBlockMobile,
		Item,
		TrailingItem,
		Character,
		Projectile,
	}

	public enum SlotGroup : byte {
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
