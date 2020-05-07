﻿using Nexus.Gameplay;
using static Nexus.Objects.Box;
using static Nexus.Objects.ExclaimBlock;
using static Nexus.Objects.Leaf;
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
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Leaf,
					subType = (byte) LeafSubType.Reform,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Leaf,
					subType = (byte) LeafSubType.Basic,
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
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ExclaimBlock,
					subType = (byte) ExclaimBlockSubType.Active,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ExclaimBlock,
					subType = (byte) ExclaimBlockSubType.Inactive,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ExclaimBlock,
					subType = (byte) ExclaimBlockSubType.Transparent,
				},
			});

			// Puff Block
			// TODO LOW PRIORITY: Need correct FACING(?) versions. Not just subType = 0;
			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PuffBlock,
			//		subType = (byte)GroundSubTypes.S,
			//	},
			//});

			// Lock
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Lock,
					subType = 0,
				},
			});
		}
	}
}
