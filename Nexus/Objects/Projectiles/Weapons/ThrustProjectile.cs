using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;

namespace Nexus.Objects {

	public class ThrustProjectile : Projectile {
		
		protected byte cycleDuration; // The duration of the weapon's attack.
		protected uint startFrame;
		protected FVector endPos;

		public ThrustProjectile(RoomScene room, byte subType, FVector pos, FVector endPos) : base(room, subType, pos, FVector.Create(0, 0)) {
			this.Damage = DamageStrength.Major;
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
			this.endPos = endPos;
			this.ResetThrustProjectile();
		}

		public void ResetThrustProjectile() {
			this.startFrame = Systems.timer.Frame;
			this.SetEndLife(startFrame + this.cycleDuration);
		}

		public override void RunTick() {

			// If the Projectile's life has expired.
			// Expire a few frames early, to avoid the weapon hanging at the end.
			if(this.EndLife < Systems.timer.Frame + 5) {
				this.ReturnToPool();
				return;
			}

			// Run Thrust
			this.Thrust();

			// Standard Physics
			this.physics.RunPhysicsTick();
		}

		public void Thrust() {

			// If the Scene gets reset, this prevents posX from going negative (which would cause bugs).
			if(Systems.timer.Frame < this.startFrame) { return; }

			FInt elapsed = FInt.Create(Systems.timer.Frame - this.startFrame);
			var newX = FPInterpolation.EaseInAndOut(elapsed, FInt.Create(this.posX), this.endPos.X.RoundInt - FInt.Create(this.posX), FInt.Create(this.cycleDuration));
			this.physics.MoveToPosX(newX.RoundInt);

			// Non-FP Version
			//float elapsed = (float)(Systems.timer.Frame - this.startFrame);
			//float newX = Interpolation.EaseOut(elapsed, this.posX, (this.endPos.X.RoundInt - this.posX), this.cycleDuration);
			//this.physics.MoveToPosX((int)Math.Round(newX));
		}

		// Prevent collision destruction of Weapon; it can go through multiple objects.
		// It will ReturnToPool() when its life has expired.
		public override void Destroy(DirCardinal dir = DirCardinal.None, GameObject obj = null) { }
	}
}
