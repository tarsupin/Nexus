using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;

namespace Nexus.ObjectComponents {

	public class Physics {

		// Object Reference
		protected DynamicObject actor;

		// Physics Values
		public FVector physPos;         // The "physics" tracks "true" positions with Fixed-Point math, but is separate from "position" on the Game Object, which uses ints.

		// Detection
		public int lastPosX;
		public int lastPosY;
		public Touch touch;

		protected bool hasExtraMovement;

		// Movements
		public FVector intend;          // The X, Y the actor intends to move during its frame. A combination of gravity + velocity + extraMovement. Once set for the frame, don't change it.
		public FVector velocity;
		public FVector extraMovement;   // Added values, such as for conveyor belts, platforms, etc.
		public FInt gravity;

		public Physics(DynamicObject actor) {
			this.actor = actor;

			this.physPos = FVector.Create(this.actor.posX, this.actor.posY);

			this.lastPosX = this.actor.posX;
			this.lastPosY = this.actor.posY;

			this.velocity = new FVector();
			this.extraMovement = new FVector();
			this.gravity = new FInt();
			this.hasExtraMovement = false;
			this.touch = new Touch();
		}

		// Get Amount Moved
		public int AmountMovedX { get { return this.actor.posX - this.lastPosX; } }
		public int AmountMovedY { get { return this.actor.posY - this.lastPosY; } }

		public void SetGravity(FInt gravity) {
			this.gravity = gravity;
		}

		// Run this method BEFORE any collisions take place. The .result value will change from collisions.
		public void RunPhysicsTick() {
			this.touch.ResetTouch();

			// Apply Gravity to Velocity
			this.velocity.Y += this.gravity;

			// Determine what the intended movement is for this frame.
			this.intend = this.velocity;

			if(hasExtraMovement) {
				this.intend = FVector.VectorAdd(this.intend, this.extraMovement);
			}

			this.TrackPhysicsTick();
		}

		public void TrackPhysicsTick() {

			// Update Positions
			this.physPos = FVector.VectorAdd(this.physPos, this.velocity);

			// Extra Movement (such as caused by Platforms or Conveyors)
			if(hasExtraMovement) {
				this.physPos = FVector.VectorAdd(this.physPos, this.extraMovement);
				this.extraMovement = new FVector();
			}

			this.UpdatePosX();
			this.UpdatePosY();
		}

		public void UpdatePosX() {
			this.lastPosX = this.actor.posX;
			this.actor.posX = this.physPos.X.RoundInt;
		}

		public void UpdatePosY() {
			this.lastPosY = this.actor.posY;
			this.actor.posY = this.physPos.Y.RoundInt;
		}

		public void MoveToPos(int posX, int posY) {
			this.physPos.X = FInt.Create(posX);
			this.physPos.Y = FInt.Create(posY);
			this.UpdatePosX();
			this.UpdatePosY();
		}

		public void MoveToPosX(int posX) {
			this.physPos.X = FInt.Create(posX);
			this.UpdatePosX();
		}

		public void MoveToPosY(int posY) {
			this.physPos.Y = FInt.Create(posY);
			this.UpdatePosY();
		}

		public void StopX() {
			this.velocity.X = FInt.Create(0);
		}

		public void StopY() {
			this.velocity.Y = FInt.Create(0);
		}

		// --- Return TRUE if crossed a threshold. Used for checking if a new grid square was entered. --- //
		public bool CrossedThresholdUp(int posY) { int top = this.actor.posY + this.actor.bounds.Top; return top <= posY && top - this.intend.Y.RoundInt >= posY; }
		public bool CrossedThresholdDown(int posY) { int bottom = this.actor.posY + this.actor.bounds.Bottom; return bottom >= posY && bottom - this.intend.Y.RoundInt <= posY; }
		public bool CrossedThresholdLeft(int posX) { int left = this.actor.posX + this.actor.bounds.Left; return left <= posX && left - this.intend.X.RoundInt >= posX; }
		public bool CrossedThresholdRight(int posX) { int right = this.actor.posX + this.actor.bounds.Right; return right >= posX && right - this.intend.X.RoundInt <= posX; }

		// --- Return the distance of how far a threshold was crossed. -- //
		public int CrossedThresholdUpDist(int posY) { return this.actor.posY + this.actor.bounds.Top - posY; }
		public int CrossedThresholdDownDist(int posY) { return this.actor.posY + this.actor.bounds.Bottom - posY; }
		public int CrossedThresholdLeftDist(int posX) { return this.actor.posX + this.actor.bounds.Left - posX; }
		public int CrossedThresholdRightDist(int posX) { return this.actor.posX + this.actor.bounds.Right - posX; }
	}
}