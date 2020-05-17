using Nexus.Gameplay;
using static Nexus.Objects.TrackDot;

namespace Nexus.GameEngine {

	public class TileToolScripting : TileTool {

		public TileToolScripting() : base() {

			this.slotGroup = (byte)SlotGroup.Scripting;

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TrackDot,
					subType = (byte) TrackDotSubType.Blue,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TrackDot,
					subType = (byte) TrackDotSubType.Green,
					layerEnum = LayerEnum.fg,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TrackDot,
					subType = (byte) TrackDotSubType.Red,
					layerEnum = LayerEnum.fg,
				},
			});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		objectId = (byte) TileEnum.Cluster,
			//		subType = (byte) Something.Cluster,
			//		layerEnum = LayerEnum.obj,
			//	},
			//	new EditorPlaceholder() {
			//		objectId = (byte) TileEnum.Cluster,
			//		subType = (byte) Something.Char,
			//		layerEnum = LayerEnum.obj,
			//	},
			//	new EditorPlaceholder() {
			//		objectId = (byte) TileEnum.Cluster,
			//		subType = (byte) Something.Screen,
			//		layerEnum = LayerEnum.obj,
			//	},
			//});
		}
	}
}
