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
			if(obj.pos.Y.IntValue >= something) {
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
					if(obj is Character) {
						System.Console.WriteLine("TEST");
					}
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

							CollideTileAffect.AlignUp(obj, gridY * (byte)TilemapEnum.TileHeight);

							// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
						}

						return true;
					}

					// The Platform Faces Downward. Collide if the Direction is Upward.
					if(dir == DirCardinal.Up) {
						// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

						CollideTileAffect.AlignDown(obj, gridY * (byte)TilemapEnum.TileHeight);

						// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
					}

					return true;
				}

				// The Platform is Horizontal.

				// The Platform Faces Left
				if(tileData[(byte)TMBTiles.HorizontalFacing]) {

					// Collide if the Direction is Right.
					if(dir == DirCardinal.Right) {
						// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

						CollideTileAffect.AlignLeft(obj, gridX * (byte)TilemapEnum.TileWidth);

						// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
					}

					return true;
				}

				// The Platform Faces Right. Collide if the Direction is Left.
				if(dir == DirCardinal.Left) {
					// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

					CollideTileAffect.AlignRight(obj, gridX * (byte)TilemapEnum.TileWidth);

					// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
				}

				return true;
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

				int vel = obj.physics.velocity.Y.IntValue;
				if(vel >= 0) { CollideTile.RunGridTest(obj, obj.scene.tilemap, gridX, gridY2, DirCardinal.Down); } else if(vel < 0) { CollideTile.RunGridTest(obj, obj.scene.tilemap, gridX, gridY, DirCardinal.Up); }
			}

			// If the object is only interacting between two tiles (left and right).
			else if(horOnly) {
				int vel = obj.physics.velocity.X.IntValue;
				if(vel >= 0) { CollideTile.RunGridTest(obj, obj.scene.tilemap, gridX2, gridY, DirCardinal.Right); } else if(vel < 0) { CollideTile.RunGridTest(obj, obj.scene.tilemap, gridX, gridY, DirCardinal.Left); }
			}

			// If the object is interacting with all four tiles (Top-Left to Bottom-Right).
			else {

				TilemapBool tilemap = obj.scene.tilemap;

				FInt velX = obj.physics.velocity.X;
				FInt velY = obj.physics.velocity.Y;

				// Note: If you were already in the same grid squares last time, no collision tests are needed for the relevant squares.

				// TODO HIGH PRIORITY: The corners aren't going to work with abs(vel) > abs(velY), etc. If momentum were too fast, it'll just throw off the edge, I think.
				// TODO HIGH PRIORITY: X OVERLAPS!! See DOWN-LEFT collision, that works.

				// If moving downward:
				if(velY > 0) {

					// If moving DOWN-RIGHT.
					if(velX > 0) {

						// Compare against TOP-RIGHT (vs. left) and BOTTOM-LEFT (vs. up)
						bool down = CollideTile.RunGridTest(obj, tilemap, gridX, gridY2, DirCardinal.Down);
						bool right = CollideTile.RunGridTest(obj, tilemap, gridX2, gridY, DirCardinal.Right);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!down && !right) {
							CollideTile.RunGridTest(obj, tilemap, gridX2, gridY2, FInt.Abs(velX) > FInt.Abs(velY) ? DirCardinal.Down : DirCardinal.Right);
						}
					}

					// If moving DOWN-LEFT:
					else if(velX < 0) {

						// Compare against TOP-LEFT (vs. right) and BOTTOM-RIGHT (vs.up)
						bool down = CollideTile.RunGridTest(obj, tilemap, gridX2, gridY2, DirCardinal.Down);
						bool left = CollideTile.RunGridTest(obj, tilemap, gridX, gridY, DirCardinal.Left);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!down && !left) {
							FInt xOverlap = obj.pos.X + obj.bounds.Left - (gridX2 * (byte)TilemapEnum.TileWidth);
							CollideTile.RunGridTest(obj, tilemap, gridX, gridY2, xOverlap < obj.physics.AmountMoved.X ? DirCardinal.Down : DirCardinal.Left);
						}
					}

					// If moving DOWN:
					else {

						// Get Overlaps
						FInt xOverlap = obj.pos.X + obj.bounds.Right - (gridX2 * (byte)TilemapEnum.TileWidth);
						//FInt yOverlap = obj.pos.Y + obj.bounds.Bottom - (gridY2 * (byte)TilemapEnum.TileHeight);

						if(obj is Character && xOverlap < 2) {
							//var a = yOverlap.ToDouble().ToString();
							//System.Console.WriteLine("TEST ");
						}

						// Compare against BOTTOM HALF (vs. up). No corner test.
						CollideTile.RunGridTest(obj, tilemap, gridX, gridY2, DirCardinal.Down);
						if(xOverlap > 0) { CollideTile.RunGridTest(obj, tilemap, gridX2, gridY2, DirCardinal.Down); }
					}
				}

				// If moving upward:
				else {

					// If moving UP-RIGHT:
					if(velX > 0) {

						// Compare against BOTTOM-RIGHT (vs. left) and TOP-LEFT (vs. down)
						bool up = CollideTile.RunGridTest(obj, tilemap, gridX, gridY, DirCardinal.Up);
						bool right = CollideTile.RunGridTest(obj, tilemap, gridX2, gridY2, DirCardinal.Right);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!up && !right) {
							CollideTile.RunGridTest(obj, tilemap, gridX2, gridY, FInt.Abs(velX) > FInt.Abs(velY) ? DirCardinal.Up : DirCardinal.Right);
						}
					}

					// If moving UP-LEFT:
					else if(velX < 0) {

						// Compare against BOTTOM-LEFT (vs. right) and TOP-RIGHT (vs. down)
						bool up = CollideTile.RunGridTest(obj, tilemap, gridX2, gridY, DirCardinal.Up);
						bool left = CollideTile.RunGridTest(obj, tilemap, gridX, gridY2, DirCardinal.Left);

						// Test against corner if neither of the above collided. Direction of collision based on momentum.
						if(!up && !left) {
							CollideTile.RunGridTest(obj, tilemap, gridX, gridY, FInt.Abs(velX) > FInt.Abs(velY) ? DirCardinal.Up : DirCardinal.Left);
						}
					}

					// If moving UP:
					else {

						// Compare against TOP HALF (vs. down). No corner test.
						CollideTile.RunGridTest(obj, tilemap, gridX, gridY, DirCardinal.Up);
						CollideTile.RunGridTest(obj, tilemap, gridX, gridY, DirCardinal.Up);
					}
				}
			}
		}
	}
}
