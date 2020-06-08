using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public class ProjectileBullet : Projectile {

		private ProjectileBullet(RoomScene room, byte subType, FVector pos, FVector velocity) : base(room, subType, pos, velocity) {
			this.SetActivity(Activity.NoTileCollide);
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
			this.SafelyJumpOnTop = true;
		}

		public static ProjectileBullet Create(RoomScene room, byte subType, FVector pos, FVector velocity) {
			ProjectileBullet projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ObjectPool.ProjectileBullet.Count > 0) {
				projectile = ObjectPool.ProjectileBullet.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}

			// Create a New Projectile Ball
			else {
				projectile = new ProjectileBullet(room, subType, pos, velocity);
			}

			projectile.SetEndLife(Systems.timer.Frame + 620); // TEMPORARY, REMOVE

			projectile.SetSpriteName("Projectiles/Bullet");
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}
	}
}
