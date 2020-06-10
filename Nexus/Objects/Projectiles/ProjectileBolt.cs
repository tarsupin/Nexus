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

		private ProjectileBolt(RoomScene room, byte subType, FVector pos, FVector velocity) : base(room, subType, pos, velocity) {

		}

		// TODO: Need to render directional rotation for Bolts:
		public static ProjectileBolt Create(RoomScene room, byte subType, FVector pos, FVector velocity) {
			ProjectileBolt projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ProjectilePool.ProjectileBolt.Count > 0) {
				projectile = ProjectilePool.ProjectileBolt.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}

			// Create a New Projectile Ball
			else {
				projectile = new ProjectileBolt(room, subType, pos, velocity);
			}

			// Rotation
			projectile.rotation = Radians.GetRadiansBetweenCoords(0, 0, velocity.X.RoundInt, velocity.Y.RoundInt);

			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		// TODO RENDER: UpdateRotation()
		//public void UpdateRotation() {
		//	this.rotRadian = Radians.getRadiansBetween(0, 0, this.physics.velocity.x, this.physics.velocity.y);
		//}


		// Instance Behavior (Green Bolt Only)
		//this.startY = this.pos.y;
		//this.elapsedOffset = scene.time.elapsed;
		// TODO UPDATE MOVEMENT (applies to Green Bolts only)
		//function BoltGreenSwimMovement( time: Timer ): void {
		//	let obj = this.obj;
		//	let vel = this.velocity;

		//	this.pos.y = Calc.lerpEaseBothDir( obj.startY + 50, obj.startY - 50, ((time.elapsed - obj.elapsedOffset) % 500) / 500 );

		//	vel.x = Calc.precisionRound(vel.x, 3);
		//	vel.y = Calc.precisionRound(vel.y, 3);

		//	this.trackPhysics( time.dt );
		//}

		private void AssignSubType(byte subType) {

			if(subType == (byte) ProjectileBoltSubType.Blue) {
				this.SetSpriteName("Projectiles/Bolt");
				this.CollisionType = ProjectileCollisionType.DestroyOnCollide;

			} else if(subType == (byte) ProjectileBoltSubType.Green) {
				this.SetSpriteName("Projectiles/BoltGreen");
				this.CollisionType = ProjectileCollisionType.IgnoreWalls;

			} else if(subType == (byte) ProjectileBoltSubType.Gold) {
				this.SetSpriteName("Projectiles/BoltGold");
				this.CollisionType = ProjectileCollisionType.IgnoreWalls;
			}
		}
	}
}
