using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DaggerProjectile : Projectile {

		public DaggerProjectile() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) {
			this.CollisionType = ProjectileCollisionType.IgnoreWallsSurvive;
			this.Damage = DamageStrength.Standard;
			this.physics.SetGravity(FInt.Create(0));
			this.spinRate = 0f;
			this.SetSpriteName("Weapon/Dagger");
			this.AssignBoundsByAtlas(2, 2, -2, -2);
		}

		public static DaggerProjectile Create(RoomScene room, byte subType, FVector pos, FVector velocity) {

			// Retrieve an available projectile from the pool.
			DaggerProjectile projectile = ProjectilePool.DaggerProjectile.GetObject();

			projectile.ResetProjectile(room, subType, pos, velocity);
			projectile.AssignBoundsByAtlas(5, 5, -5, -5); // Reduce Bounds (otherwise it appears to hit too much, too quickly)
			projectile.rotation = projectile.physics.velocity.X > 0 ? 0 : Radians.Rotate180;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.DaggerProjectile.ReturnObject(this);
		}
	}
}
