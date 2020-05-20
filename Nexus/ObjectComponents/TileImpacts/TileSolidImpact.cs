using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.ObjectComponents {

	public static class TileSolidImpact {

		// A Standard Tile Impact just triggers the collision methods that are commonly overridden (collideLeft(), collideRight(), etc).
		public static bool RunImpact(DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir = DirCardinal.Center) {

			if(dir == DirCardinal.Down) {
				CollideTile.CollideDown(actor, gridY * (byte)TilemapEnum.TileHeight - actor.bounds.Bottom);
			} else if(dir == DirCardinal.Right) {
				CollideTile.CollideRight(actor, gridX * (byte)TilemapEnum.TileWidth - actor.bounds.Right);
			} else if(dir == DirCardinal.Left) {
				CollideTile.CollideLeft(actor, gridX * (byte)TilemapEnum.TileWidth - actor.bounds.Left);
			} else if(dir == DirCardinal.Up) {
				CollideTile.CollideUp(actor, gridY * (byte)TilemapEnum.TileHeight - actor.bounds.Top);
			}

			return true;
		}
	}
}
