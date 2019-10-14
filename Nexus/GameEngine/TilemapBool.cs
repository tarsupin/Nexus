using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	/*
	 *	.ids: ushort[2]
	 *		[0] Object ID or Tile Class ID		// Points to Object ID (for GameObjects) or Tile Class ID (for ClassGameObjects) 
	 *		[1] Tile SubType ID					// Only applies to Tile IDs. Provides SubType for that Tile.
	 */

	// .tiles: bool[10]
	public enum TMBTiles : byte {
		IsTile = 0,                     // [0] Bool (Is Tile)				T: Is Tile, F: Is Object (has an Object ID)
		HasCollision = 1,               // [1] Bool (Collision)				T: Has Collision
		CharOnly = 2,					// [2] Bool (Char Only)				T: Only Char Interacts
		SpecialCollisionTest = 3,       // [3] Bool (CollisionTest)			T: Has special collision TEST rules. If collision occurs, run CollisionTest() first.

		// TODO CLEANUP: I'm 95% sure I can remove 'SpecialCollisionEffect' - SpecialCollisionTest should handle it's purpose, and does so before affecting .touch
		SpecialCollisionEffect = 4,     // [4] Bool (CollisionEffect)		T: Has special collision EFFECT rules. If collision occurs, run CollisionEffect().
		FullCollision = 5,              // [5] Bool (Full/Side)				T: Full Collision (all sides & solidity), F: Ledge Upward

		// If FullCollision = 0, then the result is either a Platform or a Slope:
		SlopeOrPlatform = 6,			// [6] Bool (Slope/Platform)		T: Is Slope, F: Is Platform
		HorizontalFacing = 7,			// [7] Bool (Facing Horizontal)		T: Platform/Slope faces Left, F: Platform/Slope faces Right
		VerticalFacing = 8,				// [8] Bool (Facing Vertical)		T: Platform/Slope is Upright, F: Platform/Slope is Upside Down

		// If dealing with a Slope, apply SlopeAngle. If dealing with a Platform, apply PlatformIsVert.
		SlopeAngle = 9,                 // [9] Bool (Slope Angle)			T: Tall Slope, F: Gentle Slope
		PlatformIsVert = 9,				// [9] Bool (Platform is Vertical)	T: Platform is Facing Up or Down, F: Platform is Facing Left or Right
	}

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

		private bool[] BuildTileData(bool isTile, bool collides, bool fullSolid, bool charOnly, bool colTest, bool colEffect, bool slopeOrPlatform = false, bool horFace = false, bool vertFace = false, bool isVert = false) {

			bool[] tileData = new bool[10];

			tileData[(byte) TMBTiles.IsTile] = true;

			// If the Tile doesn't collide (like Decor), then no need to build any further:
			if(collides) {

				tileData[(byte)TMBTiles.HasCollision] = collides;
				tileData[(byte)TMBTiles.CharOnly] = charOnly;
				tileData[(byte)TMBTiles.FullCollision] = fullSolid;
				tileData[(byte)TMBTiles.SpecialCollisionTest] = colTest;
				tileData[(byte)TMBTiles.SpecialCollisionEffect] = colEffect;

				// If the tile does not have a full-block collision, but also doesn't have a special collision test, then the result is either a Platform or a Slope:
				if(!fullSolid && !colTest) {

					// Platforms and Slopes have additional bool flags:
					tileData[(byte)TMBTiles.SlopeOrPlatform] = slopeOrPlatform;
					tileData[(byte)TMBTiles.HorizontalFacing] = horFace;
					tileData[(byte)TMBTiles.VerticalFacing] = vertFace;

					if(slopeOrPlatform == true) {
						tileData[(byte)TMBTiles.SlopeAngle] = isVert;
					} else {
						tileData[(byte)TMBTiles.PlatformIsVert] = isVert;
					}
				}
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

		// Platform Tiles are also Class Tiles
		public void AddPlatformTile(ushort gridX, ushort gridY, byte classId, byte subTypeId, bool isVert, bool faceUpOrLeft, bool colTest = false, bool colEffect = false) {
			this.boolData[gridY, gridX] = true;

			// The Tile Type is guaranteed to be TRUE (since it's a texture tile).
			bool[] tileData = this.BuildTileData(true, true, false, false, colTest, colEffect, false, faceUpOrLeft, faceUpOrLeft, isVert);

			// Add Content to Dictionaries
			uint gridId = this.GetGridID(gridX, gridY);

			this.tiles[gridId] = tileData;
			this.ids[gridId] = new ushort[] { classId, subTypeId };
		}

		// Slope Tiles are also Class Tiles
		public void AddSlopeTile(ushort gridX, ushort gridY, byte classId, byte subTypeId, bool slopeAngle, bool slopeHor, bool isVert ) {
			this.boolData[gridY, gridX] = true;

			// The Tile Type is guaranteed to be TRUE (since it's a class tile)
			// Slopes are also guaranteed to: Collide, Not Be Fully Solid, Not Be Character Only, Not Have A Collision Test, Not Have Collision Effects, Be A Slope
			bool[] tileData = this.BuildTileData(true, true, false, false, false, false, true, slopeAngle, slopeHor, isVert);

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

		// Grid Square Positions
		public static ushort GridX(int posX) { return (ushort) Math.Floor((double)(posX / (byte)TilemapEnum.TileWidth)); }
		public static ushort GridY(int posY) { return (ushort) Math.Floor((double)(posY / (byte)TilemapEnum.TileHeight)); }
	}
}
