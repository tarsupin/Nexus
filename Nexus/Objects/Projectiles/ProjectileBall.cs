using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

// NOTE: ProjectileBalls can interact with toggles.

namespace Nexus.Objects {

	public enum ProjectileBallSubType : byte {
		Electric,
		Fire,
		Frost,
		Slime,
		Water
	}

	public class ProjectileBall : Projectile {

		private ProjectileBall(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity) {
			
		}

		public static ProjectileBall Create(LevelScene scene, byte subType, FVector pos, FVector velocity) {
			ProjectileBall projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ObjectPool.ProjectileBall.Count > 0) {
				projectile = ObjectPool.ProjectileBall.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}
			
			// Create a New Projectile Ball
			else {
				projectile = new ProjectileBall(scene, subType, pos, velocity);
			}

			projectile.AssignSubType(subType);
			projectile.Damage = DamageStrength.Trivial;

			// Add the Projectile to Scene
			scene.AddToScene(projectile);

			return projectile;
		}

		// Return the ProjectileBall to the Pool
		public override void Disable() {
			this.scene.RemoveFromScene(this);
			// TODO LOW PRIORITY: Is there any possible chance this could cause a ball to return to pool on same frame it's marked for delete?
			ObjectPool.ProjectileBall.Push(this);
		}

		private void AssignSubType(byte subType) {

			// Behaviors
			if(subType == (byte) ProjectileBallSubType.Fire || subType == (byte) ProjectileBallSubType.Electric) {
				this.physics.SetGravity(FInt.Create(0.35));
				this.CollisionType = ProjectileCollisionType.BounceOnFloor;
			} else {
				this.physics.SetGravity(FInt.Create(0.4));
				this.CollisionType = ProjectileCollisionType.DestroyOnCollide;
			}

			// Sprite Image
			if(subType == (byte) ProjectileBallSubType.Fire) {
				this.SetSpriteName("Projectiles/Fire");
			} else if(subType == (byte) ProjectileBallSubType.Electric) {
				this.SetSpriteName("Projectiles/Electric");
			} else if(subType == (byte) ProjectileBallSubType.Frost) {
				this.SetSpriteName("Projectiles/Frost");
			} else if(subType == (byte) ProjectileBallSubType.Slime) {
				this.SetSpriteName("Projectiles/Slime");
			} else if(subType == (byte) ProjectileBallSubType.Water) {
				this.SetSpriteName("Projectiles/Water");
			}
		}
	}
}
