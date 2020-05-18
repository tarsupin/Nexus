using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.ObjectComponents {

	public static class TileSolidImpact {

		// A Standard Tile Impact just triggers the collision methods that are commonly overridden (collideLeft(), collideRight(), etc).
		public static bool RunImpact(DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir = DirCardinal.Center) {

			if(dir == DirCardinal.Down) {
				actor.physics.CollideDown(gridY * (byte)TilemapEnum.TileHeight);
			} else if(dir == DirCardinal.Right) {
				actor.physics.CollideRight(gridX * (byte)TilemapEnum.TileWidth);
			} else if(dir == DirCardinal.Left) {
				actor.physics.CollideLeft(gridX * (byte)TilemapEnum.TileWidth);
			} else if(dir == DirCardinal.Up) {
				actor.physics.CollideUp(gridY * (byte)TilemapEnum.TileHeight);
			}

			return true;
		}
	}
}
