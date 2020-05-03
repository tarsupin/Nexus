using Nexus.Gameplay;
using static Nexus.Objects.Box;
using static Nexus.Objects.Spike;

namespace Nexus.GameEngine {

	public class TileToolBlocks : TileTool {

		public TileToolBlocks() : base() {

			this.slotGroup = (byte)SlotGroup.Blocks;
			
			// Bricks
			// TODO LOW PRIORITY: Need "bricks" here. Brown and Gray options. Not just subType = 0;
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.Brick,
					subType = 0,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.Brick,
					subType = 1,
				},
			});

			// Boxes
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Box,
					subType = (byte) BoxSubType.Brown,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Box,
					subType = (byte) BoxSubType.Gray,
				},
			});

			// Leaf
			// TODO LOW PRIORITY: Need secondary leaf option. There's "basic" and "reform". Not just subType = 0;
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Leaf,
					subType = 0,
				},
			});

			// Spike
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Spike,
					subType = (byte) SpikeSubType.Basic,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Spike,
					subType = (byte) SpikeSubType.Lethal,
				},
			});

			// Exclaim Block
			// TODO LOW PRIORITY: Need correct versions. Not just subType = 0;
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ExclaimBlock,
					subType = (byte)GroundSubTypes.S,
				},
			});

			// Puff Block
			// TODO LOW PRIORITY: Need correct FACING(?) versions. Not just subType = 0;
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PuffBlock,
					subType = (byte)GroundSubTypes.S,
				},
			});
		}
	}
}
