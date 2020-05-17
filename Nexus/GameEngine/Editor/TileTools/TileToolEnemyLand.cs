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
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Goo,
					subType = (byte) GooSubType.Blue,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Goo,
					subType = (byte) GooSubType.Orange,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Bones,
					subType = (byte) BonesSubType.Bones,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Snek,
					subType = (byte) SnekSubType.Snek,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Wurm,
					subType = (byte) SnekSubType.Wurm,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Octo,
					subType = (byte) OctoSubType.Octo,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Liz,
					subType = (byte) LizSubType.Liz,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Snail,
					subType = (byte) SnailSubType.Snail,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Bug,
					subType = (byte) BugSubType.Bug,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Turtle,
					subType = (byte) TurtleSubType.Red,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Moosh,
					subType = (byte) MooshSubType.Brown,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Moosh,
					subType = (byte) MooshSubType.Purple,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Moosh,
					subType = (byte) MooshSubType.White,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Shroom,
					subType = (byte) ShroomSubType.Red,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Shroom,
					subType = (byte) ShroomSubType.Purple,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Shroom,
					subType = (byte) ShroomSubType.Black,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Boom,
					subType = (byte) BoomSubType.Boom,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Poke,
					subType = (byte) PokeSubType.Poke,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Lich,
					subType = (byte) LichSubType.Lich,
					layerEnum = LayerEnum.obj,
				},
			});
		}
	}
}
