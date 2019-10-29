using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponGrenadeSubType : byte {
		Grenade
	}

	public class WeaponGrenade : Projectile {

		public WeaponGrenade(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity) {
			this.AssignSubType(subType);
			this.Damage = DamageStrength.None;
			this.CollisionType = ProjectileCollisionType.DestroyOnCollide;
			this.physics.SetGravity(FInt.Create(0.4));

			// TODO HIGH PRIORITY: Weapon Grenade (( see full code; has more ))
			// TODO HIGH PRIORITY: Weapon Grenade (( see full code; has more ))

			// this.elapsedStart = 0;

			// TODO PHYSICS:
			// TODO RENDER: Need to draw render rotation for projectile:
			//this.physics.update = GrenadeMovement;
			//this.render = this.renderBallRotation;
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) WeaponGrenadeSubType.Grenade) {
				this.SetSpriteName("Weapon/Grenade");
			}
		}
	}
}
