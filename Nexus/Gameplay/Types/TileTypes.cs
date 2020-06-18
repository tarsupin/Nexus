
namespace Nexus.Gameplay {

	public enum TilemapEnum : short {

		// Grid Sizes
		TileWidth = 48,
		TileHeight = 48,
		
		// Half Width - Used to determine midpoints.
		HalfWidth = 24,
		HalfHeight = 24,

		// World Gaps - the amount of grid tiles above and below the world lines.
		GapLeft = 1,
		GapRight = 1,
		GapUp = 3,
		GapDown = 2,

		// World Gaps in Pixels
		GapLeftPixel = TilemapEnum.GapLeft * TilemapEnum.TileWidth,
		GapRightPixel = TilemapEnum.GapRight * TilemapEnum.TileWidth,
		GapUpPixel = TilemapEnum.GapUp * TilemapEnum.TileHeight,
		GapDownPixel = TilemapEnum.GapDown * TilemapEnum.TileHeight,

		// Min Sizes
		MinWidth = 30,		// 1440 / 48 = 30
		MinHeight = 19,		// 900 / 48 = 18.75

		// Tilemap Limits
		MaxTilesWide = 600,
		MaxTilesHigh = 600,
	}

}
