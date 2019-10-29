using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponShurikenSubType : byte {
		Shuriken
	}

	public class WeaponShuriken : Projectile {

		public WeaponShuriken(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity) {
			this.AssignSubType(subType);
			this.Damage = DamageStrength.Standard;
			this.CollisionType = ProjectileCollisionType.DestroyOnCollide;
			this.physics.SetGravity(FInt.Create(0.4));

			// TODO HIGH PRIORITY: Weapon Shuriken (( see full code; has more ))
			// TODO HIGH PRIORITY: Weapon Shuriken (( see full code; has more ))

			// this.elapsedStart = 0;

			// TODO PHYSICS:
			// TODO RENDER: Need to draw render rotation for projectile:
			//this.physics.update = shurikenMovement;
			//this.render = this.renderBallRotation;
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) WeaponShurikenSubType.Shuriken) {
				this.SetSpriteName("Weapon/Shuriken");
			}
		}
	}
}
