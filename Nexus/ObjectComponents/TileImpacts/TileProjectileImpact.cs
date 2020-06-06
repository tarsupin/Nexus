using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public static class TileProjectileImpact {

		// A Standard Tile Impact just triggers the collision methods that are commonly overridden (collideLeft(), collideRight(), etc).
		public static bool RunImpact(Projectile projectile, ushort gridX, ushort gridY, DirCardinal dir = DirCardinal.None) {

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

			// Some Projectiles have special behaviors (which currently also just activate the Destroy() method).
			if(projectile.CollisionType == ProjectileCollisionType.Special) {
				// TODO HIGH PRIORITY: Once we get instance tiles, we need to reapply here.
				// TODO HIGH PRIORITY: Should look like projectile.Destroy(dir, thisInstanceTile)
				// Could set this collision directly on Box and Brick (or other breakable objects) rather than in TileProjectileImpact
				projectile.Destroy(dir);
				return true;
			}

			return false;
		}
	}
}
