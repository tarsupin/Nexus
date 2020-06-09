using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponChakramSubType : byte {
		Chakram
	}

	public class ChakramProjectile : Projectile {

		private ChakramProjectile(RoomScene room, byte subType, FVector pos, FVector velocity) : base(room, subType, pos, velocity) {
			this.Damage = DamageStrength.Major;
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
		}

		public static ChakramProjectile Create(RoomScene room, byte subType, FVector pos, FVector velocity) {
			ChakramProjectile projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ProjectilePool.WeaponChakram.Count > 0) {
				projectile = ProjectilePool.WeaponChakram.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}

			// Create a New Projectile Ball
			else {
				projectile = new ChakramProjectile(room, subType, pos, velocity);
			}

			projectile.SetSpriteName("Weapon/Chakram");
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		// Prevent collision destruction of Weapon; it can go through multiple objects.
		public override void Destroy( ) {}
	}
}
