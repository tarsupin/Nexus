using Nexus.Gameplay;
using Nexus.Objects;
using static Nexus.Objects.Plant;

namespace Nexus.GameEngine {

	public class TileToolEnemyLand : TileTool {

		public TileToolEnemyLand() : base() {

			this.slotGroup = (byte)SlotGroup.EnemiesLand;

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ChomperGrass,
					subType = (byte) FacingSubType.FaceUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ChomperGrass,
					subType = (byte) FacingSubType.FaceRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ChomperGrass,
					subType = (byte) FacingSubType.FaceDown,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ChomperGrass,
					subType = (byte) FacingSubType.FaceLeft,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ChomperMetal,
					subType = (byte) FacingSubType.FaceUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ChomperMetal,
					subType = (byte) FacingSubType.FaceRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ChomperMetal,
					subType = (byte) FacingSubType.FaceDown,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ChomperMetal,
					subType = (byte) FacingSubType.FaceLeft,
				},
			});
			
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ChomperFire,
					subType = (byte) FacingSubType.FaceUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ChomperFire,
					subType = (byte) FacingSubType.FaceRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ChomperFire,
					subType = (byte) FacingSubType.FaceDown,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ChomperFire,
					subType = (byte) FacingSubType.FaceLeft,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Plant,
					subType = (byte) PlantSubType.Plant,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Plant,
					subType = (byte) PlantSubType.Metal,
				},
			});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.HoveringEye,
			//		subType = (byte) Something.Standard,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Vin,
			//		subType = (byte) Something.Vin,
			//	},
			//});


			// TODO LOW PRIORITY: Add this whole section
			//this.placeholdersAdd(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Goo,
			//		subType = (byte) Something.Green,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Goo,
			//		subType = (byte) Something.Blue,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Goo,
			//		subType = (byte) Something.Orange,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Snail,
			//		subType = (byte) Something.Standard,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Bug,
			//		subType = (byte) Something.Standard,
			//	},
			//});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Moosh,
					subType = (byte) MooshSubType.Brown,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Moosh,
					subType = (byte) MooshSubType.Purple,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Moosh,
					subType = (byte) MooshSubType.White,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Shroom,
					subType = (byte) ShroomSubType.Red,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Shroom,
					subType = (byte) ShroomSubType.Purple,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Shroom,
					subType = (byte) ShroomSubType.Black,
				},
			});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Snek,
			//		subType = (byte) Something.Snek,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Wurm,
			//		subType = (byte) Something.Wurm,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Octo,
			//		subType = (byte) Something.Octo,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Boom,
			//		subType = (byte) Something.Boom,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Turtle,
			//		subType = (byte) Something.Standard,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Bones,
			//		subType = (byte) Something.Bones,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Liz,
			//		subType = (byte) Something.Liz,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Poke,
			//		subType = (byte) Something.Poke,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Lich,
			//		subType = (byte) Something.Lich,
			//	},
			//});
		}
	}
}
