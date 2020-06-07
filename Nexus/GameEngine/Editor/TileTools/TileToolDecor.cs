using Nexus.Gameplay;
using static Nexus.Objects.DecorCave;
using static Nexus.Objects.DecorDesert;
using static Nexus.Objects.DecorItems;
using static Nexus.Objects.DecorPet;
using static Nexus.Objects.DecorSnow;
using static Nexus.Objects.DecorVeg;

namespace Nexus.GameEngine {

	public class TileToolDecor : TileTool {

		public TileToolDecor() : base() {

			this.slotGroup = (byte)SlotGroup.Decor;

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
					subType = (byte) VegSubType.Flower1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Flower2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Flower3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Flower4,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Flower5,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Tree1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Tree2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Tree3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Tree4,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Tree5,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorVeg,
					subType = (byte) VegSubType.Tree6,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
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
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorDesert,
					subType = (byte) DesertSubType.Desert1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorDesert,
					subType = (byte) DesertSubType.Desert2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorDesert,
					subType = (byte) DesertSubType.Desert3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorDesert,
					subType = (byte) DesertSubType.Desert4,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorDesert,
					subType = (byte) DesertSubType.Bones1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorDesert,
					subType = (byte) DesertSubType.Bones2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorDesert,
					subType = (byte) DesertSubType.Bones3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorDesert,
					subType = (byte) DesertSubType.Cacti1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorDesert,
					subType = (byte) DesertSubType.Cacti2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorDesert,
					subType = (byte) DesertSubType.Cacti3,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
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
			});

			this.placeholders.Add(new EditorPlaceholder[] {
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
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorCave,
					subType = (byte) CaveSubType.Slime,
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
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorItems,
					subType = (byte) CrysSubType.Sign,
				},
			});
			
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorSnow,
					subType = (byte) SnowSubType.Bank1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorSnow,
					subType = (byte) SnowSubType.Bank2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorSnow,
					subType = (byte) SnowSubType.Bush1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DecorSnow,
					subType = (byte) SnowSubType.Log1,
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
