using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class TileToolColorToggles : TileTool {

		public TileToolColorToggles() : base() {

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ToggleBlockBlue,
					subType = (byte) 0,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ToggleBlockGreen,
					subType = (byte) 0,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ToggleBlockRed,
					subType = (byte) 0,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ToggleBlockYellow,
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

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatBlue,
					subType = (byte) 0,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatGreen,
					subType = (byte) 0,
				},

				// TODO LOW PRIORITY: FINISH
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.ToggleOnPlat,
				//	subType = (byte) Something.Blue,
				//	face: DirRotate.Right,
				//},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.ToggleOnPlat,
				//	subType = (byte) Something.Green,
				//	face: DirRotate.Right,
				//},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.ToggleOnPlat,
				//	subType = (byte) Something.Blue,
				//	face: DirRotate.FlipVert,
				//},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.ToggleOnPlat,
				//	subType = (byte) Something.Green,
				//	face: DirRotate.FlipVert,
				//},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.ToggleOnPlat,
				//	subType = (byte) Something.Blue,
				//	face: DirRotate.Left,
				//},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.ToggleOnPlat,
				//	subType = (byte) Something.Green,
				//	face: DirRotate.Left,
				//},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatRed,
					subType = (byte) 0,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TogglePlatYellow,
					subType = (byte) 0,
				},

				// TODO LOW PRIORITY: FINISH
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.ToggleOffPlat,
				//	subType = (byte) Something.Red,
				//	face: DirRotate.Right,
				//},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.ToggleOffPlat,
				//	subType = (byte) Something.Yellow,
				//	face: DirRotate.Right,
				//},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.ToggleOffPlat,
				//	subType = (byte) Something.Red,
				//	face: DirRotate.FlipVert,
				//},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.ToggleOffPlat,
				//	subType = (byte) Something.Yellow,
				//	face: DirRotate.FlipVert,
				//},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.ToggleOffPlat,
				//	subType = (byte) Something.Red,
				//	face: DirRotate.Left,
				//},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.ToggleOffPlat,
				//	subType = (byte) Something.Yellow,
				//	face: DirRotate.Left,
				//},
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

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Lock,
					subType = 0,
				},
			});

			// TODO LOW PRIORITY: FINISH
			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Chest,
			//		subType = (byte) Something.Closed,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Chest,
			//		subType = (byte) Something.Lock,
			//	},
			//});
		}
	}
}
