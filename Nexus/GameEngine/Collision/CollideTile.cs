using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

// CollideTile handles collision detection between objects and tiles.

namespace Nexus.GameEngine {

	public class CollideTile {

		// Perform Collision Detection against a designated Grid Square
		public static bool RunGridTest(DynamicGameObject obj, TilemapBool tilemap, ushort gridX, ushort gridY, DirCardinal dir) {

			// TODO HIGH PRIORITY: DELETE THE CRAP OUT OF THIS. It's just a temporary measure to avoid the tilePresent thing with bool below.
			// Destroy objects that get too close to bottom:
			var something = tilemap.height - 258;
			if(obj.posY >= something) {
				obj.Destroy();
				return false;
			}

			// Verify that a tile exists at the given location:
			if(!tilemap.IsTilePresent(gridX, gridY)) { return false; }

			uint gridId = tilemap.GetGridID(gridX, gridY);

			bool[] tileData = tilemap.tiles[gridId];

			// Make sure the tile has a collision, otherwise there's no point in testing any further:
			if(!tileData[(byte)TMBTiles.HasCollision]) { return false; }

			// Determine behavior of the Tile, so collision can act accordingly.

			// Character Only
			// TODO HIGH PRIORITY: Character only tiles.
			//if(tileData[(byte) TMBTiles.CharOnly] && obj is Character == false) {}

			// Is Tile
			if(tileData[(byte)TMBTiles.IsTile]) {

			}

			// If we're dealing with a full block collision, we already know it's a confirmed hit.
			if(tileData[(byte)TMBTiles.FullCollision]) {

				// TODO: CONFIRMED HIT HERE. PROCESS IT.
				// tileData[(byte) TMBTiles.SpecialCollisionTest]  // NOTE: It already considers the collision to "hit"
				// tileData[(byte) TMBTiles.SpecialCollisionEffect]

				// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"


				// Run Collision & Alignment based on the direction moved:
				if(dir == DirCardinal.Down) {
					CollideTileAffect.CollideDown(obj, gridY * (byte)TilemapEnum.TileHeight - obj.bounds.Bottom);
				} else if(dir == DirCardinal.Right) {
					CollideTileAffect.CollideRight(obj, gridX * (byte)TilemapEnum.TileWidth - obj.bounds.Right);
				} else if(dir == DirCardinal.Left) {
					CollideTileAffect.CollideLeft(obj, gridX * (byte)TilemapEnum.TileWidth - obj.bounds.Left);
				} else if(dir == DirCardinal.Up) {
					CollideTileAffect.CollideUp(obj, gridY * (byte)TilemapEnum.TileHeight - obj.bounds.Top);
				}

				// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
				return true;
			}

			// Colliding with a Platform:
			if(!tileData[(byte)TMBTiles.SlopeOrPlatform]) {

				// The Platform is Vertical:
				if(tileData[(byte)TMBTiles.PlatformIsVert]) {

					// The Platform Faces Upward:
					if(tileData[(byte)TMBTiles.VerticalFacing]) {

						// Collide if the Direction is Downward.
						if(dir == DirCardinal.Down) {

							// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

							CollideTileAffect.CollideDown(obj, gridY * (byte)TilemapEnum.TileHeight - obj.bounds.Bottom);

							// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
							return true;
						}

						return false;
					}

					// The Platform Faces Downward. Collide if the Direction is Upward.
					if(dir == DirCardinal.Up) {
						// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

						CollideTileAffect.CollideUp(obj, gridY * (byte)TilemapEnum.TileHeight - obj.bounds.Top);

						// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.

						return true;
					}

					return false;
				}

				// The Platform is Horizontal.

				// The Platform Faces Left
				if(tileData[(byte)TMBTiles.HorizontalFacing]) {

					// Collide if the Direction is Right.
					if(dir == DirCardinal.Right) {
						// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

						CollideTileAffect.CollideRight(obj, gridX * (byte)TilemapEnum.TileWidth - obj.bounds.Right);

						// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
						return true;
					}

					return false;
				}

				// The Platform Faces Right. Collide if the Direction is Left.
				if(dir == DirCardinal.Left) {
					// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

					CollideTileAffect.CollideLeft(obj, gridX * (byte)TilemapEnum.TileWidth - obj.bounds.Left);

					// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
					return true;
				}

				return false;
			}

			// Colliding with a Slope:
			// TODO: SLOPE COLLISION
			return false;
		}

