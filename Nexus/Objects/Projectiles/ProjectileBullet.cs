using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public class ProjectileBullet : Projectile {

		public ProjectileBullet() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) {
			this.SetActivity(Activity.NoTileCollide);
			this.SetCollisionType(ProjectileCollisionType.IgnoreWallsSurvive);
			this.SetSafelyJumpOnTop(true);
			this.SetSpriteName("Projectiles/Bullet");
		}

		public static ProjectileBullet Create(RoomScene room, byte subType, FVector pos, FVector velocity) {

			// Retrieve an available projectile from the pool.
			ProjectileBullet projectile = ProjectilePool.ProjectileBullet.GetObject();

			projectile.ResetProjectile(room, subType, pos, velocity);
			projectile.SetEndLife(Systems.timer.Frame + 720);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.ProjectileBullet.ReturnObject(this);
		}
	}
}
