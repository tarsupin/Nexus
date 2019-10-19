using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponGloveSubType : byte {
		Red,
		White
	}

	public class WeaponGlove : Projectile {

		public WeaponGlove(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity, "Glove") {
			this.AssignSubType(subType);
			this.Damage = DamageStrength.Standard;
			this.CollisionType = ProjectileCollisionType.BreakObjects;

			// Reduce Bounds (otherwise it appears to hit too much, too quickly)
			this.AssignBoundsByAtlas(5, 5, -5, -5);

			// TODO PHYSICS:
			// TODO RENDER: Need to draw render rotation for projectile:
			//this.physics.update = moveThrustHor;
			//this.render = this.renderRotation;
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
