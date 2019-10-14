using Nexus.Objects;

namespace Nexus.GameEngine {

	public class HitCompareProjectile : IHitCompare {

		public virtual bool RunImpact( DynamicGameObject projectile, DynamicGameObject obj ) {

			// TODO: ALL OF THIS HAS TO BE BUILT. NEED DYNAMIC OBJECTS IN PLACE FIRST.
			// TODO: ALL OF THIS HAS TO BE BUILT. NEED DYNAMIC OBJECTS IN PLACE FIRST.

			// Specific Impact Types
			if(obj is Projectile) { return false; }

			Projectile proj = (Projectile) projectile;

			// Some projectiles get destroyed on collision.
			if(proj.CollisionType == ProjectileCollisionType.DestroyOnCollide) {
				proj.Destroy();
			}

			// Some projectiles bounce.
			else if(proj.CollisionType == ProjectileCollisionType.BounceOnFloor) {

				// TODO: Could we just use standard collision here?
				//if(dir === DirCardinal.Down) {
				//	projectile.collision.alignVert(projectile.pos.y, projectile.bounds, -1);
				//	projectile.physics.velocity.y = -(projectile as any).bounceStrength;
				//	return false;
				//}

				proj.Destroy();
			}

			// Some projectiles can break objects (like boxes).
			else if(proj.CollisionType == ProjectileCollisionType.BreakObjects) {
				if(obj is Block) { this.ProjectileBreakBlock(proj, (Block) obj); }
			}

			// Some projectiles have special behaviors.
			else if(proj.CollisionType == ProjectileCollisionType.Special) {
				proj.Destroy();
			}

			// Standard Collision
			return true;
		}

		public bool ProjectileBreakBlock(Projectile projectile, Block block) {

			// TODO HIGH PRIORITY: Break block test if box or brick (these are tiles, does this even work here? called directly from tile?)
			//if(block is Box || block is Brick) {
			//object.breakapart();
			//}

			return true;
		}
	}
}
