using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.ObjectComponents {

	public class Physics {

		// Object Reference
		protected DynamicGameObject objRef;

		// Physics Values
		public FVector lastPos;
		public FVector velocity;
		public FVector extraMovement;
		public FInt gravity;
		public Touch touch;

		protected bool hasExtraMovement;

		public Physics( DynamicGameObject objRef ) {
			this.objRef = objRef;

			this.lastPos = new FVector();
			this.lastPos = FVector.VectorAdd(this.lastPos, this.objRef.pos);

			this.velocity = new FVector();
			this.extraMovement = new FVector();
			this.gravity = new FInt();
			this.hasExtraMovement = false;
			this.touch = new Touch();
		}

		// Get Amount Moved (in old system, movement was updated every frame; no need for that)
		public FVector AmountMoved {
			get { return FVector.VectorSubtract(this.objRef.pos, this.lastPos); }
		}

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
			this.lastPos = this.objRef.pos;
			this.objRef.pos = FVector.VectorAdd(this.objRef.pos, this.velocity);
			
			// Extra Movement (such as caused by Platforms or Conveyors)
			if(hasExtraMovement) {
				this.objRef.pos = FVector.VectorAdd(this.objRef.pos, this.extraMovement);
				this.extraMovement = new FVector();
			}
		}

		public void MoveToPos( FVector pos ) {
			this.lastPos = pos;
			this.objRef.pos = pos;
		}

		public void MoveToPosX( int posX ) {
			this.lastPos.X = this.objRef.pos.X;
			this.objRef.pos.X = FInt.Create(posX);
		}

		public void MoveToPosY( int posY ) {
			this.lastPos.Y = this.objRef.pos.Y;
			this.objRef.pos.Y = FInt.Create(posY);
		}

		public void StopX() {
			this.velocity.X = FInt.Create(0);
		}

		public void StopY() {
			this.velocity.Y = FInt.Create(0);
		}
	}
}
