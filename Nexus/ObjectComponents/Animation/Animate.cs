using Nexus.Engine;

// The Animate Class is attached as a component to the DynamicGameObject (e.g. character.animate).

namespace Nexus.ObjectComponents {

	public class Animate {

		public byte AnimNum { get; private set; }		// The number of animation IDs in a loop.
		public byte AnimId { get; private set; }		// The animation ID of the texture (e.g. "1", "2", etc.), which cycles through the animation.
		public byte AnimSpeed { get; private set; }		// The number of frames that each animation takes place.
		public uint NextFrame { get; private set; }     // The frame # that the texture will next update.

		public Animate() {
			this.AnimNum = 0;
			this.AnimSpeed = 15;
			this.AnimId = 1;
			this.NextFrame = 0;
		}

		// Returns TRUE if we're on an animation update cycle.
		public bool IsAnimationTick(TimerGlobal timer) {

			// If the animation is limited to 1 frame or less, it has no animations; therefore, no animation tick.
			if(this.AnimNum <= 1) { return false; }

			// Check if the Animation Tick has met the next frame required.
			return this.NextFrame <= timer.frame;
		}

		// Sets (and returns) the Next Animation ID. Run on any frame when it's an Animation Tick.
		public byte NextAnimationId(TimerGlobal timer) {

			// Update the Animation ID (and cycle it once it reaches the animation number limit).
			this.AnimId += 1;
			if(this.AnimId > this.AnimNum) { this.AnimId = 1; }

			// Finally, update the next frame to run this effect in.
			this.NextFrame = timer.frame + AnimSpeed;

			return this.AnimId;
		}

		// Sets (and returns) a designated Animation ID.
		public byte SetAnimation( TimerGlobal timer, byte animNum, byte animSpeed, byte animId = 1 ) {
			this.AnimNum = animNum;
			this.AnimSpeed = animSpeed;
			this.AnimId = animId;
			this.NextFrame = timer.frame + animSpeed;
			return this.AnimId;
		}

		// Disables Animation
		public void DisableAnimation() {
			this.AnimNum = 0;
		}
	}
}
