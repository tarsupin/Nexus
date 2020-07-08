using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

// CollideTile handles collision detection between objects and tiles.

namespace Nexus.GameEngine {

	// --- Collisions Against Tiles --- //
	public class CollideTile {

		// Tests if object is completely within a tile square.
		public static bool IsWithinTile(GameObject dynamicObj, short gridX, short gridY) {
			return (dynamicObj.GridX == gridX && dynamicObj.GridX2 == gridX && dynamicObj.GridY == gridY && dynamicObj.GridY2 == gridY);
		}

		// Tests if object is within a tile with padded borders. Note: Many tests can't use this, since they're probably already touching the tile.
		public static bool IsWithinPaddedTile(GameObject dynamicObj, short gridX, short gridY, byte left = 0, byte right = 0, byte top = 0, byte bottom = 0) {

			byte w = (byte) TilemapEnum.TileWidth;
			byte h = (byte) TilemapEnum.TileHeight;
			int xStart = gridX * w;
			int yStart = gridY * h;

			return (dynamicObj.posX + dynamicObj.bounds.Left >= xStart - left && dynamicObj.posX + dynamicObj.bounds.Right <= xStart + w + right && dynamicObj.posY + dynamicObj.bounds.Top >= yStart - top && dynamicObj.posY + dynamicObj.bounds.Bottom <= yStart + h + bottom);
		}

		// Check if an exact coordinate has a Blocking Square (Ground, BlockTile, HorizontalWall, etc.)
		public static bool IsBlockingCoord(TilemapLevel tilemap, int posX, int posY, DirCardinal dir) {
			short gridX = (short)(posX / (byte)TilemapEnum.TileWidth);
			short gridY = (short)(posY / (byte)TilemapEnum.TileHeight);
			return CollideTile.IsBlockingSquare(tilemap, gridX, gridY, dir);
		}

		// Check if Grid Square is a Blocking Square (Ground, BlockTile, HorizontalWall, etc.)
		public static bool IsBlockingSquare(TilemapLevel tilemap, short gridX, short gridY, DirCardinal dir) {

			// Verify that a tile exists at the given location:
			byte[] tileData = tilemap.GetTileDataAtGrid(gridX, gridY);

			// If the tile was removed, never existed, or the main layer has no content:
			if(tileData == null || tileData[0] == 0) { return false; }

			TileObject tileObj = Systems.mapper.TileDict[tileData[0]];

			// Make sure the tile exists and collides, otherwise there's no point in testing any further:
			if(tileObj == null || !tileObj.collides) { return false; }

			// Check if the Tile is Blocking:
			if(tileObj is Ground || tileObj is BlockTile || tileObj is HorizontalWall || tileObj is ButtonFixed || tileObj is SpringFixed || tileObj is Cannon || tileObj is Placer) {
				return true;
			}

			if(tileObj is Ledge) {
				return dir == DirCardinal.Down;
			}

			// Tile may be blocking against specific directions:
			if(dir == DirCardinal.Up) {
				if(tileObj is PlatformFixedDown) { return true; }
			}

			return false;
		}

		// Perform Collision Detection against a designated Grid Square
		public static bool RunGridTest(GameObject actor, short gridX, short gridY, DirCardinal dir) {

			// Verify that a tile exists at the given location:
			byte[] tileData = actor.room.tilemap.GetTileDataAtGrid(gridX, gridY);

			// If the tile was removed, never existed, or the main layer has no content:
			if(tileData == null || tileData[0] == 0) { return false; }

			TileObject tileObj = Systems.mapper.TileDict[tileData[0]];

			// Make sure the tile exists and collides, otherwise there's no point in testing any further:
			if(tileObj == null || !tileObj.collides) { return false; }

			// Run Tile Collision
			return tileObj.RunImpact(actor.room, actor, gridX, gridY, dir);
		}

