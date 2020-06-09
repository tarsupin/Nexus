using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

// NOTE: ProjectileBalls can interact with toggles.

namespace Nexus.Objects {

	public enum ProjectileBallSubType : byte {
		Electric,
		Fire,
		Frost,
		Rock,
		Slime,
		Water,
	}

	public class ProjectileBall : Projectile {

		public ProjectileBall(RoomScene room, byte subType, FVector pos, FVector velocity) : base(room, subType, pos, velocity) {}

		public static ProjectileBall Create(RoomScene room, byte subType, FVector pos, FVector velocity) {
			ProjectileBall projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ProjectilePool.ProjectileBall.Count > 0) {
				projectile = ProjectilePool.ProjectileBall.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}
			
			// Create a New Projectile Ball
			else {
				projectile = new ProjectileBall(room, subType, pos, velocity);
			}

			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		// Return the ProjectileBall to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			// TODO LOW PRIORITY: Is there any possible chance this could cause a ball to return to pool on same frame it's marked for delete?
			ProjectilePool.ProjectileBall.Push(this);
		}

		private void AssignSubType(byte subType) {

			// Behaviors
			if(subType == (byte) ProjectileBallSubType.Fire || subType == (byte) ProjectileBallSubType.Electric) {
				this.Damage = DamageStrength.Trivial;
				this.physics.SetGravity(FInt.Create(0.35));
				this.CollisionType = ProjectileCollisionType.BounceOnFloor;
			}

			else if(subType == (byte) ProjectileBallSubType.Rock) {
				this.Damage = DamageStrength.Lethal;
				this.physics.SetGravity(FInt.Create(0.8));
				this.CollisionType = ProjectileCollisionType.Special;
			}

			else {
				this.Damage = DamageStrength.Trivial;
				this.physics.SetGravity(FInt.Create(0.4));
				this.CollisionType = ProjectileCollisionType.DestroyOnCollide;
			}

			// Sprite Image
			if(subType == (byte)ProjectileBallSubType.Fire) {
				this.SetSpriteName("Projectiles/Fire");
			} else if(subType == (byte)ProjectileBallSubType.Electric) {
				this.SetSpriteName("Projectiles/Electric");
			} else if(subType == (byte)ProjectileBallSubType.Frost) {
				this.SetSpriteName("Projectiles/Frost");
			} else if(subType == (byte)ProjectileBallSubType.Rock) {
				this.SetSpriteName("Projectiles/Earth1");
			} else if(subType == (byte)ProjectileBallSubType.Slime) {
				this.SetSpriteName("Projectiles/Slime");
			} else if(subType == (byte)ProjectileBallSubType.Water) {
				this.SetSpriteName("Projectiles/Water");
			}
		}
	}
}
