using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public enum ProjectileBoltSubType : byte {
		Blue,
		Green,
		Gold,
		Necro,
	}

	public class ProjectileBolt : Projectile {

		public ProjectileBolt() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) { }

		public static ProjectileBolt Create(RoomScene room, byte subType, FVector pos, FVector velocity) {

			// Retrieve an available projectile from the pool.
			ProjectileBolt projectile = ProjectilePool.ProjectileBolt.GetObject();

			projectile.ResetProjectile(room, subType, pos, velocity);
			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			projectile.rotation = Radians.GetRadiansBetweenCoords(0, 0, velocity.X.RoundInt, velocity.Y.RoundInt);

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		private void AssignSubType(byte subType) {

			if(subType == (byte) ProjectileBoltSubType.Blue) {
				this.SetSpriteName("Projectiles/Bolt");
				this.SetCollisionType(ProjectileCollisionType.DestroyOnCollide);

			} else if(subType == (byte) ProjectileBoltSubType.Green) {
				this.SetSpriteName("Projectiles/BoltGreen");
				this.SetCollisionType(ProjectileCollisionType.IgnoreWalls);

			} else if(subType == (byte) ProjectileBoltSubType.Gold) {
				this.SetSpriteName("Projectiles/BoltGold");
				this.SetCollisionType(ProjectileCollisionType.IgnoreWalls);
			}
		}

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.ProjectileBolt.ReturnObject(this);
		}
	}
}
