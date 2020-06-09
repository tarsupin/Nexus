using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.ObjectComponents {

	public static class CollideTileFacing {

		// A Facing Tile Impact only collides if the actor is colliding against the faced direction.
		public static bool RunImpact(GameObject actor, ushort gridX, ushort gridY, DirCardinal dir, DirCardinal facing) {

			if(dir == DirCardinal.None) { return false; }

			// The Tile Faces Up. Collide if the Actor is moving is Down.
			if(facing == DirCardinal.Up) {
				if(dir == DirCardinal.Down) {
					actor.CollidePosDown(gridY * (byte)TilemapEnum.TileHeight - actor.bounds.Bottom);
					return true;
				}
			}

			// The Tile Faces Down. Collide if the Actor is moving is Up.
			else if(facing == DirCardinal.Down) {
				if(dir == DirCardinal.Up) {
					actor.CollidePosUp(gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight - actor.bounds.Top);
					return true;
				}
			}

			// The Tile Faces Left. Collide if the Actor is moving Right.
			else if(facing == DirCardinal.Left) {
				if(dir == DirCardinal.Right) {
					actor.CollidePosRight(gridX * (byte)TilemapEnum.TileWidth - actor.bounds.Right);
					return true;
				}
			}

			// The Tile Faces Right. Collide if the Actor is moving is Left.
			else if(facing == DirCardinal.Right) {
				if(dir == DirCardinal.Left) {
					actor.CollidePosLeft(gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.TileWidth - actor.bounds.Left);
					return true;
				}
			}

			return false;
		}

		// Identical to Impact, but only checks if it would be an impact. Doesn't affect physics.
		public static bool RunImpactTest(DirCardinal dir, DirCardinal facing) {
			if(facing == DirCardinal.Up) { return dir == DirCardinal.Down; }
			else if(facing == DirCardinal.Down) { return dir == DirCardinal.Up; }
			else if(facing == DirCardinal.Left) { return dir == DirCardinal.Right; }
			else if(facing == DirCardinal.Right) { return dir == DirCardinal.Left; }
			return false;
		}
	}
}
