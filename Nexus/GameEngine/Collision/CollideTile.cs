using Nexus.Engine;
using Nexus.Gameplay;

// CollideTile handles collision detection between objects and tiles.

namespace Nexus.GameEngine {

	public class CollideTile {

		// Tests if object is completely within a tile square.
		public static bool IsWithinTile(DynamicGameObject dynamicObj, ushort gridX, ushort gridY) {
			return (dynamicObj.GridX == gridX && dynamicObj.GridX2 == gridX && dynamicObj.GridY == gridY && dynamicObj.GridY2 == gridY);
		}

		// Tests if object is within a tile with padded borders. Note: Many tests can't use this, since they're probably already touching the tile.
		public static bool IsWithinPaddedTile(DynamicGameObject dynamicObj, ushort gridX, ushort gridY, byte left = 0, byte right = 0, byte top = 0, byte bottom = 0) {

			byte w = (byte) TilemapEnum.TileWidth;
			byte h = (byte) TilemapEnum.TileHeight;
			int xStart = gridX * w;
			int yStart = gridY * h;

			return (dynamicObj.posX + dynamicObj.bounds.Left >= xStart - left && dynamicObj.posX + dynamicObj.bounds.Right <= xStart + w + right && dynamicObj.posY + dynamicObj.bounds.Top >= yStart - top && dynamicObj.posY + dynamicObj.bounds.Bottom <= yStart + h + bottom);
		}

		// Perform Collision Detection against a designated Grid Square
		public static bool RunGridTest(RoomScene room, DynamicGameObject actor, TilemapLevel tilemap, ushort gridX, ushort gridY, DirCardinal dir) {

			// TODO HIGH PRIORITY: DELETE THE CRAP OUT OF THIS. It's just a temporary measure to avoid the tilePresent thing with bool below.
			// Destroy objects that get too close to bottom:
			var something = tilemap.Height;
			if(actor.posY >= something) {
				actor.Destroy();
				return false;
			}

			// Verify that a tile exists at the given location:
			uint gridId = tilemap.GetGridID(gridX, gridY);

			byte[] tileData = tilemap.GetTileDataAtGridID(gridId);

			// Make sure the tile class exists:
			if(tileData == null) {
				// TODO HIGH PRIOIRTY: This block should never run; tileData should never be null.
				// Remove any invalid options here.
				// Delete this block once the TileMap has been completed.
				return false;
			}

			TileGameObject tileObj = Systems.mapper.TileDict[tileData[0]];

			// Make sure the tile exists and collides, otherwise there's no point in testing any further:
			if(tileObj == null || !tileObj.collides) { return false; }

			// Run Tile Collision
			return tileObj.RunImpact(room, actor, gridX, gridY, dir);
		}

		// Detect interactions with 4 Grid Squares, with object's X,Y in the Top-Left square.
		public static void RunQuadrantDetection(RoomScene room, DynamicGameObject actor) {

			// Don't run collision if the actor is designated not to collide.
			if(actor.Activity <= Activity.NoCollide) { return; }

			// Determine Tiles Potentially Touched
			ushort gridX = actor.GridX;
			ushort gridY = actor.GridY;
			ushort gridX2 = actor.GridX2;
			ushort gridY2 = actor.GridY2;

			bool vertOnly = gridX == gridX2;
			bool horOnly = gridY == gridY2;

			// If the object is only interacting with vertical squares:
			if(vertOnly) {

				// If the object is only interacting with a single square (the one it's on), no need to run collisions.
				if(horOnly) { return; }

				FInt velY = actor.physics.velocity.Y;

				if(velY >= 0) {

					// TODO: Want to implement this, but causes corner clipping when all of them are added.
					// If the Y-Overlap has exceeded the maximum allowed, no need to run any tests.
					//int yOverlap = actor.posY + actor.bounds.Bottom - (gridY2 * (byte)TilemapEnum.TileHeight);
					//if(yOverlap > actor.physics.AmountMovedY) { return; }

					CollideTile.RunGridTest(room, actor, actor.room.tilemap, gridX, gridY2, DirCardinal.Down);
				}

				else if(velY < 0) { CollideTile.RunGridTest(room, actor, actor.room.tilemap, gridX, gridY, DirCardinal.Up); }
			}

			// If the object is only interacting between two tiles (left and right).
			else if(horOnly) {
				FInt velX = actor.physics.velocity.X;
				if(velX >= 0) { CollideTile.RunGridTest(room, actor, actor.room.tilemap, gridX2, gridY, DirCardinal.Right); }
				else if(velX < 0) { CollideTile.RunGridTest(room, actor, actor.room.tilemap, gridX, gridY, DirCardinal.Left); }
			}

			// If the object is interacting with all four tiles (Top-Left to Bottom-Right).
			else {

				TilemapLevel tilemap = actor.room.tilemap;

				FInt velX = actor.physics.velocity.X;
				FInt velY = actor.physics.velocity.Y;

				// If moving downward:
				if(velY > 0) {

					// If moving DOWN-RIGHT.
					if(velX > 0) {

						// Compare against BOTTOM-LEFT (vs. up) and TOP-RIGHT (vs. left)
						bool down = CollideTile.RunGridTest(room, actor, tilemap, gridX, gridY2, DirCardinal.Down);
						bool right = CollideTile.RunGridTest(room, actor, tilemap, gridX2, gridY, DirCardinal.Right);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!down && !right) {
							int xOverlap = actor.posX + actor.bounds.Right - (gridX2 * (byte) TilemapEnum.TileWidth);

							// TODO: Want to implement this, but causes corner clipping when all of them are added.
							// If the Y-Overlap has exceeded the maximum allowed, no need to run any tests.
							//int yOverlap = actor.posY + actor.bounds.Bottom - (gridY2 * (byte)TilemapEnum.TileHeight);
							//if(yOverlap > actor.physics.AmountMovedY) { return; }

							CollideTile.RunGridTest(room, actor, tilemap, gridX2, gridY2, xOverlap > actor.physics.AmountMovedX ? DirCardinal.Down : DirCardinal.Right);
						}
					}

					// If moving DOWN-LEFT:
					else if(velX < 0) {

						// Compare against BOTTOM-RIGHT (vs.up) and TOP-LEFT (vs. right)
						bool down = CollideTile.RunGridTest(room, actor, tilemap, gridX2, gridY2, DirCardinal.Down);
						bool left = CollideTile.RunGridTest(room, actor, tilemap, gridX, gridY, DirCardinal.Left);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!down && !left) {

							// TODO: Want to implement this, but causes corner clipping when all of them are added.
							// If the Y-Overlap has exceeded the maximum allowed, no need to run any tests.
							//int yOverlap = actor.posY + actor.bounds.Bottom - (gridY2 * (byte)TilemapEnum.TileHeight);
							//if(yOverlap > actor.physics.AmountMovedY) { return; }

							int xOverlap = actor.posX + actor.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth);
							CollideTile.RunGridTest(room, actor, tilemap, gridX, gridY2, xOverlap < actor.physics.AmountMovedX ? DirCardinal.Down : DirCardinal.Left);
						}
					}

