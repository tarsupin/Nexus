using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WTAutoTiles : WorldTileTool {

		public WTAutoTiles() : base() {

			this.slotGroup = (byte)WorldSlotGroup.AutoTiles;
			
			// Bases / Terrain
			this.placeholders.Add(new WEPlaceholder[] {
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
				new WEPlaceholder() {
					auto = true,
					tBase = 11,
				},
			});

			// Grass Terrain (Trees, Mountains, etc.)
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					auto = true,
					tBase = 1,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 1,
					tCat = 1,
					tLayer = 50,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 1,
					tCat = 2,
					tLayer = 50,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 1,
					tCat = 3,
					tLayer = 55,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 1,
					tCat = 4,
					tLayer = 50,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 1,
					tCat = 5,
					tLayer = 22,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 1,
					tCat = 6,
					tLayer = 55,
				},
			});

			// Desert Terrain (Trees, Mountains, etc.)
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					auto = true,
					tBase = 2,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 2,
					tCat = 2,
					tLayer = 50,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 2,
					tCat = 4,
					tLayer = 50,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 2,
					tCat = 5,
				},
			});

			// Snow Terrain (Trees, Mountains, etc.)
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					auto = true,
					tBase = 3,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 3,
					tCat = 1,
					tLayer = 50,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 3,
					tCat = 2,
					tLayer = 50,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 3,
					tCat = 3,
					tLayer = 55,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 3,
					tCat = 4,
					tLayer = 50,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = 3,
					tCat = 5,
					tLayer = 22,
				},
			});
		}
	}
}
