using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponAxeSubType : byte {
		Axe,
		Axe2,
		Axe3
	}

	public class WeaponAxe : Projectile {

		public WeaponAxe(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity) {
			this.AssignSubType(subType);
			this.Damage = DamageStrength.Lethal;
			this.physics.SetGravity(FInt.Create(0.45));
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;

			// TODO PHYSICS:
			// this.physics.update = ballMovement;
			// TODO RENDER: Need to draw render rotation for projectile:
			// this.render = this.renderBallRotation;		// still how I want to do this? maybe? or override Draw()?
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) WeaponAxeSubType.Axe) {
				this.SetSpriteName("Weapon/Axe");
			}

			else if(subType == (byte) WeaponAxeSubType.Axe2) {
				this.SetSpriteName("Weapon/Axe2");
			}

			else if(subType == (byte) WeaponAxeSubType.Axe3) {
				this.SetSpriteName("Weapon/Axe3");
			}
		}
	}
}
