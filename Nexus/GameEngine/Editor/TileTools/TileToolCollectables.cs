using Nexus.Gameplay;
using Nexus.Objects;
using static Nexus.Objects.Goodie;

namespace Nexus.GameEngine {

	public class TileToolCollectables : TileTool {

		public TileToolCollectables() : base() {

			this.slotGroup = (byte)SlotGroup.Collectables;

			// Coins
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Coins,
					subType = (byte) CoinsSubType.Coin,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Coins,
					subType = (byte) CoinsSubType.Gem,
				},
			});

			// Goodies (Key)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Key,
				},
			});

			// Goodies (Health)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Apple,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Pear,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Melon,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Soup,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Pack1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Pack2,
				},
			});

			// Goodies (Armor)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.ShieldWood,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.ShieldWhite,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.ShieldBlue,
				},
			});

			// Goodies (Protection)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.RingMagic,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.AmuletMagic,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.RingFire,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.NeckFire,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.RingPoison,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.NeckElectric,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.RingElements,
				},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.Goodie,
				//	subType = (byte) GoodieSubType.NeckHeart,
				//},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.Goodie,
				//	subType = (byte) GoodieSubType.RingHawk,
				//},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.Goodie,
				//	subType = (byte) GoodieSubType.RingDruid,
				//},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.Goodie,
				//	subType = (byte) GoodieSubType.RingEye,
				//},
			});

			// Goodies (Invincibility)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Shiny,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Stars,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.GodMode,
				},
			});

			// Goodies (Effect)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Explosive,
				},
			});

			// Goodies (Time)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Set5,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Set10,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Set20,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Plus5,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Plus10,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Plus20,
				},
			});
		}
	}
}
