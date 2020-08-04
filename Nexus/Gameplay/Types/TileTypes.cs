
namespace Nexus.Gameplay {

	public enum TilemapEnum : short {

		// Grid Sizes
		TileWidth = 48,
		TileHeight = 48,
		
		// Half Width - Used to determine midpoints.
		HalfWidth = 24,
		HalfHeight = 24,

		// Barrier Gaps - the amount of grid tiles above and below the barrier lines.
		GapLeft = 1,
		GapRight = 1,
		GapUp = 3,
		GapDown = 2,

		// Barrier Gaps in Pixels
		GapLeftPixel = TilemapEnum.GapLeft * TilemapEnum.TileWidth,
		GapRightPixel = TilemapEnum.GapRight * TilemapEnum.TileWidth,
		GapUpPixel = TilemapEnum.GapUp * TilemapEnum.TileHeight,
		GapDownPixel = TilemapEnum.GapDown * TilemapEnum.TileHeight,

		// Min Sizes
		MinWidth = 30,      // ScreenSys.VirtualWidth / 48 = 30
		MinHeight = 19,     // ScreenSys.VirtualHeight / 48 = 18.75

		// Tilemap Limits
		MaxTilesWide = 600,
		MaxTilesHigh = 600,
		MaxTilesTotal = 16000,
	}

}
