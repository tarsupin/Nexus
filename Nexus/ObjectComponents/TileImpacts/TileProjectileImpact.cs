using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public static class TileProjectileImpact {

		// A Standard Tile Impact just triggers the collision methods that are commonly overridden (collideLeft(), collideRight(), etc).
		public static bool RunImpact(Projectile projectile, short gridX, short gridY, DirCardinal dir = DirCardinal.None) {

			// Some Projectiles get to ignore walls.
			if(projectile.CollisionType == ProjectileCollisionType.IgnoreWalls) {
				return false;
			}

			// Some Projectiles get destroyed on collision:
			if(projectile.CollisionType == ProjectileCollisionType.DestroyOnCollide) {
				projectile.Destroy( dir );
				return true;
			}

			// Some Projectiles bounce against the floor, or otherwise collide.
			if(projectile.CollisionType == ProjectileCollisionType.BounceOnFloor) {

				if(dir == DirCardinal.Down) {
					projectile.CollidePosDown(gridY * (byte)TilemapEnum.TileHeight - projectile.bounds.Bottom);
					projectile.BounceOnGround();
					return true;
				}

				projectile.Destroy(dir);
				return true;
			}

			// Some Projectiles can break boxes.
			if(projectile.CollisionType == ProjectileCollisionType.BreakObjects) {
				// TODO HIGH PRIORITY: Once we get breakable objects (instance tiles), we need to reapply here.
				// Could set this collision directly on Box and Brick (or other breakable objects) rather than in TileProjectileImpact
				// if(object instanceof Box || object instanceof Brick) { object.BreakApart(); }
			}

			return false;
		}
	}
}
