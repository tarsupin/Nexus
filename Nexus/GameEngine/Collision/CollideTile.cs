using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

// CollideTile handles collision detection between objects and tiles.

namespace Nexus.GameEngine {

	public class CollideTile {

		// Perform Collision Detection against a designated Grid Square
		public static bool RunGridTest(DynamicGameObject actor, TilemapBool tilemap, ushort gridX, ushort gridY, DirCardinal dir) {

			// TODO HIGH PRIORITY: DELETE THE CRAP OUT OF THIS. It's just a temporary measure to avoid the tilePresent thing with bool below.
			// Destroy objects that get too close to bottom:
			var something = tilemap.Height - 258;
			if(actor.posY >= something) {
				actor.Destroy();
				return false;
			}

			// Verify that a tile exists at the given location:
			uint gridId = tilemap.GetGridID(gridX, gridY);

			TileGameObject tileObj = tilemap.GetTileAtGridID(gridId);

			if(tileObj == null) { return false; }

			// Make sure the tile has a collision, otherwise there's no point in testing any further:
			if(!tileObj.collides) { return false; }

			// Some tiles are Character-Only. Make sure the tile can collide with this actor:
			if(tileObj.charOnly && actor is Character == false) { return false; }

			// If we're dealing with a full block collision, we already know it's a confirmed hit.
			if(tileObj.facing == DirCardinal.Center) {

				// TODO: CONFIRMED HIT HERE. PROCESS IT.
				// tileData[(byte) TMBTiles.SpecialCollisionTest]  // NOTE: It already considers the collision to "hit"
				// tileData[(byte) TMBTiles.SpecialCollisionEffect]

				// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

				// Run Collision & Alignment based on the direction moved:
				if(dir == DirCardinal.Down) {
					CollideTileAffect.CollideDown(actor, gridY * (byte)TilemapEnum.TileHeight - actor.bounds.Bottom);
				} else if(dir == DirCardinal.Right) {
					CollideTileAffect.CollideRight(actor, gridX * (byte)TilemapEnum.TileWidth - actor.bounds.Right);
				} else if(dir == DirCardinal.Left) {
					CollideTileAffect.CollideLeft(actor, gridX * (byte)TilemapEnum.TileWidth - actor.bounds.Left);
				} else if(dir == DirCardinal.Up) {
					CollideTileAffect.CollideUp(actor, gridY * (byte)TilemapEnum.TileHeight - actor.bounds.Top);
				}

				// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
				return true;
			}

			// Colliding with a Platform:
			else if(tileObj is PlatformFixed) {

				// The Platform Faces Up. Collide if the Actor is moving is Down.
				if(tileObj.facing == DirCardinal.Up) {
					if(dir == DirCardinal.Down) {
						// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

						CollideTileAffect.CollideDown(actor, gridY * (byte)TilemapEnum.TileHeight - actor.bounds.Bottom);

						// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
						return true;
					}

					return false;
				}

				// The Platform Faces Down. Collide if the Actor is moving is Up.
				else if(tileObj.facing == DirCardinal.Down) {
					if(dir == DirCardinal.Up) {
						// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

						CollideTileAffect.CollideUp(actor, gridY * (byte)TilemapEnum.TileHeight - actor.bounds.Top);

						// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.

						return true;
					}
				}

				// The Platform Faces Left. Collide if the Actor is moving Right.
				else if(tileObj.facing == DirCardinal.Left) {
					if(dir == DirCardinal.Right) {
						// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

						CollideTileAffect.CollideRight(actor, gridX * (byte)TilemapEnum.TileWidth - actor.bounds.Right);

						// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
						return true;
					}

					return false;
				}

				// The Platform Faces Right. Collide if the Actor is moving is Left.
				else if(tileObj.facing == DirCardinal.Right) {
					if(dir == DirCardinal.Left) {
						// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

						CollideTileAffect.CollideLeft(actor, gridX * (byte)TilemapEnum.TileWidth - actor.bounds.Left);

						// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
						return false;
					}
				}
			}
			
			// Colliding with a Slope:
			// TODO: SLOPE COLLISION
			return false;
		}

		// Detect interactions with 4 Grid Squares, with object's X,Y in the Top-Left square.
		public static void RunQuadrantDetection(DynamicGameObject actor) {

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

				FInt velX = actor.physics.velocity.Y;
				if(velX >= 0) { CollideTile.RunGridTest(actor, actor.scene.tilemap, gridX, gridY2, DirCardinal.Down); }
				else if(velX < 0) { CollideTile.RunGridTest(actor, actor.scene.tilemap, gridX, gridY, DirCardinal.Up); }
			}

