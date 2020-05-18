
using Nexus.GameEngine;

namespace Nexus.ObjectComponents {

	public class Touch {

		public bool isTouching = false;

		public bool toLeft = false;
		public bool toRight = false;
		public bool toTop = false;
		public bool toBottom = false;

		public bool toFloor = false;			// True if touching the floor; lasts for extra round beyond touchBottom.

		public DynamicObject touchObj = null;		// Touching Floor

		public void ResetTouch() {
			if(!this.isTouching) { return; }
			this.isTouching = false;

			if(this.toFloor) { this.toFloor = false; }

			// TODO HIGH PRIOIRTY: Affected by Platforms
			//if(this.touchObj != null) {
			//	if(this.touchObj is Platform) {

			//	}
			//}

			if(toBottom) {
				toBottom = false;
				toFloor = true;
				isTouching = true;
			} else {
				isTouching = false;
			}

			toTop = false;
			toLeft = false;
			toRight = false;
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

		public void TouchPlatform( DynamicObject platform ) {
			if(this.isTouching == false) { this.isTouching = true; }
			this.touchObj = platform;
		}
	}
}
