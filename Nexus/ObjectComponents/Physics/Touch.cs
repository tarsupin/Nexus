using Nexus.GameEngine;

namespace Nexus.ObjectComponents {

	public class Touch {

		public bool isTouching = false;

		public bool toLeft = false;
		public bool toRight = false;
		public bool toTop = false;
		public bool toBottom = false;

		public byte toFloor = 0;                // Number of frames since touched the floor (up to ten)

		public GameObject moveObj = null;		// Reference to the moving object you're standing on.
		public bool onMover = false;			// TRUE if you're on an object that is moving, and you move with it (conveyor, platform, etc).

		public void ResetTouch() {
			if(!this.isTouching) { return; }
			this.isTouching = false;

			if(toBottom) {
				toBottom = false;
				isTouching = true;
				toFloor = 0;
			} else {
				if(toFloor < 11) { toFloor++; }
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
			this.toFloor = 0;
			if(this.isTouching == false) { this.isTouching = true; }
		}

		public void TouchMover( GameObject mover = null ) {
			if(this.isTouching == false) { this.isTouching = true; }
			this.moveObj = mover;
			this.onMover = true;
		}

		public void ProcessMover( Physics physics ) {
			this.onMover = false;
			physics.SetExtraMovement(this.moveObj.physics.AmountMovedX, this.moveObj.physics.AmountMovedY);
		}
	}
}
