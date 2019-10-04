using System.Collections.Generic;

namespace Nexus.GameEngine {

	/*
	 * .tiles: bool[4]:
	 *		[0] Bool (Is Tile)				T: Is Tile, F: Is Object (has an Object ID)
	 *		[1] Bool (Char Only)			T: Only Char Interacts
	 *		[2] Bool (Collision)			T: Has Collision
	 *		[3] Bool (Full/Ledge)			T: Full Collision (all sides & solidity), F: Ledge Upward
	 *		[4] Bool (Special Collision)	T: Has special collision rules; interact with class methods.
	 *	
	 *	.ids: ushort[2]
	 *		[0] Object ID or Tile Class ID		// Points to Object ID (for GameObjects) or Tile Class ID (for ClassGameObjects) 
	 *		[1] Tile SubType ID					// Only applies to Tile IDs. Provides SubType for that Tile.
	 */

	public class TilemapBool {

		// The Bool Array: Identifies if a grid square DOES or DOES NOT have tile data inside of it.
		public bool[,] boolData { get; protected set; }

		// Tile Data: Dictionaries of data that matches to the gridID (gridY*xCount + gridX)
		public Dictionary<uint, bool[]> tiles;
		public Dictionary<uint, ushort[]> ids;

		// The Maximum Width and Height of the Tilemap:
		public ushort xCount { get; protected set; }
		public ushort yCount { get; protected set; }

		public TilemapBool(ushort xCount, ushort yCount) {
			this.xCount = xCount;
			this.yCount = yCount;

			this.boolData = new bool[yCount, xCount];

			this.tiles = new Dictionary<uint, bool[]>();
			this.ids = new Dictionary<uint, ushort[]>();
		}

		public bool IsTilePresent(ushort gridX, ushort gridY) {
			return this.boolData[gridY, gridX];
		}

		//public byte Detect4Grid( ushort gridX, ushort gridY ) {
		//	Bitwise.Set4Bits()
		//}

		private bool[] BuildTileData(bool isTile, bool collides, bool fullSolid, bool charOnly, bool specialCol ) {

			bool[] tileData = new bool[5];
			tileData[0] = true;
			tileData[1] = charOnly;
			tileData[2] = collides;
			tileData[3] = fullSolid;
			tileData[4] = specialCol;

			return tileData;
		}

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void AddClassTile(ushort gridX, ushort gridY, byte classId, byte subTypeId, bool collides, bool fullSolid, bool charOnly = false, bool specialCol = false ) {
			this.boolData[gridY, gridX] = true;

			// The Tile Type is guaranteed to be TRUE (since it's a texture tile).
			bool[] tileData = this.BuildTileData(true, collides, fullSolid, charOnly, specialCol );

			// Add Content to Dictionaries
			uint gridId = this.GetGridID(gridX, gridY);

			this.tiles[gridId] = tileData;
			this.ids[gridId] = new ushort[] { classId, subTypeId };
		}

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void AddObjectTile( ushort gridX, ushort gridY, ushort objectId, bool collides, bool fullSolid, bool charOnly = false, bool specialCol = false ) {
			this.boolData[gridY, gridX] = true;

			// The Tile Type is guaranteed to be FALSE (since it's an Object ID)
			bool[] tileData = this.BuildTileData(false, collides, fullSolid, charOnly, specialCol);
			
			// Add Content to Dictionaries
			uint gridId = this.GetGridID(gridX, gridY);

			this.tiles[gridId] = tileData;
			this.ids[gridId] = new ushort[] { objectId };
		}

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void RemoveTile( ushort gridX, ushort gridY ) {
			this.boolData[gridY, gridX] = false;

			// Remove any values in the .tiles Dictionary
			this.tiles.Remove(this.GetGridID(gridX, gridY));
		}

		public uint GetGridID( ushort gridX, ushort gridY ) {
			return (uint) gridY * this.xCount + gridX;
		}
	}
}
