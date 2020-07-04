using Nexus.Gameplay;
using static Nexus.Objects.CannonDiag;
using static Nexus.Objects.CannonHor;
using static Nexus.Objects.CannonVert;
using static Nexus.Objects.Placer;
using static Nexus.Objects.SpringFixed;
using static Nexus.Objects.SpringSide;

namespace Nexus.GameEngine {

	public class TileToolFixtures : TileTool {

		public TileToolFixtures() : base() {

			this.slotGroup = (byte)SlotGroup.Fixtures;

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
					tileId = (byte) TileEnum.ButtonFixedBRUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ButtonFixedGYUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ButtonTimedBRUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ButtonTimedGYUp,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.SpringFixed,
					subType = (byte) SpringFixedSubType.Up,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.SpringSide,
					subType = (byte) SpringSideSubType.Right,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.SpringFixed,
					subType = (byte) SpringFixedSubType.Rev,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.SpringSide,
					subType = (byte) SpringSideSubType.Left,
				},
			});
		}
	}
}
