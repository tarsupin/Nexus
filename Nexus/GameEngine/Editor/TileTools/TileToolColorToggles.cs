﻿using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class TileToolColorToggles : TileTool {

		public TileToolColorToggles() : base() {

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ToggleBlockBlue,
					subType = (byte) 0,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ToggleBlockRed,
					subType = (byte) 0,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ToggleBlockGreen,
					subType = (byte) 0,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ToggleBlockYellow,
					subType = (byte) 0,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatBlue,
					subType = (byte) FacingSubType.FaceUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatRed,
					subType = (byte) FacingSubType.FaceUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatBlue,
					subType = (byte) FacingSubType.FaceLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatRed,
					subType = (byte) FacingSubType.FaceLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatBlue,
					subType = (byte) FacingSubType.FaceDown,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatRed,
					subType = (byte) FacingSubType.FaceDown,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatBlue,
					subType = (byte) FacingSubType.FaceRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatRed,
					subType = (byte) FacingSubType.FaceRight,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatGreen,
					subType = (byte) FacingSubType.FaceUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatYellow,
					subType = (byte) FacingSubType.FaceUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatGreen,
					subType = (byte) FacingSubType.FaceLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatYellow,
					subType = (byte) FacingSubType.FaceLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatGreen,
					subType = (byte) FacingSubType.FaceDown,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatYellow,
					subType = (byte) FacingSubType.FaceDown,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatGreen,
					subType = (byte) FacingSubType.FaceRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatYellow,
					subType = (byte) FacingSubType.FaceRight,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ToggleBoxBR,
					subType = (byte) 0,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ToggleBoxGY,
					subType = (byte) 0,
				},
			});

			// TODO LOW PRIORITY: FINISH
			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ToggleOnMobile,
			//		subType = (byte) Something.Blue,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ToggleOnMobile,
			//		subType = (byte) Something.Green,
			//	},
			//});

			// TODO LOW PRIORITY: FINISH
			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ToggleOffMobile,
			//		subType = (byte) Something.Red,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ToggleOffMobile,
			//		subType = (byte) Something.Yellow,
			//	},
			//});

			// TODO LOW PRIORITY: FINISH
			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ButtonStandard,
			//		subType = (byte) Something.BR,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ButtonStandard,
			//		subType = (byte) Something.GY,
			//	},
			//});

			// TODO LOW PRIORITY: FINISH
			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ButtonFixed,
			//		subType = (byte) Something.BR,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ButtonFixed,
			//		subType = (byte) Something.GY,
			//	},
			//});

			// TODO LOW PRIORITY: FINISH
			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ButtonTimed,
			//		subType = (byte) Something.BR,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ButtonTimed,
			//		subType = (byte) Something.GY,
			//	},
			//});

			// TODO LOW PRIORITY: FINISH
			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.BGTap,
			//		subType = (byte) Something.BR,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.BGTap,
			//		subType = (byte) Something.GY,
			//	},
			//});
		}
	}
}
