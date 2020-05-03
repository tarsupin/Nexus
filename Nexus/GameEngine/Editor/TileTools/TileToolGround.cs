using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class TileToolGround : TileTool {

		public TileToolGround() : base() {

			this.slotGroup = (byte)SlotGroup.Ground;

			// Grass
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.H3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.V1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.V2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.V3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.FUL,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.FU,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.FUR,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.FL,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.FC,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.FR,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.FBL,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.FB,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundGrass,
					subType = (byte) GroundSubTypes.FBR,
				},
			});

			// Stone
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundStone,
					subType = (byte) GroundSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.H3,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.V1,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.V2,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.V3,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.FUL,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.FU,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.FUR,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.FL,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.FC,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.FR,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.FBL,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.FB,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundStone,
					subType = (byte)GroundSubTypes.FBR,
				},
			});

			// Mud
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundMud,
					subType = (byte) GroundSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.H3,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.V1,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.V2,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.V3,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.FUL,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.FU,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.FUR,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.FL,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.FC,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.FR,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.FBL,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.FB,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundMud,
					subType = (byte)GroundSubTypes.FBR,
				},
			});

			// Snow
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundSnow,
					subType = (byte) GroundSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.H3,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.V1,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.V2,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.V3,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.FUL,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.FU,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.FUR,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.FL,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.FC,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.FR,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.FBL,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.FB,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.GroundSnow,
					subType = (byte)GroundSubTypes.FBR,
				},
			});

			// Grass Ledge
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrass,
					subType = (byte) GroundSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrass,
					subType = (byte) GroundSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrass,
					subType = (byte) GroundSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrass,
					subType = (byte) GroundSubTypes.H3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrass,
					subType = (byte) GroundSubTypes.V1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrassDecor,
					subType = (byte) GroundSubTypes.V2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrassDecor,
					subType = (byte) GroundSubTypes.V3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrass,
					subType = (byte) GroundSubTypes.FUL,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrass,
					subType = (byte) GroundSubTypes.FU,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrass,
					subType = (byte) GroundSubTypes.FUR,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrassDecor,
					subType = (byte) GroundSubTypes.FL,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrassDecor,
					subType = (byte) GroundSubTypes.FC,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrassDecor,
					subType = (byte) GroundSubTypes.FR,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrassDecor,
					subType = (byte) GroundSubTypes.FBL,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrassDecor,
					subType = (byte) GroundSubTypes.FB,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.LedgeGrassDecor,
					subType = (byte) GroundSubTypes.FBR,
				},
			});

			// Cloud
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.GroundCloud,
					subType = (byte) GroundSubTypes.S,
				},
			});

			// Log
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Log,
					subType = (byte) GroundSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.Log,
					subType = (byte)GroundSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.Log,
					subType = (byte)GroundSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.Log,
					subType = (byte)GroundSubTypes.H3,
				},
			});

			// Slab (Gray, Brown)
			// TODO LOW PRIORITY: Need correct versions. Not just subType = 0;
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Log,
					subType = (byte) GroundSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Log,
					subType = (byte) GroundSubTypes.S,
				},
			});
		}
	}
}
