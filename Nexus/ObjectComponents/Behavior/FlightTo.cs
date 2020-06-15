using Nexus.Engine;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.ObjectComponents {

	public class FlightTo : FlightBehavior {

		Physics physics;        // Reference to the actor's physics.

		protected readonly FInt speedX;
		protected readonly FInt speedY;

		public byte countdown;			// Number of seconds until the motion will collapse.
		public uint startFrame;			// The frame at which the motion begins (timer.frame).
		public uint endFrame;			// The frame at which the countdown concludes.

		protected bool isFalling;		// TRUE means the motion is falling (e.g. like falling platforms).

		public FlightTo(GameObject actor, Dictionary<string, short> paramList) : base(actor, paramList) {
			this.physics = actor.physics;

			this.startFrame = 0;
			this.endFrame = 0;
			this.speedX = FInt.Create(Interpolation.Speed(this.endX - this.startX, this.duration));
			this.speedY = FInt.Create(Interpolation.Speed(this.endY - this.startY, this.duration));

			this.isFalling = false;
			this.countdown = paramList.ContainsKey("countdown") ? (byte)paramList["countdown"] : (byte)0;
		}

		public void BeginMovement() {

			if(this.startFrame == 0) {
				this.startFrame = Systems.timer.Frame;
				this.physics.velocity.X = this.speedX;
				this.physics.velocity.Y = this.speedY;

				if(this.countdown != 0) {
					this.endFrame = (uint)(this.startFrame + (this.countdown * 60));
				}
			}
		}

		public override void RunTick() {

			// If the actor has begun falling (i.e. after the countdown concludes).
			if(this.isFalling) {
				if(this.physics.velocity.Y > 6) {
					this.physics.velocity.Y = FInt.Create(6);
				} else {
					this.physics.velocity.Y += FInt.Create(0.05);
					this.physics.velocity.X *= FInt.Create(0.99);
				}
			}

			// If the countdown has ended, begin the falling process.
			else if(this.endFrame != 0 && this.endFrame <= Systems.timer.Frame) {
				this.isFalling = true;
			}
		}
	}
}
