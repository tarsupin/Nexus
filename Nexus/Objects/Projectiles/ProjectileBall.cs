using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

// NOTE: ProjectileBalls can interact with toggles.

namespace Nexus.Objects {

	public enum ProjectileBallSubType : byte {
		Electric,
		Fire,
		Frost,
		Slime,
		Water
	}

	public class ProjectileBall : Projectile {

		public ProjectileBall(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity, "Ball") {
			this.AssignSubType(subType);
			this.Damage = DamageStrength.Trivial;
			
			// TODO RENDER: Need to draw render rotation for projectile:
			// this.render = this.renderBallRotation;		// still how I want to do this? maybe? or override Draw()?
		}

		private void AssignSubType(byte subType) {

			// Behaviors
			if(subType == (byte) ProjectileBallSubType.Fire || subType == (byte) ProjectileBallSubType.Electric) {
				this.physics.SetGravity(FInt.Create(0.35));
				this.CollisionType = ProjectileCollisionType.BounceOnFloor;
			} else {
				this.physics.SetGravity(FInt.Create(0.4));
				this.CollisionType = ProjectileCollisionType.DestroyOnCollide;
			}

			// Sprite Image
			if(subType == (byte) ProjectileBallSubType.Fire) {
				this.SetSpriteName("Projectiles/Fire");
			} else if(subType == (byte) ProjectileBallSubType.Electric) {
				this.SetSpriteName("Projectiles/Electric");
			} else if(subType == (byte) ProjectileBallSubType.Frost) {
				this.SetSpriteName("Projectiles/Frost");
			} else if(subType == (byte) ProjectileBallSubType.Slime) {
				this.SetSpriteName("Projectiles/Slime");
			} else if(subType == (byte) ProjectileBallSubType.Water) {
				this.SetSpriteName("Projectiles/Water");
			}
		}
	}
}
