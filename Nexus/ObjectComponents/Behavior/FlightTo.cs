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
		protected FInt fallAccel;		// The acceleration at which the attached object will fall.

		public FlightTo(DynamicGameObject actor, Dictionary<string, short> paramList) : base(actor, paramList) {
			this.physics = actor.physics;

			this.countdown = paramList.ContainsKey("countdown") ? (byte) paramList["countdown"] : (byte) 0;
			this.startFrame = 0;
			this.endFrame = 0;

			this.isFalling = false;
			this.fallAccel = paramList.ContainsKey("fallAccel") ? FInt.Create((byte) paramList["fallAccel"]) : FInt.Create(0.25);

			// Not sure why startX is set like this. Setting to non-zero to indicate that it moves?
			this.startX = paramList.ContainsKey("countdown") ? paramList["countdown"] : (byte) 0;

			this.speedX = FInt.Create(Interpolation.Speed(this.endX - this.startX, this.duration) * 60);
			this.speedY = FInt.Create(Interpolation.Speed(this.endY - this.startY, this.duration) * 60);
		}

		public void BeginMovement() {
			this.startFrame = Systems.timer.Frame;

			if(this.countdown > 0) {
				this.endFrame = (uint) (this.startFrame + (this.countdown * 60));
			}
		}

		public override void RunTick() {

			// If the actor has begun falling (i.e. after the countdown concludes).
			if(this.isFalling) {
				if(this.physics.velocity.Y > 5) {
					this.physics.velocity.Y = FInt.Create(5);
				} else {
					this.physics.velocity.Y += this.fallAccel;
				}
			}

			// If the actor is moving normally:
			else if(this.startFrame > 0) {

				// Begin Movement
				if(this.startFrame == Systems.timer.Frame) {
					this.physics.velocity.X = this.speedX;
					this.physics.velocity.Y = this.speedY;
				}

				// If the countdown has ended, begin the falling process.
				if(this.endFrame > 0 && this.endFrame > Systems.timer.Frame) {
					this.isFalling = true;
				}
			}
		}
	}
}
