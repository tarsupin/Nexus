using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.ObjectComponents {

	public static class TileSolidImpact {

		// A Standard Tile Impact just triggers the collision methods that are commonly overridden (collideLeft(), collideRight(), etc).
		public static bool RunImpact(DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir = DirCardinal.None) {

			if(dir == DirCardinal.Down) {
				actor.CollidePosDown(gridY * (byte)TilemapEnum.TileHeight - actor.bounds.Bottom);
			} else if(dir == DirCardinal.Right) {
				actor.CollidePosRight(gridX * (byte)TilemapEnum.TileWidth - actor.bounds.Right);
			} else if(dir == DirCardinal.Left) {
				actor.CollidePosLeft(gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.TileWidth - actor.bounds.Left);
			} else if(dir == DirCardinal.Up) {
				actor.CollidePosUp(gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight - actor.bounds.Top);
			}

			return true;
		}

		// An Inner Tile Impact checks for an additional, inner collision based on a designated rectangle.
		public static DirCardinal RunInnerImpact(DynamicObject actor, int x1, int x2, int y1, int y2) {

			// Check Overlap with Altered Border
			if(!CollideRect.IsTouchingRect(actor, x1, x2, y1, y2)) { return DirCardinal.None; }

			DirCardinal newDir = CollideRect.GetDirectionOfCollision(actor, x1, x2, y1, y2);

			if(newDir == DirCardinal.Down) {
				actor.CollidePosDown(y1 - actor.bounds.Bottom);
			} else if(newDir == DirCardinal.Right) {
				actor.CollidePosRight(x1 - actor.bounds.Right);
			} else if(newDir == DirCardinal.Left) {
				actor.CollidePosLeft(x2 - actor.bounds.Left);
			} else if(newDir == DirCardinal.Up) {
				actor.CollidePosUp(y2 - actor.bounds.Top);
			}

			return newDir;
		}

		// Identical to RunInnerImpact, except it doesn't collide.
		public static DirCardinal RunOverlapTest(DynamicObject actor, int x1, int x2, int y1, int y2) {
			if(!CollideRect.IsTouchingRect(actor, x1, x2, y1, y2)) { return DirCardinal.None; }
			return CollideRect.GetDirectionOfCollision(actor, x1, x2, y1, y2);
		}
	}
}
