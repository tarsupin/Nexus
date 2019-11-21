using Nexus.Gameplay;
using static Nexus.Objects.DecorCave;
using static Nexus.Objects.DecorItems;
using static Nexus.Objects.DecorPet;
using static Nexus.Objects.DecorVeg;
using static Nexus.Objects.PromptIcon;

namespace Nexus.GameEngine {

	public class TileToolPrompt : TileTool {

		public TileToolPrompt() : base() {

			this.DefaultIcon = "Decor/Grass2";

			// TODO LOW PRIORITY: Add this whole section (there's a section with facing options)

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Left,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Right,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Up,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Down,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.A,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.B,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.X,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Y,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.L1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.R1,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.N1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.N2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.N3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.N4,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.N5,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Run,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Jump,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Cast,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Burst,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Fist,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Hand,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Chat,
				},
			});

			// TODO LOW PRIORITY: Fix this section
			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Arrow,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Arrow,
			//		face: DirRotate.UpRight,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Arrow,
			//		face: DirRotate.Right,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Arrow,
			//		face: DirRotate.DownRight,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Arrow,
			//		face: DirRotate.Down,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Arrow,
			//		face: DirRotate.DownLeft,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Arrow,
			//		face: DirRotate.Left,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Arrow,
			//		face: DirRotate.UpLeft,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Finger,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Finger,
			//		face: DirRotate.UpRight,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Finger,
			//		face: DirRotate.Right,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Finger,
			//		face: DirRotate.DownRight,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Finger,
			//		face: DirRotate.Down,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Finger,
			//		face: DirRotate.DownLeft,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Finger,
			//		face: DirRotate.Left,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PromptArrow,
			//		subType = (byte) So.Finger,
			//		face: DirRotate.UpLeft,
			//	},
			//});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorCave,
					subType = (byte) CaveSubType.Bulge1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorCave,
					subType = (byte) CaveSubType.Bulge2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorCave,
					subType = (byte) CaveSubType.Bulge3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorCave,
					subType = (byte) CaveSubType.Bulge4,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorCave,
					subType = (byte) CaveSubType.Bulge5,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorCave,
					subType = (byte) CaveSubType.Bulge6,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorCave,
					subType = (byte) CaveSubType.Rock1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorCave,
					subType = (byte) CaveSubType.Rock2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorCave,
					subType = (byte) CaveSubType.Slime,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorCave,
					subType = (byte) CaveSubType.Top1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorCave,
					subType = (byte) CaveSubType.Top2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorCave,
					subType = (byte) CaveSubType.Top3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorCave,
					subType = (byte) CaveSubType.Top4,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorItems,
					subType = (byte) CrysSubType.Gem1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorItems,
					subType = (byte) CrysSubType.Gem2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorItems,
					subType = (byte) CrysSubType.Gem3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorItems,
					subType = (byte) CrysSubType.Gem4,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Grass1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Grass2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Plant1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Plant2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Plant3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Plant4,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Vine1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Vine2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Vine3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Fung1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Fung2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Fung3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Fung4,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Fung5,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Tree1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Tree2,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.BunnyLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.BunnyRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.CowLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.CowRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.DeerLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.DeerRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.DogLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.DogRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.DuckLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.DuckRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.FoxLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.FoxRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.GoatLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.GoatRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.HenLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.HenRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.PigLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.PigRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.RaccoonLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.RaccoonRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.SheepLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.SheepRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.SquirrelLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorPet,
					subType = (byte) PetSubType.SquirrelRight,
				},
			});
		}
	}
}
