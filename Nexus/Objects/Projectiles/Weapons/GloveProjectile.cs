using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponGloveSubType : byte {
		Red,
		White
	}

	public class GloveProjectile : ThrustProjectile {

		public GloveProjectile() : base() {
			this.cycleDuration = 24;
			this.CollisionType = ProjectileCollisionType.BreakObjects;
			this.Damage = DamageStrength.Standard;
		}

		public static GloveProjectile Create(RoomScene room, byte subType, FVector pos, FVector endPos) {

			// Retrieve an available projectile from the pool.
			GloveProjectile projectile = ProjectilePool.GloveProjectile.GetObject();

			projectile.ResetProjectile(room, subType, pos, FVector.Create(0, 0));
			projectile.ResetThrustProjectile(endPos);
			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(5, 5, -5, -5); // Reduce Bounds (otherwise it appears to hit too much, too quickly)
			projectile.rotation = projectile.endPos.X > projectile.posX ? 0 : Radians.Rotate180;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) WeaponGloveSubType.Red) {
				this.SetSpriteName("Weapon/BoxingRed");
			} else if(subType == (byte) WeaponGloveSubType.White) {
				this.SetSpriteName("Weapon/BoxingWhite");
			}
		}

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.GloveProjectile.ReturnObject(this);
		}
	}
}
