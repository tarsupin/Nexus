using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WTAutoTiles : WorldTileTool {

		public WTAutoTiles() : base() {

			this.slotGroup = (byte)WorldSlotGroup.AutoTiles;
			
			// Bases
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					auto = true,
					tBase = 1,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 2,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 3,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 4,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 5,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 6,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 7,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 8,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 9,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 10,
				},
			});
		}
	}
}