			// If the object is only interacting between two tiles (left and right).
			else if(horOnly) {
				FInt velX = actor.physics.velocity.X;
				if(velX >= 0) { CollideTile.RunGridTest(actor, actor.scene.tilemap, gridX2, gridY, DirCardinal.Right); }
				else if(velX < 0) { CollideTile.RunGridTest(actor, actor.scene.tilemap, gridX, gridY, DirCardinal.Left); }
			}

			// If the object is interacting with all four tiles (Top-Left to Bottom-Right).
			else {

				TilemapBool tilemap = actor.scene.tilemap;

				FInt velX = actor.physics.velocity.X;
				FInt velY = actor.physics.velocity.Y;

				// If moving downward:
				if(velY > 0) {

					// If moving DOWN-RIGHT.
					if(velX > 0) {

						// Compare against BOTTOM-LEFT (vs. up) and TOP-RIGHT (vs. left)
						bool down = CollideTile.RunGridTest(actor, tilemap, gridX, gridY2, DirCardinal.Down);
						bool right = CollideTile.RunGridTest(actor, tilemap, gridX2, gridY, DirCardinal.Right);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!down && !right) {
							int xOverlap = actor.posX + actor.bounds.Right - (gridX2 * (byte) TilemapEnum.TileWidth);
							CollideTile.RunGridTest(actor, tilemap, gridX2, gridY2, xOverlap > actor.physics.AmountMovedX ? DirCardinal.Down : DirCardinal.Right);
						}
					}

					// If moving DOWN-LEFT:
					else if(velX < 0) {

						// Compare against BOTTOM-RIGHT (vs.up) and TOP-LEFT (vs. right)
						bool down = CollideTile.RunGridTest(actor, tilemap, gridX2, gridY2, DirCardinal.Down);
						bool left = CollideTile.RunGridTest(actor, tilemap, gridX, gridY, DirCardinal.Left);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!down && !left) {
							int xOverlap = actor.posX + actor.bounds.Left - (gridX2 * (byte) TilemapEnum.TileWidth);
							CollideTile.RunGridTest(actor, tilemap, gridX, gridY2, xOverlap < actor.physics.AmountMovedX ? DirCardinal.Down : DirCardinal.Left);
						}
					}

					// If moving DOWN:
					else {

						// Get Overlaps
						int xOverlapLeft = actor.posX + actor.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth);
						int xOverlapRight = actor.posX + actor.bounds.Right - (gridX2 * (byte)TilemapEnum.TileWidth);

						// Compare against BOTTOM HALF (vs. up). No corner test.
						if(xOverlapLeft < 0) { CollideTile.RunGridTest(actor, tilemap, gridX, gridY2, DirCardinal.Down); }
						if(xOverlapRight > 0) { CollideTile.RunGridTest(actor, tilemap, gridX2, gridY2, DirCardinal.Down); }
					}
				}

				// If moving upward:
				else {

					// If moving UP-RIGHT:
					if(velX > 0) {

						// Compare against TOP-LEFT (vs. down) and BOTTOM-RIGHT (vs. left) 
						bool up = CollideTile.RunGridTest(actor, tilemap, gridX, gridY, DirCardinal.Up);
						bool right = CollideTile.RunGridTest(actor, tilemap, gridX2, gridY2, DirCardinal.Right);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!up && !right) {
							int xOverlap = actor.posX + actor.bounds.Right - (gridX2 * (byte)TilemapEnum.TileWidth);
							CollideTile.RunGridTest(actor, tilemap, gridX2, gridY, xOverlap > actor.physics.AmountMovedX ? DirCardinal.Up : DirCardinal.Right);
						}
					}

					// If moving UP-LEFT:
					else if(velX < 0) {

						// Compare against TOP-RIGHT (vs. down) and BOTTOM-LEFT (vs. right)
						bool up = CollideTile.RunGridTest(actor, tilemap, gridX2, gridY, DirCardinal.Up);
						bool left = CollideTile.RunGridTest(actor, tilemap, gridX, gridY2, DirCardinal.Left);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!up && !left) {
							int xOverlap = actor.posX + actor.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth);
							CollideTile.RunGridTest(actor, tilemap, gridX, gridY, xOverlap < actor.physics.AmountMovedX ? DirCardinal.Up : DirCardinal.Left);
						}
					}

					// If moving UP:
					else {

						// Get Overlaps
						int xOverlapLeft = actor.posX + actor.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth);
						int xOverlapRight = actor.posX + actor.bounds.Right - (gridX2 * (byte)TilemapEnum.TileWidth);

						// Compare against TOP HALF (vs. down). No corner test.
						if(xOverlapLeft < 0) { CollideTile.RunGridTest(actor, tilemap, gridX, gridY, DirCardinal.Up); }
						if(xOverlapRight > 0) { CollideTile.RunGridTest(actor, tilemap, gridX2, gridY, DirCardinal.Up); }
					}
				}
			}
		}
	}
}
