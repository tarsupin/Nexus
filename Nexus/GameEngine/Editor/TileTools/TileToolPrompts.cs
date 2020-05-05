using Nexus.Gameplay;
using static Nexus.Objects.PromptArrow;
using static Nexus.Objects.PromptIcon;

namespace Nexus.GameEngine {

	public class TileToolPrompts : TileTool {

		public TileToolPrompts() : base() {

			this.slotGroup = (byte)SlotGroup.Prompts;

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

			// TODO LOW PRIORITY: Fix all the facing options with the arrows.
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					//face: DirRotate.UpRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					//face: DirRotate.Right,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					//face: DirRotate.DownRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					//face: DirRotate.Down,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					//face: DirRotate.DownLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					//face: DirRotate.Left,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					//face: DirRotate.UpLeft,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					//face: DirRotate.UpRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					//face: DirRotate.Right,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					//face: DirRotate.DownRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					//face: DirRotate.Down,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					//face: DirRotate.DownLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					//face: DirRotate.Left,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					//face: DirRotate.UpLeft,
				},
			});
		}
	}
}
