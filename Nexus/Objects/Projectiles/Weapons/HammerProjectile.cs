using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class HammerProjectile : Projectile {

		public HammerProjectile() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) { }

		public static HammerProjectile Create(RoomScene room, byte subType, FVector pos, FVector velocity) {

			// Retrieve an available projectile from the pool.
			HammerProjectile projectile = ProjectilePool.HammerProjectile.GetObject();

			projectile.ResetProjectile(room, subType, pos, velocity);
			projectile.Damage = DamageStrength.Major;
			projectile.CollisionType = ProjectileCollisionType.IgnoreWalls;
			projectile.physics.SetGravity(FInt.Create(0.45));
			projectile.SetSpriteName("Weapon/Hammer");
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);
			projectile.rotation = Radians.UpRight + (projectile.physics.velocity.X > 0 ? 0.3f : -0.3f);
			projectile.spinRate = projectile.physics.velocity.X > 0 ? 0.10f : -0.10f;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.HammerProjectile.ReturnObject(this);
		}
	}
}