		// Detect interactions with 4 Grid Squares, with object's X,Y in the Top-Left square.
		public static void RunTileCollision(GameObject actor) {

			// Don't run collision if the actor is designated not to collide.
			if(actor.CollideVal <= CollideEnum.NoTileCollide) { return; }

			// Determine Tiles Potentially Touched
			short gridX = actor.GridX;
			short gridY = actor.GridY;
			short gridX2 = actor.GridX2;
			short gridY2 = actor.GridY2;

			bool vertOnly = gridX == gridX2;

			// If the object is only interacting with vertical squares:
			if(vertOnly) {
				if(actor.physics.velocity.Y < 0) { CollideTile.RunGridTest(actor, gridX, gridY, DirCardinal.Up); }
				else { CollideTile.RunGridTest(actor, gridX, gridY2, DirCardinal.Down); }
				return;
			}

			bool horOnly = gridY == gridY2;

			// If the object is only interacting between two tiles (left and right).
			if(horOnly) {
				if(actor.physics.velocity.X >= 0) { CollideTile.RunGridTest(actor, gridX2, gridY, DirCardinal.Right); }
				else { CollideTile.RunGridTest(actor, gridX, gridY, DirCardinal.Left); }
				return;
			}

			// If the object is interacting with all four tiles (Top-Left to Bottom-Right).
			FInt velX = actor.physics.velocity.X;
			FInt velY = actor.physics.velocity.Y;

			// If moving downward:
			if(velY > 0) {

				// If moving DOWN-RIGHT.
				if(velX > 0) {

					// Compare against BOTTOM-LEFT (vs. up) and TOP-RIGHT (vs. left)
					bool down = CollideTile.RunGridTest(actor, gridX, gridY2, DirCardinal.Down);
					bool right = CollideTile.RunGridTest(actor, gridX2, gridY, DirCardinal.Right);

					// Test against corner if neither of the above collided. Direction of collision based on momentum.
					if(!down && !right) {

						int yOverlap = actor.posY + actor.bounds.Bottom - (gridY2 * (byte)TilemapEnum.TileHeight); // Returns POSITIVE value
						if(yOverlap > velY.RoundInt) { CollideTile.RunGridTest(actor, gridX2, gridY2, DirCardinal.Right); return; }

						int xOverlap = actor.posX + actor.bounds.Right - (gridX2 * (byte)TilemapEnum.TileWidth); // Returns POSITIVE value
						if(xOverlap > velX.RoundInt) { CollideTile.RunGridTest(actor, gridX2, gridY2, DirCardinal.Down); }
					}
				}

				// If moving DOWN-LEFT:
				else if(velX < 0) {

					// Compare against BOTTOM-RIGHT (vs.up) and TOP-LEFT (vs. right)
					bool down = CollideTile.RunGridTest(actor, gridX2, gridY2, DirCardinal.Down);
					bool left = CollideTile.RunGridTest(actor, gridX, gridY, DirCardinal.Left);

					// Test against corner if neither of the above collided. Direction of collision based on momentum.
					if(!down && !left) {

						int yOverlap = actor.posY + actor.bounds.Bottom - (gridY2 * (byte)TilemapEnum.TileHeight); // Returns POSITIVE value
						if(yOverlap > velY.RoundInt) { CollideTile.RunGridTest(actor, gridX, gridY2, DirCardinal.Left); return; }

						int xOverlap = actor.posX + actor.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth); // Returns NEGATIVE value
						if(xOverlap < velX.RoundInt) { CollideTile.RunGridTest(actor, gridX, gridY2, DirCardinal.Down); }
					}
				}

				// If moving DOWN:
				else {

					// Get Overlaps
					int xOverlapLeft = actor.posX + actor.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth);
					int xOverlapRight = actor.posX + actor.bounds.Right - (gridX2 * (byte)TilemapEnum.TileWidth);

					// Compare against BOTTOM HALF (vs. up). No corner test.
					if(xOverlapLeft < 0) { CollideTile.RunGridTest(actor, gridX, gridY2, DirCardinal.Down); }
					if(xOverlapRight > 0) { CollideTile.RunGridTest(actor, gridX2, gridY2, DirCardinal.Down); }
				}
			}

			// If moving upward:
			else {

				// If moving UP-RIGHT:
				if(velX > 0) {

					// Compare against TOP-LEFT (vs. down) and BOTTOM-RIGHT (vs. left) 
					bool up = CollideTile.RunGridTest(actor, gridX, gridY, DirCardinal.Up);
					bool right = CollideTile.RunGridTest(actor, gridX2, gridY2, DirCardinal.Right);

					// Test against corner if neither of the above collided. Direction of collision based on momentum.
					if(!up && !right) {

						int yOverlap = actor.posY + actor.bounds.Top - (gridY2 * (byte)TilemapEnum.TileHeight); // Returns NEGATIVE value
						if(yOverlap < velY.RoundInt) { CollideTile.RunGridTest(actor, gridX2, gridY, DirCardinal.Right); return; }

						int xOverlap = actor.posX + actor.bounds.Right - (gridX2 * (byte)TilemapEnum.TileWidth); // Returns POSITIVE value
						if(xOverlap > velX.RoundInt) { CollideTile.RunGridTest(actor, gridX2, gridY, DirCardinal.Up); }
					}
				}

				// If moving UP-LEFT:
				else if(velX < 0) {

					// Compare against TOP-RIGHT (vs. down) and BOTTOM-LEFT (vs. right)
					bool up = CollideTile.RunGridTest(actor, gridX2, gridY, DirCardinal.Up);
					bool left = CollideTile.RunGridTest(actor, gridX, gridY2, DirCardinal.Left);

					// Test against corner if neither of the above collided. Direction of collision based on momentum.
					if(!up && !left) {

						int yOverlap = actor.posY + actor.bounds.Top - (gridY2 * (byte)TilemapEnum.TileHeight); // Returns NEGATIVE value
						if(yOverlap < velY.RoundInt) { CollideTile.RunGridTest(actor, gridX, gridY, DirCardinal.Left); return; }

						int xOverlap = actor.posX + actor.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth); // Returns NEGATIVE value
						if(xOverlap < velX.RoundInt) { CollideTile.RunGridTest(actor, gridX, gridY, DirCardinal.Up); }
					}
				}

				// If moving UP:
				else {

					// Get Overlaps
					int xOverlapLeft = actor.posX + actor.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth);
					int xOverlapRight = actor.posX + actor.bounds.Right - (gridX2 * (byte)TilemapEnum.TileWidth);

					// Compare against TOP HALF (vs. down). No corner test.
					if(xOverlapLeft < 0) { CollideTile.RunGridTest(actor, gridX, gridY, DirCardinal.Up); }
					if(xOverlapRight > 0) { CollideTile.RunGridTest(actor, gridX2, gridY, DirCardinal.Up); }
				}
			}
		}
	}
}
