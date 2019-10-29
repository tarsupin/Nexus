using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public enum ProjectileBulletSubType : byte {
		Bullet
	}

	public class ProjectileBullet : Projectile {

		public ProjectileBullet(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity) {
			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 2, -2, -2);
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
			this.SafelyJumpOnTop = true;
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) ProjectileBulletSubType.Bullet) {
				this.SetSpriteName("Projectiles/Bullet");
			}
		}
	}
}
