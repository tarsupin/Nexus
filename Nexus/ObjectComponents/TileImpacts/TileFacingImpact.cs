﻿using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.ObjectComponents {

	public static class TileFacingImpact {

		// A Facing Tile Impact only collides if the actor is colliding against the faced direction.
		public static bool RunImpact(DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir, DirCardinal facing) {

			if(dir == DirCardinal.Center) { return false; }

			// The Tile Faces Up. Collide if the Actor is moving is Down.
			if(facing == DirCardinal.Up) {
				if(dir == DirCardinal.Down) {
					CollideTile.CollideDown(actor, gridY * (byte)TilemapEnum.TileHeight - actor.bounds.Bottom);
					return true;
				}
			}

			// The Tile Faces Down. Collide if the Actor is moving is Up.
			else if(facing == DirCardinal.Down) {
				if(dir == DirCardinal.Up) {
					CollideTile.CollideUp(actor, gridY * (byte)TilemapEnum.TileHeight - actor.bounds.Top);
					return true;
				}
			}

			// The Tile Faces Left. Collide if the Actor is moving Right.
			else if(facing == DirCardinal.Left) {
				if(dir == DirCardinal.Right) {
					CollideTile.CollideRight(actor, gridX * (byte)TilemapEnum.TileWidth - actor.bounds.Right);
					return true;
				}
			}

			// The Tile Faces Right. Collide if the Actor is moving is Left.
			else if(facing == DirCardinal.Right) {
				if(dir == DirCardinal.Left) {
					CollideTile.CollideLeft(actor, gridX * (byte)TilemapEnum.TileWidth - actor.bounds.Left);
					return true;
				}
			}

			return false;
		}
	}
}
