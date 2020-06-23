using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum ProjectileEnemySubType : byte {
		Fire,
		Electric,
		Poison,
		Magic,
		BoltBlue,
	}

	public class ProjectileEnemy : Projectile {

		public ProjectileEnemy() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) {
			this.Damage = DamageStrength.Standard;
		}

		public static ProjectileEnemy Create(RoomScene room, byte subType, FVector pos, FVector velocity) {

			// Retrieve an available projectile from the pool.
			ProjectileEnemy projectile = ProjectilePool.ProjectileEnemy.GetObject();

			projectile.ResetProjectile(room, subType, pos, velocity);
			projectile.spinRate = projectile.physics.velocity.X > 0 ? 0.05f : -0.05f;
			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);
			projectile.physics.SetGravity(FInt.Create(0));

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		private void AssignSubType(byte subType) {

			if(subType == (byte) ProjectileEnemySubType.Fire) {
				this.SetSpriteName("Projectiles/Fire");
				this.SetCollisionType(ProjectileCollisionType.DestroyOnCollide);
			}

			else if(subType == (byte)ProjectileEnemySubType.Electric) {
				this.SetSpriteName("Projectiles/Electric");
				this.SetCollisionType(ProjectileCollisionType.DestroyOnCollide);
			}

			else if(subType == (byte) ProjectileEnemySubType.Poison) {
				this.SetSpriteName("Projectiles/Poison");
				this.SetCollisionType(ProjectileCollisionType.DestroyOnCollide);
			}

			else if(subType == (byte) ProjectileEnemySubType.Magic) {
				this.SetSpriteName("Projectiles/Magi");
				this.SetCollisionType(ProjectileCollisionType.IgnoreWallsSurvive);
			}

			else if(subType == (byte) ProjectileEnemySubType.BoltBlue) {
				this.SetSpriteName("Projectiles/Bolt");
				this.SetCollisionType(ProjectileCollisionType.IgnoreWallsDestroy);
				this.spinRate = 0;
			}
		}

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.ProjectileEnemy.ReturnObject(this);
		}
	}
}
