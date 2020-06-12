using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public class DaggerProjectile : ThrustProjectile {

		public DaggerProjectile() : base() { }

		public static DaggerProjectile Create(RoomScene room, byte subType, FVector pos, FVector endPos) {

			// Retrieve an available projectile from the pool.
			DaggerProjectile projectile = ProjectilePool.DaggerProjectile.GetObject();

			projectile.ResetProjectile(room, subType, pos, FVector.Create(0, 0));
			projectile.ResetThrustProjectile(endPos);
			projectile.SetSpriteName("Weapon/Dagger");
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);
			projectile.cycleDuration = 16;
			projectile.rotation = projectile.endPos.X > projectile.posX ? 0 : Radians.Rotate180;

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
