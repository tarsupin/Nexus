using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.ObjectComponents {

	public class Physics {

		// Object Reference
		protected DynamicObject actor;

		// Physics Values
		public FVector physPos;			// The "physics" tracks "true" positions with Fixed-Point math, but is separate from "position" on the Game Object, which uses ints.

		// Detection
		public int lastPosX;
		public int lastPosY;
		public Touch touch;

		protected bool hasExtraMovement;

		// Movements
		public FVector moved;           // The X, Y actually moved during its last frame, after its resistances were handled. Once set for the frame, don't change it.
		public FVector intend;          // The X, Y the actor intends to move during its frame. A combination of gravity + velocity + extraMovement. Once set for the frame, don't change it.
		public FVector result;			// The X, Y the actor will actually travel. This can change through the frame, if blocked by tile or object.
		public FVector velocity;
		public FVector extraMovement;	// Added values, such as for conveyor belts, platforms, etc.
		public FInt gravity;

		public Physics( DynamicObject actor ) {
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

		public void SetGravity( FInt gravity ) {
			this.gravity = gravity;
		}

		// Run this method BEFORE any collisions take place. The .result value will change from collisions.
		public void InitializePhysicsTick() {

			// Track what was moved last frame:
			this.moved = this.result;

			// Determine what the intended movement is for this frame.
			this.intend = FVector.VectorAdd(this.velocity, FVector.Create(FInt.Create(0), this.gravity));

			if(hasExtraMovement) {
				this.intend = FVector.VectorAdd(this.intend, this.extraMovement);
			}

			// Reset the result, matching the intention - it may change throughout the rest of the physics update.
			this.result = this.intend;
		}

		// Run this method AFTER collisions take place.
		// At this point, this.result may have changed from collisions. Update certain values accordingly.
		public void RunPhysicsTick() {

			// Extra Movement (such as caused by Platforms or Conveyors)
			if(hasExtraMovement) {
				this.physPos = FVector.VectorAdd(this.physPos, this.extraMovement);
				this.extraMovement = new FVector();
			}

			this.velocity.Y += this.gravity;
			this.touch.ResetTouch();
			this.TrackPhysicsTick();
		}

		public void ResetPhysicsValues() {
			if(hasExtraMovement) { this.extraMovement = new FVector(); }
			this.touch.ResetTouch();
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
			this.actor.posX = this.physPos.X.IntValue;
		}

		public void UpdatePosY() {
			this.lastPosY = this.actor.posY;
			this.actor.posY = this.physPos.Y.IntValue;
		}

		public void MoveToPos( FVector pos ) {
			this.physPos = pos;
			this.UpdatePosX();
			this.UpdatePosY();
		}

		public void MoveToPosX( int posX ) {
			this.physPos.X = FInt.Create(posX);
			this.UpdatePosX();
		}

		public void MoveToPosY( int posY ) {
			this.physPos.Y = FInt.Create(posY);
			this.UpdatePosY();
		}

		public void StopX() {
			this.velocity.X = FInt.Create(0);
		}

		public void StopY() {
			this.velocity.Y = FInt.Create(0);
		}
	}
}
