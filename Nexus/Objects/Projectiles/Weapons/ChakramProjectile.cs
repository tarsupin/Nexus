using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponChakramSubType : byte {
		Chakram
	}

	public class ChakramProjectile : Projectile {

		public ChakramProjectile() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) { }

		public static ChakramProjectile Create(RoomScene room, byte subType, FVector pos, FVector velocity) {

			// Retrieve an available projectile from the pool.
			ChakramProjectile projectile = ProjectilePool.ChakramProjectile.GetObject();

			projectile.ResetProjectile(room, subType, pos, velocity);
			projectile.Damage = DamageStrength.Major;
			projectile.CollisionType = ProjectileCollisionType.IgnoreWalls;
			projectile.SetSpriteName("Weapon/Chakram");
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		// Prevent collision destruction of Weapon; it can go through multiple objects.
		public override void Destroy( ) { }

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.ChakramProjectile.ReturnObject(this);
		}
	}
}
