using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public class SpearProjectile : ThrustProjectile {

		public SpearProjectile() : base() {
			this.cycleDuration = 24;
			this.SetSpriteName("Weapon/Spear");
			this.AssignBoundsByAtlas(2, 2, -2, -2);
		}

		public static SpearProjectile Create(RoomScene room, byte subType, FVector pos, FVector endPos) {

			// Retrieve an available projectile from the pool.
			SpearProjectile projectile = ProjectilePool.SpearProjectile.GetObject();

			projectile.ResetProjectile(room, subType, pos, FVector.Create(0, 0));
			projectile.ResetThrustProjectile(endPos);
			projectile.rotation = projectile.endPos.X > projectile.posX ? 0 : Radians.Rotate180;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.SpearProjectile.ReturnObject(this);
		}
	}
}
