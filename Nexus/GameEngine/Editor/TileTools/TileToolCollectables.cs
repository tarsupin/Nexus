using Nexus.Gameplay;
using static Nexus.Objects.Goodie;

namespace Nexus.GameEngine {

	public class TileToolCollectables : TileTool {

		public TileToolCollectables() : base() {

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
					subType = (byte) GoodieSubType.Heart,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Shield,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.ShieldPlus,
				},
			});
			
			// Goodies (Protection)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Guard,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.GuardPlus,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Explosive,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Disrupt,
				},
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