		// Detect interactions with 4 Grid Squares, with object's X,Y in the Top-Left square.
		public static void RunQuadrantDetection(DynamicGameObject obj) {

			// Determine Tiles Potentially Touched
			ushort gridX = obj.GridX;
			ushort gridY = obj.GridY;
			ushort gridX2 = obj.GridX2;
			ushort gridY2 = obj.GridY2;

			bool vertOnly = gridX == gridX2;
			bool horOnly = gridY == gridY2;

			// If the object is only interacting with vertical squares:
			if(vertOnly) {

				// If the object is only interacting with a single square (the one it's on), no need to run collisions.
				if(horOnly) { return; }

				FInt velX = obj.physics.velocity.Y;
				if(velX >= 0) { CollideTile.RunGridTest(obj, obj.scene.tilemap, gridX, gridY2, DirCardinal.Down); }
				else if(velX < 0) { CollideTile.RunGridTest(obj, obj.scene.tilemap, gridX, gridY, DirCardinal.Up); }
			}

			// If the object is only interacting between two tiles (left and right).
			else if(horOnly) {
				FInt velX = obj.physics.velocity.X;
				if(velX >= 0) { CollideTile.RunGridTest(obj, obj.scene.tilemap, gridX2, gridY, DirCardinal.Right); }
				else if(velX < 0) { CollideTile.RunGridTest(obj, obj.scene.tilemap, gridX, gridY, DirCardinal.Left); }
			}

			// If the object is interacting with all four tiles (Top-Left to Bottom-Right).
			else {

				TilemapBool tilemap = obj.scene.tilemap;

				FInt velX = obj.physics.velocity.X;
				FInt velY = obj.physics.velocity.Y;

				// If moving downward:
				if(velY > 0) {

					// If moving DOWN-RIGHT.
					if(velX > 0) {

						// Compare against BOTTOM-LEFT (vs. up) and TOP-RIGHT (vs. left)
						bool down = CollideTile.RunGridTest(obj, tilemap, gridX, gridY2, DirCardinal.Down);
						bool right = CollideTile.RunGridTest(obj, tilemap, gridX2, gridY, DirCardinal.Right);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!down && !right) {
							int xOverlap = obj.posX + obj.bounds.Right - (gridX2 * (byte) TilemapEnum.TileWidth);
							CollideTile.RunGridTest(obj, tilemap, gridX2, gridY2, xOverlap > obj.physics.AmountMovedX ? DirCardinal.Down : DirCardinal.Right);
						}
					}

					// If moving DOWN-LEFT:
					else if(velX < 0) {

						// Compare against BOTTOM-RIGHT (vs.up) and TOP-LEFT (vs. right)
						bool down = CollideTile.RunGridTest(obj, tilemap, gridX2, gridY2, DirCardinal.Down);
						bool left = CollideTile.RunGridTest(obj, tilemap, gridX, gridY, DirCardinal.Left);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!down && !left) {
							int xOverlap = obj.posX + obj.bounds.Left - (gridX2 * (byte) TilemapEnum.TileWidth);
							CollideTile.RunGridTest(obj, tilemap, gridX, gridY2, xOverlap < obj.physics.AmountMovedX ? DirCardinal.Down : DirCardinal.Left);
						}
					}

					// If moving DOWN:
					else {

						// Get Overlaps
						int xOverlapLeft = obj.posX + obj.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth);
						int xOverlapRight = obj.posX + obj.bounds.Right - (gridX2 * (byte)TilemapEnum.TileWidth);

						// Compare against BOTTOM HALF (vs. up). No corner test.
						if(xOverlapLeft < 0) { CollideTile.RunGridTest(obj, tilemap, gridX, gridY2, DirCardinal.Down); }
						if(xOverlapRight > 0) { CollideTile.RunGridTest(obj, tilemap, gridX2, gridY2, DirCardinal.Down); }
					}
				}

				// If moving upward:
				else {

					// If moving UP-RIGHT:
					if(velX > 0) {

						// Compare against TOP-LEFT (vs. down) and BOTTOM-RIGHT (vs. left) 
						bool up = CollideTile.RunGridTest(obj, tilemap, gridX, gridY, DirCardinal.Up);
						bool right = CollideTile.RunGridTest(obj, tilemap, gridX2, gridY2, DirCardinal.Right);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!up && !right) {
							int xOverlap = obj.posX + obj.bounds.Right - (gridX2 * (byte)TilemapEnum.TileWidth);
							CollideTile.RunGridTest(obj, tilemap, gridX2, gridY, xOverlap > obj.physics.AmountMovedX ? DirCardinal.Up : DirCardinal.Right);
						}
					}

					// If moving UP-LEFT:
					else if(velX < 0) {

						// Compare against TOP-RIGHT (vs. down) and BOTTOM-LEFT (vs. right)
						bool up = CollideTile.RunGridTest(obj, tilemap, gridX2, gridY, DirCardinal.Up);
						bool left = CollideTile.RunGridTest(obj, tilemap, gridX, gridY2, DirCardinal.Left);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!up && !left) {
							int xOverlap = obj.posX + obj.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth);
							CollideTile.RunGridTest(obj, tilemap, gridX, gridY, xOverlap < obj.physics.AmountMovedX ? DirCardinal.Up : DirCardinal.Left);
						}
					}

					// If moving UP:
					else {

						// Get Overlaps
						int xOverlapLeft = obj.posX + obj.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth);
						int xOverlapRight = obj.posX + obj.bounds.Right - (gridX2 * (byte)TilemapEnum.TileWidth);

						// Compare against TOP HALF (vs. down). No corner test.
						if(xOverlapLeft < 0) { CollideTile.RunGridTest(obj, tilemap, gridX, gridY, DirCardinal.Up); }
						if(xOverlapRight > 0) { CollideTile.RunGridTest(obj, tilemap, gridX2, gridY, DirCardinal.Up); }
					}
				}
			}
		}
	}
}