					// If moving DOWN:
					else {

						// TODO: Want to implement this, but causes corner clipping when all of them are added.
						// If the Y-Overlap has exceeded the maximum allowed, no need to run any tests.
						//int yOverlap = actor.posY + actor.bounds.Bottom - (gridY2 * (byte)TilemapEnum.TileHeight);
						//if(yOverlap > actor.physics.AmountMovedY) { return; }

						// Get Overlaps
						int xOverlapLeft = actor.posX + actor.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth);
						int xOverlapRight = actor.posX + actor.bounds.Right - (gridX2 * (byte)TilemapEnum.TileWidth);

						// Compare against BOTTOM HALF (vs. up). No corner test.
						if(xOverlapLeft < 0) { CollideTile.RunGridTest(room, actor, tilemap, gridX, gridY2, DirCardinal.Down); }
						if(xOverlapRight > 0) { CollideTile.RunGridTest(room, actor, tilemap, gridX2, gridY2, DirCardinal.Down); }
					}
				}

				// If moving upward:
				else {

					// If moving UP-RIGHT:
					if(velX > 0) {

						// Compare against TOP-LEFT (vs. down) and BOTTOM-RIGHT (vs. left) 
						bool up = CollideTile.RunGridTest(room, actor, tilemap, gridX, gridY, DirCardinal.Up);
						bool right = CollideTile.RunGridTest(room, actor, tilemap, gridX2, gridY2, DirCardinal.Right);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!up && !right) {
							int xOverlap = actor.posX + actor.bounds.Right - (gridX2 * (byte)TilemapEnum.TileWidth);
							CollideTile.RunGridTest(room, actor, tilemap, gridX2, gridY, xOverlap > actor.physics.AmountMovedX ? DirCardinal.Up : DirCardinal.Right);
						}
					}

					// If moving UP-LEFT:
					else if(velX < 0) {

						// Compare against TOP-RIGHT (vs. down) and BOTTOM-LEFT (vs. right)
						bool up = CollideTile.RunGridTest(room, actor, tilemap, gridX2, gridY, DirCardinal.Up);
						bool left = CollideTile.RunGridTest(room, actor, tilemap, gridX, gridY2, DirCardinal.Left);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!up && !left) {
							int xOverlap = actor.posX + actor.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth);
							CollideTile.RunGridTest(room, actor, tilemap, gridX, gridY, xOverlap < actor.physics.AmountMovedX ? DirCardinal.Up : DirCardinal.Left);
						}
					}

					// If moving UP:
					else {

						// Get Overlaps
						int xOverlapLeft = actor.posX + actor.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth);
						int xOverlapRight = actor.posX + actor.bounds.Right - (gridX2 * (byte)TilemapEnum.TileWidth);

						// Compare against TOP HALF (vs. down). No corner test.
						if(xOverlapLeft < 0) { CollideTile.RunGridTest(room, actor, tilemap, gridX, gridY, DirCardinal.Up); }
						if(xOverlapRight > 0) { CollideTile.RunGridTest(room, actor, tilemap, gridX2, gridY, DirCardinal.Up); }
					}
				}
			}
		}
	}
}
