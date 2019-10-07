using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	/*
	 * .tiles: bool[6]:
	 *		[0] Bool (Is Tile)				T: Is Tile, F: Is Object (has an Object ID)
	 *		[1] Bool (Char Only)			T: Only Char Interacts
	 *		[2] Bool (Collision)			T: Has Collision
	 *		[3] Bool (Full/Side)			T: Full Collision (all sides & solidity), F: Ledge Upward
	 *		[4] Bool (CollisionTest)		T: Has special collision TEST rules. If collision occurs, run CollisionTest() first.
	 *		[5] Bool (CollisionEffect)		T: Has special collision EFFECT rules. If collision occurs, run CollisionEffect().
	 *		
	 *		// Optional bool[10] for slopes:
	 *		[6] Bool (Slope)				T: Is Slope Piece
	 *		[7] Bool (Slope Angle)			T: Tall Slope, F: Gentle Slope
	 *		[8] Bool (Slope Horizontal)		T: Slope Faces Left, F: Slope Faces Right
	 *		[9] Bool (Slope Vertical)		T: Slope is Upside Down, F: Slope is Upright (ground)
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

		// Width and Height of the Tilemap:
		public int width { get; protected set; }
		public int height { get; protected set; }
		public ushort xCount { get; protected set; }
		public ushort yCount { get; protected set; }

		public TilemapBool(ushort xCount, ushort yCount) {

			// Sizing
			this.xCount = xCount;
			this.yCount = yCount;
			this.width = xCount * (ushort) TilemapEnum.TileWidth;
			this.height = yCount * (ushort) TilemapEnum.TileWidth;

			// Data
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

		private bool[] BuildTileData(bool isTile, bool collides, bool fullSolid, bool charOnly, bool colTest, bool colEffect, bool isSlope = false, bool slopeAngle = false, bool slopeHor = false, bool slopeVert = false ) {

			bool[] tileData = new bool[isSlope ? 10 : 6];

			tileData[0] = true;
			tileData[1] = charOnly;
			tileData[2] = collides;
			tileData[3] = fullSolid;
			tileData[4] = colTest;
			tileData[5] = colEffect;

			// Slopes have additional bools:
			if(isSlope) {
				tileData[6] = true;
				tileData[7] = slopeAngle;
				tileData[8] = slopeHor;
				tileData[9] = slopeVert;
			}

			return tileData;
		}

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void AddClassTile(ushort gridX, ushort gridY, byte classId, byte subTypeId, bool collides, bool fullSolid, bool charOnly = false, bool colTest = false, bool colEffect = false ) {
			this.boolData[gridY, gridX] = true;

			// The Tile Type is guaranteed to be TRUE (since it's a texture tile).
			bool[] tileData = this.BuildTileData(true, collides, fullSolid, charOnly, colTest, colEffect );

			// Add Content to Dictionaries
			uint gridId = this.GetGridID(gridX, gridY);

			this.tiles[gridId] = tileData;
			this.ids[gridId] = new ushort[] { classId, subTypeId };
		}

		// Slope Tiles are also Class Tiles
		public void AddSlopeTile(ushort gridX, ushort gridY, byte classId, byte subTypeId, bool slopeAngle, bool slopeHor, bool slopeVert ) {
			this.boolData[gridY, gridX] = true;

			// The Tile Type is guaranteed to be TRUE (since it's a class tile)
			// Slopes are also guaranteed to: Collide, Not Be Fully Solid, Not Be Character Only, Not Have A Collision Test, Not Have Collision Effects, Be A Slope
			bool[] tileData = this.BuildTileData(true, true, false, false, false, false, true, slopeAngle, slopeHor, slopeVert);

			// Add Content to Dictionaries
			uint gridId = this.GetGridID(gridX, gridY);

			this.tiles[gridId] = tileData;
			this.ids[gridId] = new ushort[] { classId, subTypeId };
		}

		// For performance reasons, it is up to the user to avoid exceeding the grid's X,Y limits.
		public void AddObjectTile( ushort gridX, ushort gridY, ushort objectId, bool collides, bool fullSolid, bool charOnly = false, bool colTest = false, bool colEffect = false ) {
			this.boolData[gridY, gridX] = true;

			// The Tile Type is guaranteed to be FALSE (since it's an Object ID)
			bool[] tileData = this.BuildTileData(false, collides, fullSolid, charOnly, colTest, colEffect);
			
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
