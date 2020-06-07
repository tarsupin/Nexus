using Nexus.Gameplay;
using static Nexus.Objects.Conveyor;

namespace Nexus.GameEngine {

	public class TileToolPlatforms : TileTool {

		public TileToolPlatforms() : base() {

			this.slotGroup = (byte)SlotGroup.Platforms;

			// Fixed Platforms
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedUp,
					subType = (byte) HorizontalSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedUp,
					subType = (byte) HorizontalSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedUp,
					subType = (byte) HorizontalSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedUp,
					subType = (byte) HorizontalSubTypes.H3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedRight,
					subType = (byte) HorizontalSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedRight,
					subType = (byte) HorizontalSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedRight,
					subType = (byte) HorizontalSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedRight,

					subType = (byte) HorizontalSubTypes.H3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedDown,
					subType = (byte) HorizontalSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedDown,
					subType = (byte) HorizontalSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedDown,
					subType = (byte) HorizontalSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedDown,
					subType = (byte) HorizontalSubTypes.H3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedLeft,
					subType = (byte) HorizontalSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedLeft,
					subType = (byte) HorizontalSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedLeft,
					subType = (byte) HorizontalSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformFixedLeft,
					subType = (byte) HorizontalSubTypes.H3,
				},
			});
			
			// Rock Platform
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockUp,
					subType = (byte) HorizontalSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockUp,
					subType = (byte) HorizontalSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockUp,
					subType = (byte) HorizontalSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockUp,
					subType = (byte) HorizontalSubTypes.H3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockRight,
					subType = (byte) HorizontalSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockRight,
					subType = (byte) HorizontalSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockRight,
					subType = (byte) HorizontalSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockRight,

					subType = (byte) HorizontalSubTypes.H3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockDown,
					subType = (byte) HorizontalSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockDown,
					subType = (byte) HorizontalSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockDown,
					subType = (byte) HorizontalSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockDown,
					subType = (byte) HorizontalSubTypes.H3,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockLeft,
					subType = (byte) HorizontalSubTypes.S,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockLeft,
					subType = (byte) HorizontalSubTypes.H1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockLeft,
					subType = (byte) HorizontalSubTypes.H2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.PlatformRockLeft,
					subType = (byte) HorizontalSubTypes.H3,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformDelay,
					subType = (byte) HorizontalSubTypes.S,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformDelay,
					subType = (byte) HorizontalSubTypes.H1,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformDelay,
					subType = (byte) HorizontalSubTypes.H2,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformDelay,
					subType = (byte) HorizontalSubTypes.H3,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformDip,
					subType = (byte) HorizontalSubTypes.S,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformDip,
					subType = (byte) HorizontalSubTypes.H1,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformDip,
					subType = (byte) HorizontalSubTypes.H2,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformDip,
					subType = (byte) HorizontalSubTypes.H3,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformFall,
					subType = (byte) HorizontalSubTypes.S,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformFall,
					subType = (byte) HorizontalSubTypes.H1,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformFall,
					subType = (byte) HorizontalSubTypes.H2,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformFall,
					subType = (byte) HorizontalSubTypes.H3,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformMove,
					subType = (byte) HorizontalSubTypes.S,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformMove,
					subType = (byte) HorizontalSubTypes.H1,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformMove,
					subType = (byte) HorizontalSubTypes.H2,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.PlatformMove,
					subType = (byte) HorizontalSubTypes.H3,
					layerEnum = LayerEnum.obj,
				},
			});
			
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Conveyor,
					subType = (byte) ConveyorSubType.Left,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Conveyor,
					subType = (byte) ConveyorSubType.Right,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Conveyor,
					subType = (byte) ConveyorSubType.SlowLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Conveyor,
					subType = (byte) ConveyorSubType.SlowRight,
				},
			});
		}
	}
}
