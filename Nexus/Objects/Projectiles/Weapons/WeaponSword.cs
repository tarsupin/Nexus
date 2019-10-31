using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponSwordSubType : byte {
		Sword
	}

	public class WeaponSword : Projectile {

		private WeaponSword(RoomScene room, byte subType, FVector pos, FVector velocity) : base(room, subType, pos, velocity) {
			this.Damage = DamageStrength.Major;
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
		}

		public static WeaponSword Create(RoomScene room, byte subType, FVector pos, FVector velocity) {
			WeaponSword projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ObjectPool.WeaponSword.Count > 0) {
				projectile = ObjectPool.WeaponSword.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}

			// Create a New Projectile Ball
			else {
				projectile = new WeaponSword(room, subType, pos, velocity);
			}

			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		// Prevent collision destruction of Weapon; it can go through multiple objects.
		public override void Destroy( ) {}

		private void AssignSubType(byte subType) {
			if(subType == (byte) WeaponSwordSubType.Sword) {
				this.SetSpriteName("Weapon/Sword");
			}
		}
	}
}
