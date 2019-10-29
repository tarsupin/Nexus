using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public enum ProjectileBulletSubType : byte {
		Bullet
	}

	public class ProjectileBullet : Projectile {

		private ProjectileBullet(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity) {
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
			this.SafelyJumpOnTop = true;
		}

		public static ProjectileBullet Create(LevelScene scene, byte subType, FVector pos, FVector velocity) {
			ProjectileBullet projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ObjectPool.ProjectileBullet.Count > 0) {
				projectile = ObjectPool.ProjectileBullet.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}

			// Create a New Projectile Ball
			else {
				projectile = new ProjectileBullet(scene, subType, pos, velocity);
			}

			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Add the Projectile to Scene
			scene.AddToScene(projectile, false);

			return projectile;
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) ProjectileBulletSubType.Bullet) {
				this.SetSpriteName("Projectiles/Bullet");
			}
		}
	}
}
