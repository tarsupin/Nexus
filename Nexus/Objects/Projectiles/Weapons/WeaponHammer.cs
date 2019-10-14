using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponHammerSubType : byte {
		Hammer
	}

	public class WeaponHammer : Projectile {

		public WeaponHammer(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity, "Hammer") {
			this.AssignSubType(subType);
			this.Damage = DamageStrength.Major;
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
			this.physics.SetGravity(FInt.Create(0.45));

			// TODO PHYSICS:
			// TODO RENDER: Need to draw render rotation for projectile:
			//this.physics.update = ballMovement;
			//this.render = this.renderBallRotation;
		}

		// Prevent collision destruction of Weapon; it can go through multiple objects.
		public override void Destroy( ) {}

		private void AssignSubType(byte subType) {
			if(subType == (byte) WeaponHammerSubType.Hammer) {
				this.SpriteName = "Weapon/Hammer";
			}
		}
	}
}
