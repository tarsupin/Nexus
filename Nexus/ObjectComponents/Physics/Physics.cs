using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.ObjectComponents {

	public class Physics {

		// Object Reference
		protected GameObject actor;

		// Physics Values
		public FVector physPos;         // The "physics" tracks "true" positions with Fixed-Point math, but is separate from "position" on the Game Object, which uses ints.

		// Detection
		public int lastPosX;
		public int lastPosY;
		public Touch touch;

		public bool hasExtraMovement;

		// Movements
		public FVector intend;          // The X, Y the actor intends to move during its frame. A combination of gravity + velocity + extraMovement. Once set for the frame, don't change it.
		public FVector velocity;
		public FVector extraMovement;   // Added values, such as for conveyor belts, platforms, etc.
		public FInt gravity;

		public Physics(GameObject actor) {
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

		public void AddExtraMovement(int x, int y) {
			this.hasExtraMovement = true;
			this.extraMovement.X += x;
			this.extraMovement.Y += y;
		}

		public void SetExtraMovement(int x, int y) {
			this.hasExtraMovement = true;
			this.extraMovement.X = FInt.Create(x);
			this.extraMovement.Y = FInt.Create(y);
		}

		public void ClearExtraMovement() {
			this.hasExtraMovement = false;
			this.extraMovement.X = FInt.Create(0);
			this.extraMovement.Y = FInt.Create(0);
			this.touch.onMover = false;
			this.touch.moveObj = null;
		}

		// Run this method BEFORE any collisions take place. The .result value will change from collisions.
		public void RunPhysicsTick() {
			this.touch.ResetTouch();
			
			if(this.touch.onMover) {
				this.touch.ProcessMover(this);
			}

			// Update Last Positions
			this.lastPosX = this.actor.posX;
			this.lastPosY = this.actor.posY;

			// Apply Gravity to Velocity
			this.velocity.Y += this.gravity;

			// Determine what the intended movement is for this frame.
			this.intend = this.velocity;

			if(this.hasExtraMovement) {
				this.intend = FVector.VectorAdd(this.intend, this.extraMovement);
				this.extraMovement = new FVector();
				this.hasExtraMovement = false;
			}

			this.TrackPhysicsTick();
		}

		public void TrackPhysicsTick() {
			this.physPos = FVector.VectorAdd(this.physPos, this.intend);
			this.actor.posX = this.physPos.X.RoundInt;
			this.actor.posY = this.physPos.Y.RoundInt;
		}

		// --- Move Actor --- //
		public void MoveToPos(int posX, int posY) {
			this.physPos.X = FInt.Create(posX);
			this.physPos.Y = FInt.Create(posY);
			this.actor.posX = posX;
			this.actor.posY = posY;
		}

		public void MoveToPosX(int posX) {
			this.physPos.X = FInt.Create(posX);
			this.actor.posX = posX;
		}

		public void MoveToPosY(int posY) {
			this.physPos.Y = FInt.Create(posY);
			this.actor.posY = posY;
		}

		// --- Align Relative to Object --- //
		public void AlignLeft(GameObject obj) {
			this.actor.posX = obj.posX + obj.bounds.Left - this.actor.bounds.Right;
			this.physPos.X = FInt.Create(this.actor.posX);
		}

		public void AlignRight(GameObject obj) {
			this.actor.posX = obj.posX + obj.bounds.Right - this.actor.bounds.Left;
			this.physPos.X = FInt.Create(this.actor.posX);
		}

		public void AlignUp(GameObject obj) {
			this.actor.posY = obj.posY + obj.bounds.Top - this.actor.bounds.Bottom;
			this.physPos.Y = FInt.Create(this.actor.posY);
		}

		public void AlignDown(GameObject obj) {
			this.actor.posY = obj.posY + obj.bounds.Bottom - this.actor.bounds.Top;
			this.physPos.Y = FInt.Create(this.actor.posY);
		}

		// --- Stop Velocity --- //
		public void StopX() {
			this.velocity.X = FInt.Create(0);
			this.intend.X = FInt.Create(0);
		}

		public void StopY() {
			this.velocity.Y = FInt.Create(0);
			this.intend.Y = FInt.Create(0);
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