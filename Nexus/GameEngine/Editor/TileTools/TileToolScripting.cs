using Nexus.Gameplay;
using Nexus.Objects;
using static Nexus.Objects.TrackDot;

namespace Nexus.GameEngine {

	public class TileToolScripting : TileTool {

		public TileToolScripting() : base() {

			this.slotGroup = (byte)SlotGroup.Scripting;

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TrackDot,
					subType = (byte) TrackDotSubType.Blue,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TrackDot,
					subType = (byte) TrackDotSubType.Green,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.TrackDot,
					subType = (byte) TrackDotSubType.Red,
				},
			});
		}
	}
}
