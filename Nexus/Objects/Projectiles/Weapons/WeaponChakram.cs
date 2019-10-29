using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponChakramSubType : byte {
		Chakram
	}

	public class WeaponChakram : Projectile {

		public WeaponChakram(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity) {
			this.AssignSubType(subType);
			this.Damage = DamageStrength.Major;
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;

			// TODO PHYSICS:
			// TODO RENDER: Need to draw render rotation for projectile:
			//this.physics.update = moveThrustHor;
			//this.render = this.renderBallRotation;
		}

		// Prevent collision destruction of Weapon; it can go through multiple objects.
		public override void Destroy( ) {}

		private void AssignSubType(byte subType) {
			if(subType == (byte) WeaponChakramSubType.Chakram) {
				this.SetSpriteName("Weapon/Chakram");
			}
		}
	}
}
