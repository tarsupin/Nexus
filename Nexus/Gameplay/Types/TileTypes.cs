
namespace Nexus.Gameplay {

	public enum TilemapEnum : ushort {

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
		MaxTileMult = 40000,			// The maximum size of (Tiles Wide x Tiles High)

		// Sectors
		SectorWidth = 6,
		SectorHeight = 6,
		SectorVertIncrement = 100,			// The step increment that sectors count vertically (e.g. 0, 100, 200, ...)

		SectorXPixels = 288,
		SectorYPixels = 288,
	}

}
