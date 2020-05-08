using Nexus.Gameplay;
using Nexus.Objects;
using static Nexus.Objects.CannonDiag;
using static Nexus.Objects.CannonHor;
using static Nexus.Objects.CannonVert;

namespace Nexus.GameEngine {

	public class TileToolGadgets : TileTool {

		public TileToolGadgets() : base() {

			this.slotGroup = (byte)SlotGroup.Gadgets;

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonHorizontal,
					subType = (byte) CannonHorSubType.Left,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonHorizontal,
					subType = (byte) CannonHorSubType.Right,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonVertical,
					subType = (byte) CannonVertSubType.Up,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonVertical,
					subType = (byte) CannonVertSubType.Down,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonDiagonal,
					subType = (byte) CannonDiagSubType.DownLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonDiagonal,
					subType = (byte) CannonDiagSubType.DownRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonDiagonal,
					subType = (byte) CannonDiagSubType.UpLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonDiagonal,
					subType = (byte) CannonDiagSubType.UpRight,
				},
			});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Placer,
			//		subType = (byte) Something.Horizontal,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Placer,
			//		subType = (byte) Something.Vertical,
			//		face: DirRotate.FlipVert,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Placer,
			//		subType = (byte) Something.Horizontal,
			//		face: DirRotate.FlipHor,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Placer,
			//		subType = (byte) Something.Vertical,
			//	},
			//});

			// TODO LOW PRIORITY: Add this whole section

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.SpringFixed,
			//		subType = (byte) Something.Standard,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.SpringFixed,
			//		subType = (byte) Something.Standard,
			//		face: DirRotate.Right,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.SpringFixed,
			//		subType = (byte) Something.Standard,
			//		face: DirRotate.FlipVert,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.SpringFixed,
			//		subType = (byte) Something.Standard,
			//		face: DirRotate.Left,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.SpringStandard,
			//		subType = (byte) Something.Standard,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Shell,
			//		subType = (byte) Something.Red,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Shell,
			//		subType = (byte) Something.Green,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Shell,
			//		subType = (byte) Something.GreenWing,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Shell,
			//		subType = (byte) Something.Heavy,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Boulder,
			//		subType = (byte) Something.Boulder,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.TNT,
			//		subType = (byte) Something.Standard,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Handheld,
			//		subType = (byte) Something.Feather,
			//	},
			//});
		}
	}
}
