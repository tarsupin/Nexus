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

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Goo,
					subType = (byte) GooSubType.Green,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Goo,
					subType = (byte) GooSubType.Blue,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Goo,
					subType = (byte) GooSubType.Orange,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Bones,
					subType = (byte) BonesSubType.Bones,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Snek,
					subType = (byte) SnekSubType.Snek,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Wurm,
					subType = (byte) SnekSubType.Wurm,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Octo,
					subType = (byte) OctoSubType.Octo,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Liz,
					subType = (byte) LizSubType.Liz,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Snail,
					subType = (byte) SnailSubType.Snail,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Bug,
					subType = (byte) BugSubType.Bug,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Turtle,
					subType = (byte) TurtleSubType.Red,
				},
			});

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

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Boom,
					subType = (byte) BoomSubType.Boom,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Poke,
					subType = (byte) PokeSubType.Poke,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Lich,
					subType = (byte) LichSubType.Lich,
				},
			});
		}
	}
}
