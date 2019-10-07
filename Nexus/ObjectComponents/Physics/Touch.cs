
using Nexus.GameEngine;

namespace Nexus.ObjectComponents {

	public class Touch {

		public bool isTouching = false;

		public bool toLeft = false;
		public bool toRight = false;
		public bool toTop = false;
		public bool toBottom = false;

		public bool toFloor = false;			// True if touching the floor; lasts for extra round beyond touchBottom.

		public GameObject touchObj = null;		// Touching Floor

		public void ResetTouch() {
			if(!this.isTouching) { return; }
			this.isTouching = false;

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
			this.isTouching = true;
		}

		public void TouchRight() {
			this.toRight = true;
			this.isTouching = true;
		}

		public void TouchUp() {
			this.toTop = true;
			this.isTouching = true;
		}

		public void TouchDown() {
			this.toBottom = true;
			this.isTouching = true;
		}

		public void TouchPlatform( GameObject platform ) {
			this.isTouching = true;
			this.touchObj = platform;
		}
	}
}
