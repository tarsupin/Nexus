using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class TileToolPlatforms : TileTool {

		public TileToolPlatforms() : base() {

			this.slotGroup = (byte)SlotGroup.Platforms;

			// TODO LOW PRIORITY: Add this whole section

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatSolid,
			//		subType = (byte) Something.S,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatSolid,
			//		subType = (byte) Something.S,
			//		face: DirRotate.Right,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatSolid,
			//		subType = (byte) Something.S,
			//		face: DirRotate.FlipVert,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatSolid,
			//		subType = (byte) Something.S,
			//		face: DirRotate.Left,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatFall,
			//		subType = (byte) Something.S,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatDelay,
			//		subType = (byte) Something.S,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatDip,
			//		subType = (byte) Something.S,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatMove,
			//		subType = (byte) Something.S,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatMove,
			//		subType = (byte) Something.S,
			//		face: DirRotate.Right,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatMove,
			//		subType = (byte) Something.S,
			//		face: DirRotate.FlipVert,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatMove,
			//		subType = (byte) Something.S,
			//		face: DirRotate.Left,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatItem,
			//		subType = (byte) Something.Top,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatItem,
			//		subType = (byte) Something.Top,
			//		face: DirRotate.Right,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatItem,
			//		subType = (byte) Something.Top,
			//		face: DirRotate.FlipVert,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PlatItem,
			//		subType = (byte) Something.Top,
			//		face: DirRotate.Left,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Conveyor,
			//		subType = (byte) Something.Left,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Conveyor,
			//		subType = (byte) Something.Right,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Conveyor,
			//		subType = (byte) Something.SlowLeft,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Conveyor,
			//		subType = (byte) Something.SlowRight,
			//	},
			//});

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
