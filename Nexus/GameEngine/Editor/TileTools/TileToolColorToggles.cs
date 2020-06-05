using Nexus.Gameplay;
using static Nexus.Objects.BGTap;

namespace Nexus.GameEngine {

	public class TileToolColorToggles : TileTool {

		public TileToolColorToggles() : base() {

			this.slotGroup = (byte)SlotGroup.ColorToggles;

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
					tileId = (byte) TileEnum.PlatBlueUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatRedUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatBlueRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatRedRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatBlueDown,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatRedDown,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatBlueLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatRedLeft,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatGreenUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatYellowUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatGreenRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatYellowRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatGreenDown,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatYellowDown,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatGreenLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatYellowLeft,
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

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.BGTap,
					subType = (byte) BGTapSubType.TapBR,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.BGTap,
					subType = (byte) BGTapSubType.TapGY,
				},
			});
		}
	}
}
