/*
 * SubTypes may overlap in many objects.
 */

namespace Nexus.Gameplay {

	public enum TilemapEnum : ushort {

		// Grid Spaces
		TileWidth = 48,
		TileHeight = 48,

		// Tilemap Limits
		MaxTilesWide = 600,
		MaxTilesHigh = 600,
		MaxTileMult = 40000,			// The maximum size of (Tiles Wide x Tiles High)

		// Sectors
		SectorWidth = 6,
		SectorHeight = 6,
		SectorVertIncrement = 100,			// The step increment that sectors count vertically (e.g. 0, 100, 200, ...)
	}
}
