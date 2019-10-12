using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

// Behaviors perform additional updates that relate to the actor, such as watching for a particular action.
// actor.behavior.RunTick();

namespace Nexus.ObjectComponents {

	public class PrepareHop : Behavior {

		private readonly byte reactLeft;
		private readonly ushort reactWidth;
		private readonly byte reactTop;
		private readonly ushort reactHeight;

		public PrepareHop( DynamicGameObject actor, byte left = 5, byte right = 5, byte above = 144, byte below = 0 ) : base(actor) {
			Bounds bounds = this.actor.bounds;
			this.reactLeft = (byte) (bounds.Left - left);
			this.reactTop = (byte) (bounds.Top - above);
			this.reactWidth = (ushort)(bounds.Right + right - this.reactLeft);
			this.reactHeight = (ushort) (bounds.Bottom + below - this.reactTop);
		}

		public void RunTick() {

			// Only run this behavior every 11 frames (prime number to reduce overlap).
			if(this.actor.scene.timer.frame % 11 != 0) { return; }

			FVector pos = this.actor.pos;

			uint objectId = CollideDetect.FindObjectsTouchingArea(
				this.actor.scene.objects[(byte)LoadOrder.Character],
				(uint) pos.X.IntValue + this.reactLeft,
				(uint) pos.Y.IntValue + this.reactTop,
				this.reactWidth,
				this.reactHeight
			);

			// If an object was located, enemy should hop.
			if(objectId != 0) {
				
			}
		}
	}
}
