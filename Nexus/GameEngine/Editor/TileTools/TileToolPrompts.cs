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
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.N2,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.N3,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.N4,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.N5,
					layerEnum = LayerEnum.fg,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Run,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Jump,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Cast,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Burst,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Fist,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Hand,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Chat,
					layerEnum = LayerEnum.fg,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Left,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Right,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Up,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Down,
					layerEnum = LayerEnum.fg,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.A,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.B,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.X,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.Y,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.L1,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptIcon,
					subType = (byte) IconSubType.R1,
					layerEnum = LayerEnum.fg,
				},
			});

			// TODO LOW PRIORITY: Fix all the facing options with the arrows.
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					//face: DirRotate.UpRight,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					//face: DirRotate.Right,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					//face: DirRotate.DownRight,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					//face: DirRotate.Down,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					//face: DirRotate.DownLeft,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					//face: DirRotate.Left,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Arrow,
					//face: DirRotate.UpLeft,
					layerEnum = LayerEnum.fg,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					//face: DirRotate.UpRight,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					//face: DirRotate.Right,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					//face: DirRotate.DownRight,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					//face: DirRotate.Down,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					//face: DirRotate.DownLeft,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					//face: DirRotate.Left,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PromptArrow,
					subType = (byte) ArrowSubType.Finger,
					//face: DirRotate.UpLeft,
					layerEnum = LayerEnum.fg,
				},
			});
		}
	}
}
