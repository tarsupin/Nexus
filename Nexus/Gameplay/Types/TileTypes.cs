
namespace Nexus.Gameplay {

	public enum TilemapEnum : short {

		// Grid Sizes
		TileWidth = 48,
		TileHeight = 48,
		
		// Half Width - Used to determine midpoints.
		HalfWidth = 24,
		HalfHeight = 24,

		// World Gaps - the amount of grid tiles above and below the world lines.
		WorldGapLeft = 1,
		WorldGapRight = 1,
		WorldGapUp = 3,
		WorldGapDown = 2,

		// Tilemap Limits
		MaxTilesWide = 800,
		MaxTilesHigh = 800,
	}

}
