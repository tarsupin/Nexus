using Nexus.GameEngine;

namespace Nexus.ObjectComponents {

	public class Touch {

		public bool isTouching = false;

		public bool toLeft = false;
		public bool toRight = false;
		public bool toTop = false;
		public bool toBottom = false;

		public byte toFloor = 0;                // Number of frames since touched the floor (up to ten)

		public GameObject touchObj = null;
		public bool onPlatform = false;

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
			if(this.onPlatform) { this.onPlatform = false; }
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

		public void TouchPlatform( GameObject platform = null ) {
			if(this.isTouching == false) { this.isTouching = true; }
			this.touchObj = platform;
			this.onPlatform = true;
		}
	}
}
