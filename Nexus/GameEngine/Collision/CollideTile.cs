using Nexus.Gameplay;

// CollideTile handles collision detection between objects and tiles.

namespace Nexus.GameEngine {

	public class CollideTile {

		// Perform Collision Detection against a designated Grid Square
		public static bool RunGridTest(DynamicGameObject obj, TilemapBool tilemap, ushort gridX, ushort gridY, DirCardinal dir) {

			// Verify that a tile exists at the given location:
			if(!tilemap.IsTilePresent(gridX, gridY)) { return false; }

			uint gridId = tilemap.GetGridID(gridX, gridY);

			bool[] tileData = tilemap.tiles[gridId];

			// Make sure the tile has a collision, otherwise there's no point in testing any further:
			if(!tileData[(byte) TMBTiles.HasCollision]) { return false; }

			// Determine behavior of the Tile, so collision can act accordingly.

			// Character Only
			// TODO HIGH PRIORITY: Character only tiles.
			//if(tileData[(byte) TMBTiles.CharOnly] && obj is Character == false) {}

			// Is Tile
			if(tileData[(byte) TMBTiles.IsTile]) {

			}

			// If we're dealing with a full block collision, we already know it's a confirmed hit.
			if(tileData[(byte) TMBTiles.FullCollision]) {

				// TODO: CONFIRMED HIT HERE. PROCESS IT.
				// tileData[(byte) TMBTiles.SpecialCollisionTest]  // NOTE: It already considers the collision to "hit"
				// tileData[(byte) TMBTiles.SpecialCollisionEffect]

				// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"


				// Run Collision & Alignment based on the direction moved:
				if(dir == DirCardinal.Down) {
					CollideTileAffect.CollideDown(obj, gridY * (byte)TilemapEnum.TileHeight);
				} else if(dir == DirCardinal.Right) {
					CollideTileAffect.CollideRight(obj, gridX * (byte)TilemapEnum.TileWidth);
				} else if(dir == DirCardinal.Left) {
					CollideTileAffect.CollideLeft(obj, gridX * (byte)TilemapEnum.TileWidth);
				} else if(dir == DirCardinal.Up) {
					CollideTileAffect.CollideUp(obj, gridY * (byte)TilemapEnum.TileHeight);
				}

				// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
				return true;
			}

			// Colliding with a Platform:
			if(!tileData[(byte) TMBTiles.SlopeOrPlatform]) {

				// The Platform is Vertical:
				if(tileData[(byte) TMBTiles.PlatformIsVert]) {

					// The Platform Faces Upward:
					if(tileData[(byte) TMBTiles.VerticalFacing]) {

						// Collide if the Direction is Downward.
						if(dir == DirCardinal.Down) {

							// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

							CollideTileAffect.AlignUp(obj, gridY * (byte) TilemapEnum.TileHeight);

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
				if(tileData[(byte) TMBTiles.HorizontalFacing]) {

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
		public static void RunQuadrantDetection( DynamicGameObject obj ) {
			
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
				if(vel < 0) { CollideTile.RunGridTest(obj, obj.scene.tilemap, gridX, gridY, DirCardinal.Up); }
				else if(vel > 0) { CollideTile.RunGridTest(obj, obj.scene.tilemap, gridX, gridY2, DirCardinal.Down); }
			}

			// If the object is only interacting between two tiles (left and right).
			else if(horOnly) {
				int vel = obj.physics.velocity.X.IntValue;
				if(vel < 0) { CollideTile.RunGridTest(obj, obj.scene.tilemap, gridX, gridY, DirCardinal.Left); }
				else if(vel > 0) { CollideTile.RunGridTest(obj, obj.scene.tilemap, gridX2, gridY, DirCardinal.Right); }
			}

			// If the object is interacting with all four tiles (Top-Left to Bottom-Right).
			// This requires more testing, since you need to know the relative position of the object's last frame.
			// If the object was already overlapping the right side, no right-alignment needs to occur. You could only hit the upper-right block from below.
			// If the object was already overlapping the upper side, no upper-alignment needs to occur. You could only hit the upper-right block from the left.
			else {

				TilemapBool tilemap = obj.scene.tilemap;

				int velX = obj.physics.velocity.X.IntValue;
				int velY = obj.physics.velocity.Y.IntValue;

				// TODO CLEANUP: Remove this once it's proven unnecessary.
				//int lastX = obj.pos.X.IntValue;
				//int lastY = obj.pos.Y.IntValue;

				//int lastXMove = 0 - obj.physics.velocity.X.IntValue;
				//int lastYMove = 0 - obj.physics.velocity.Y.IntValue;

				//// Get the last frame's grid positions (required to determine corner effects).
				//ushort lastGridX = TilemapBool.GridX(lastX + lastXMove + obj.bounds.Left);
				//ushort lastGridY = TilemapBool.GridY(lastY + lastYMove + obj.bounds.Top);

				//ushort lastGridX2 = TilemapBool.GridX(lastX + lastXMove + obj.bounds.Right);
				//ushort lastGridY2 = TilemapBool.GridY(lastY + lastYMove + obj.bounds.Bottom);

				// If the object gets realigned, no need to perform related re-tests on certain tiles.
				bool bottomRealigned = false;
				bool topRealigned = false;

				// Note: If you were already in the same grid squares last time, no collision tests are needed for the relevant squares.

				// Lower-Left and Lower-Right Quadrants. Collide only if moving DOWN this frame.
				if(velY > 0) {
					bottomRealigned = CollideTile.RunGridTest(obj, tilemap, gridX, gridY2, DirCardinal.Down);
					if(!bottomRealigned) { CollideTile.RunGridTest(obj, tilemap, gridX2, gridY2, DirCardinal.Down); }
				}

				// Upper-Left and Upper-Right Quadrants. Collide only if moving UP this frame.
				else if(velY < 0) {
					topRealigned = CollideTile.RunGridTest(obj, tilemap, gridX, gridY, DirCardinal.Up);
					if(!topRealigned) { CollideTile.RunGridTest(obj, tilemap, gridX2, gridY, DirCardinal.Up); }
				}

				// Upper-Left and Lower-Left Quadrants. Collide only if moving LEFT this frame.
				if(velX < 0) {
					bool leftRealigned = false;
					if(!topRealigned) { leftRealigned = CollideTile.RunGridTest(obj, tilemap, gridX, gridY, DirCardinal.Left); }
					if(!leftRealigned && !bottomRealigned) { CollideTile.RunGridTest(obj, tilemap, gridX, gridY2, DirCardinal.Left); }
				}

				// Upper-Right and Lower-Right Quadrants. Collide only if moving RIGHT this frame.
				else if(velX > 0) {
					bool rightRealigned = false;
					if(!topRealigned) { CollideTile.RunGridTest(obj, tilemap, gridX2, gridY, DirCardinal.Right); }
					if(!rightRealigned && !bottomRealigned) { CollideTile.RunGridTest(obj, tilemap, gridX2, gridY2, DirCardinal.Right); }
				}
			}
		}
	}
}
