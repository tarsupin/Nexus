using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public enum ProjectileBulletSubType : byte {
		Bullet
	}

	public class ProjectileBullet : Projectile {

		public ProjectileBullet(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity, "Bullet") {
			this.AssignSubType(subType);
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
			this.SafelyJumpOnTop = true;
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) ProjectileBulletSubType.Bullet) {
				this.SpriteName = "Projectiles/Bullet";
			}
		}
	}
}
