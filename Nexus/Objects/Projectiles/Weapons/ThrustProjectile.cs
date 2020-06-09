using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ThrustProjectile : Projectile {

		protected uint startFrame;
		protected FVector endPos;

		public ThrustProjectile(RoomScene room, byte subType, FVector pos, FVector endPos, uint startFrame, uint endFrame) : base(room, subType, pos, FVector.Create(0, 0)) {
			this.Damage = DamageStrength.Major;
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
			this.endPos = endPos;
			this.ResetThrustProjectile(startFrame, endFrame);
		}

		public void ResetThrustProjectile(uint startFrame, uint endFrame) {
			this.startFrame = startFrame;
			this.SetEndLife(endFrame);
		}

		public override void RunTick() {

			// If the Projectile's life has expired.
			if(this.EndLife < Systems.timer.Frame) {
				this.ReturnToPool();
				return;
			}

			// Standard Physics
			this.physics.RunPhysicsTick();
		}

		//public void ThrustToHorizontal(): void {
		//	let power = this.obj.power;
		//	this.trackPhysics( time.dt );
			
		//	// Identify Position based on Global Timing
		//	const elapse = time.elapsed - power.elapsedOffset;
		//	const weight = elapse / power.cycleDuration;
	
		//	// Replicates behavior of Calc.lerpVector2DEaseAbrupt
		//	let posX = power.startPos.x;
		//	posX = posX + Math.abs(Math.sin(weight * Math.PI)) * (power.endPos.x - posX),
	
		//	this.velocity.x = Math.round(posX - this.pos.x);
	
		//	// Weapon Strike End-Cycle
		//	if(elapse > power.cycleDuration / 2) { this.obj.disable(); }
		//}

		//public void ThrustReturn(): void {
		//	let power = this.obj.power;
		//	this.trackPhysics( time.dt );
	
		//	// Identify Position based on Global Timing
		//	const elapse = time.elapsed - power.elapsedOffset;
	
		//	let posX;
		//	let posY;
	
		//	// Weapon Strike End-Cycle
		//	if(elapse > power.cycleDuration / 2) {
		//		if(elapse > power.cycleDuration) { return this.obj.disable(); }
		//		posX = power.character.pos.x + power.offsetX;
		//		posY = power.character.pos.y + power.offsetY;
		//	} else {
		//		posX = power.startPos.x;
		//		posY = power.startPos.y;
		//	}
	
		//	const weight = elapse / power.cycleDuration;
	
		//	// Replicates behavior of Calc.lerpVector2DEaseAbrupt
		//	posX = posX + Math.abs(Math.sin(weight * Math.PI)) * (power.endPos.x - posX),
		//	posY = posY + Math.abs(Math.sin(weight * Math.PI)) * (power.endPos.y - posY)
	
		//	this.velocity.x = Math.round(posX - this.pos.x);
		//	this.velocity.y = Math.round(posY - this.pos.y);
		//}


		// Prevent collision destruction of Weapon; it can go through multiple objects.
		// It will ReturnToPool() when its life has expired.
		public override void Destroy(DirCardinal dir = DirCardinal.None, GameObject obj = null) { }
	}
}
