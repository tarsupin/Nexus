using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponGrenadeSubType : byte {
		Grenade
	}

	public class GrenadeProjectile : Projectile {

		private GrenadeProjectile(RoomScene room, byte subType, FVector pos, FVector velocity) : base(room, subType, pos, velocity) {
			this.Damage = DamageStrength.None;
			this.CollisionType = ProjectileCollisionType.DestroyOnCollide;
			this.physics.SetGravity(FInt.Create(0.4));
		}

		public static GrenadeProjectile Create(RoomScene room, byte subType, FVector pos, FVector velocity) {
			GrenadeProjectile projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ObjectPool.WeaponGrenade.Count > 0) {
				projectile = ObjectPool.WeaponGrenade.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}

			// Create a New Projectile Ball
			else {
				projectile = new GrenadeProjectile(room, subType, pos, velocity);
			}

			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) WeaponGrenadeSubType.Grenade) {
				this.SetSpriteName("Weapon/Grenade");
			}
		}
	}
}
