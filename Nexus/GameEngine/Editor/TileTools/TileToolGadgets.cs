﻿using Nexus.Gameplay;
using Nexus.Objects;
using static Nexus.Objects.CannonDiag;
using static Nexus.Objects.CannonHor;
using static Nexus.Objects.CannonVert;
using static Nexus.Objects.Placer;

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

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Placer,
					subType = (byte) PlacerSubType.Up,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Placer,
					subType = (byte) PlacerSubType.Right,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Placer,
					subType = (byte) PlacerSubType.Down,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Placer,
					subType = (byte) PlacerSubType.Left,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ButtonFixed,
					subType = (byte) ButtonSubTypes.BR,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ButtonFixed,
					subType = (byte) ButtonSubTypes.GY,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ButtonTimed,
					subType = (byte) ButtonSubTypes.BR,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ButtonTimed,
					subType = (byte) ButtonSubTypes.GY,
				},
			});

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
