using Nexus.Gameplay;
using static Nexus.Objects.Box;
using static Nexus.Objects.ExclaimBlock;
using static Nexus.Objects.Leaf;
using static Nexus.Objects.PuffBlock;
using static Nexus.Objects.Spike;

namespace Nexus.GameEngine {

	public class WorldTileToolBlocks : WorldTileTool {

		public WorldTileToolBlocks() : base() {

			this.slotGroup = (byte)SlotGroup.Blocks;
			
			// Bricks
			this.placeholders.Add(new WorldEditorPlaceholder[] {
				new WorldEditorPlaceholder() {
					tileId = (byte)TileEnum.Brick,
					subType = 0,
				},
				new WorldEditorPlaceholder() {
					tileId = (byte)TileEnum.Brick,
					subType = 1,
				},
			});

			// Boxes
			this.placeholders.Add(new WorldEditorPlaceholder[] {
				new WorldEditorPlaceholder() {
					tileId = (byte) TileEnum.Box,
					subType = (byte) BoxSubType.Brown,
				},
				new WorldEditorPlaceholder() {
					tileId = (byte) TileEnum.Box,
					subType = (byte) BoxSubType.Gray,
				},
			});

			// Leaf
			this.placeholders.Add(new WorldEditorPlaceholder[] {
				new WorldEditorPlaceholder() {
					tileId = (byte) TileEnum.Leaf,
					subType = (byte) LeafSubType.Reform,
				},
				new WorldEditorPlaceholder() {
					tileId = (byte) TileEnum.Leaf,
					subType = (byte) LeafSubType.Basic,
				},
			});

			// Spike
			this.placeholders.Add(new WorldEditorPlaceholder[] {
				new WorldEditorPlaceholder() {
					tileId = (byte) TileEnum.Spike,
					subType = (byte) SpikeSubType.Basic,
				},
				new WorldEditorPlaceholder() {
					tileId = (byte) TileEnum.Spike,
					subType = (byte) SpikeSubType.Lethal,
				},
			});

			// Exclaim Block
			this.placeholders.Add(new WorldEditorPlaceholder[] {
				new WorldEditorPlaceholder() {
					tileId = (byte) TileEnum.ExclaimBlock,
					subType = (byte) ExclaimBlockSubType.Active,
				},
				new WorldEditorPlaceholder() {
					tileId = (byte) TileEnum.ExclaimBlock,
					subType = (byte) ExclaimBlockSubType.Inactive,
				},
				new WorldEditorPlaceholder() {
					tileId = (byte) TileEnum.ExclaimBlock,
					subType = (byte) ExclaimBlockSubType.Transparent,
				},
			});

			// Puff Block
			this.placeholders.Add(new WorldEditorPlaceholder[] {
				new WorldEditorPlaceholder() {
					tileId = (byte) TileEnum.PuffBlock,
					subType = (byte) PuffBlockSubType.Up,
				},
				new WorldEditorPlaceholder() {
					tileId = (byte) TileEnum.PuffBlock,
					subType = (byte) PuffBlockSubType.Right,
				},
				new WorldEditorPlaceholder() {
					tileId = (byte) TileEnum.PuffBlock,
					subType = (byte) PuffBlockSubType.Down,
				},
				new WorldEditorPlaceholder() {
					tileId = (byte) TileEnum.PuffBlock,
					subType = (byte) PuffBlockSubType.Left,
				},
			});

			// Lock
			this.placeholders.Add(new WorldEditorPlaceholder[] {
				new WorldEditorPlaceholder() {
					tileId = (byte) TileEnum.Lock,
					subType = 0,
				},
			});
		}
	}
}
