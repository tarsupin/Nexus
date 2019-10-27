using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.ObjectComponents {

	public class Physics {

		// Object Reference
		protected DynamicGameObject actor;

		// Physics Values
		public FVector physPos;			// The "physics" tracks "true" positions with Fixed-Point math, but is separate from "position" on the Game Object, which uses ints.
		public int lastPosX;
		public int lastPosY;
		public FVector velocity;
		public FVector extraMovement;
		public FInt gravity;
		public Touch touch;

		protected bool hasExtraMovement;

		public Physics( DynamicGameObject actor ) {
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

		public void RunTick() {
			this.velocity.Y += this.gravity;
			this.touch.ResetTouch();
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
