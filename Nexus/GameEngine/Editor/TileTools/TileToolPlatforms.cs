using Nexus.Gameplay;
using static Nexus.Objects.Conveyor;

namespace Nexus.GameEngine {

	public class TileToolPlatforms : TileTool {

		public TileToolPlatforms() : base() {

			this.slotGroup = (byte)SlotGroup.Platforms;

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixed,
					subType = (byte) HorizontalSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixed,
					subType = (byte) HorizontalSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixed,
					subType = (byte) HorizontalSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixed,
					subType = (byte) HorizontalSubTypes.H3,
				},
			});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatFall,
			//		subType = (byte) HorizontalSubTypes.S,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatDelay,
			//		subType = (byte) HorizontalSubTypes.S,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatDip,
			//		subType = (byte) HorizontalSubTypes.S,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatMove,
			//		subType = (byte) HorizontalSubTypes.S,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatMove,
			//		subType = (byte) HorizontalSubTypes.S,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatMove,
			//		subType = (byte) HorizontalSubTypes.S,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatMove,
			//		subType = (byte) HorizontalSubTypes.S,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatItem,
			//		subType = (byte) HorizontalSubTypes.S,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatItem,
			//		subType = (byte) HorizontalSubTypes.H1,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatItem,
			//		subType = (byte) HorizontalSubTypes.H2,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatItem,
			//		subType = (byte) HorizontalSubTypes.H3,
			//	},
			//});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Conveyor,
					subType = (byte) ConveyorSubType.Left,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Conveyor,
					subType = (byte) ConveyorSubType.Right,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Conveyor,
					subType = (byte) ConveyorSubType.SlowLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Conveyor,
					subType = (byte) ConveyorSubType.SlowRight,
				},
			});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Cluster,
			//		subType = (byte) Something.Cluster,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Cluster,
			//		subType = (byte) Something.Char,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Cluster,
			//		subType = (byte) Something.Screen,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Track,
			//		subType = (byte) Something.Blue,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Track,
			//		subType = (byte) Something.Green,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Track,
			//		subType = (byte) Something.Red,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.BGWind,
			//		subType = (byte) Something.Left,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.BGWind,
			//		subType = (byte) Something.Right,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.BGWind,
			//		subType = (byte) Something.Up,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.BGWind,
			//		subType = (byte) Something.Down,
			//	},
			//});

		}
	}
}
