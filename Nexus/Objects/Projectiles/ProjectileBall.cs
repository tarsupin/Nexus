using Nexus.Config;
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
		Poison,
		Water,
	}

	public class ProjectileBall : Projectile {

		public ProjectileBall() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) {}

		public static ProjectileBall Create(RoomScene room, byte subType, FVector pos, FVector velocity) {

			// Retrieve an available projectile from the pool.
			ProjectileBall projectile = ProjectilePool.ProjectileBall.GetObject();

			projectile.ResetProjectile(room, subType, pos, velocity);
			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		public override void BounceOnGround() {
			this.physics.velocity.Y = this.subType == (byte) ProjectileBallSubType.Fire ? FInt.Create(-6) : FInt.Create(-8);
		}

		private void AssignSubType(byte subType) {

			// Behaviors
			if(subType == (byte) ProjectileBallSubType.Fire || subType == (byte) ProjectileBallSubType.Electric) {
				this.Damage = DamageStrength.Standard;
				this.spinRate = this.physics.velocity.X > 0 ? 0.05f : -0.05f;
				this.physics.SetGravity(FInt.Create(0.45));
				this.CollisionType = ProjectileCollisionType.BounceOnFloor;
			}

			else if(subType == (byte) ProjectileBallSubType.Rock) {
				this.Damage = DamageStrength.Lethal;
				this.physics.SetGravity(FInt.Create(0.8));
				this.CollisionType = ProjectileCollisionType.Special;
			}

			else {
				this.Damage = DamageStrength.Standard;
				this.spinRate = this.physics.velocity.X > 0 ? 0.09f : -0.09f;
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
			} else if(subType == (byte)ProjectileBallSubType.Poison) {
				this.SetSpriteName("Projectiles/Poison");
			} else if(subType == (byte)ProjectileBallSubType.Water) {
				this.SetSpriteName("Projectiles/Water");
			}
		}

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.ProjectileBall.ReturnObject(this);
		}
	}
}
