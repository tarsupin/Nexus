using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public enum ProjectileBoltSubType : byte {
		Blue,
		Green,
		Gold,
	}

	public class ProjectileBolt : Projectile {

		public ProjectileBolt(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity) {
			this.AssignSubType(subType);

			// TODO RENDER: Need to draw render rotation for projectile:
			// this.physics.update = ballMovement;		// Probably override RunTick()
			// this.render = this.renderRotation;		// still how I want to do this? maybe? or override Draw()? 
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

				// TODO RENDER: Need to draw render rotation for projectile:
				// NOTE: See how this.physics.update is different than others? Maybe use a behavior? Or an action? Or something else? RunTick?
				// this.physics.update = BoltGreenSwimMovement;
				// this.render = this.renderRotation;		// still how I want to do this? maybe? or override Draw()?
				this.SetSpriteName("Projectiles/BoltGreen");
				this.CollisionType = ProjectileCollisionType.IgnoreWalls;

			} else if(subType == (byte) ProjectileBoltSubType.Gold) {
				this.SetSpriteName("Projectiles/BoltGold");
				this.CollisionType = ProjectileCollisionType.IgnoreWalls;
			}
		}
	}
}
