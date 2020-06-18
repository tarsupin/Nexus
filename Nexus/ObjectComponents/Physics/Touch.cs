using Nexus.GameEngine;

namespace Nexus.ObjectComponents {

	public class Touch {

		public bool isTouching = false;

		public bool toLeft = false;
		public bool toRight = false;
		public bool toTop = false;
		public bool toBottom = false;
		public bool toFloor = false;			// True if touched the floor last frame, but not this one.

		public GameObject moveObj = null;		// Reference to the moving object you're standing on.
		public bool onMover = false;			// TRUE if you're on an object that is moving, and you move with it (conveyor, platform, etc).

		public void ResetTouch() {
			if(!this.isTouching) { return; }
			this.isTouching = false;

			if(this.toBottom) {
				this.toBottom = false;
				this.isTouching = true;
				this.toFloor = true;
			} else {
				this.toFloor = false;
			}

			if(this.toTop) { this.toTop = false; }
			if(this.toLeft) { this.toLeft = false; }
			if(this.toRight) { this.toRight = false; }
		}

		public void TouchLeft() {
			this.toLeft = true;
			if(this.isTouching == false) { this.isTouching = true; }
		}

		public void TouchRight() {
			this.toRight = true;
			if(this.isTouching == false) { this.isTouching = true; }
		}

		public void TouchUp() {
			this.toTop = true;
			if(this.isTouching == false) { this.isTouching = true; }
		}

		public void TouchDown() {
			this.toBottom = true;
			this.toFloor = true;
			if(this.isTouching == false) { this.isTouching = true; }
		}

		public void TouchMover( GameObject mover = null ) {
			if(this.isTouching == false) { this.isTouching = true; }
			this.moveObj = mover;
			this.onMover = true;
		}

		public void ProcessMover( Physics physics ) {
			this.onMover = false;
			if(this.moveObj == null) { return; }
			physics.SetExtraMovement(this.moveObj.physics.AmountMovedX, this.moveObj.physics.AmountMovedY);
			this.moveObj = null;
		}
	}
}
