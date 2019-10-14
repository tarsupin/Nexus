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
				this.physics.SetGravity(FInt.FromParts(0, 350));
				this.CollisionType = ProjectileCollisionType.BounceOnFloor;
			} else {
				this.physics.SetGravity(FInt.FromParts(0, 400));
				this.CollisionType = ProjectileCollisionType.DestroyOnCollide;
			}

			// Sprite Image
			if(subType == (byte) ProjectileBallSubType.Fire) {
				this.SpriteName = "Projectiles/Fire";
			} else if(subType == (byte) ProjectileBallSubType.Electric) {
				this.SpriteName = "Projectiles/Electric";
			} else if(subType == (byte) ProjectileBallSubType.Frost) {
				this.SpriteName = "Projectiles/Frost";
			} else if(subType == (byte) ProjectileBallSubType.Slime) {
				this.SpriteName = "Projectiles/Slime";
			} else if(subType == (byte) ProjectileBallSubType.Water) {
				this.SpriteName = "Projectiles/Water";
			}
		}
	}
}
