using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponGloveSubType : byte {
		Red,
		White
	}

	public class GloveProjectile : Projectile {

		private GloveProjectile(RoomScene room, byte subType, FVector pos, FVector velocity) : base(room, subType, pos, velocity) {
			this.Damage = DamageStrength.Standard;
			this.CollisionType = ProjectileCollisionType.BreakObjects;
		}

		public static GloveProjectile Create(RoomScene room, byte subType, FVector pos, FVector velocity) {
			GloveProjectile projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ProjectilePool.WeaponGlove.Count > 0) {
				projectile = ProjectilePool.WeaponGlove.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}

			// Create a New Projectile Ball
			else {
				projectile = new GloveProjectile(room, subType, pos, velocity);
			}

			projectile.AssignSubType(subType);

			// Reduce Bounds (otherwise it appears to hit too much, too quickly)
			projectile.AssignBoundsByAtlas(5, 5, -5, -5);

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		// Prevent collision destruction of Weapon; it can go through multiple objects.
		public override void Destroy( ) {}

		private void AssignSubType(byte subType) {
			if(subType == (byte) WeaponGloveSubType.Red) {
				this.SetSpriteName("Weapon/BoxingRed");
			} else if(subType == (byte) WeaponGloveSubType.White) {
				this.SetSpriteName("Weapon/BoxingWhite");
			}
		}
	}
}
