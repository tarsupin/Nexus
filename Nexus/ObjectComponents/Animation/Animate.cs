using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

// The Animate Class is attached as a component to the DynamicObject (e.g. character.animate).

namespace Nexus.ObjectComponents {

	public class Animate {

		public DynamicObject actor;

		public string name;
		public string BaseName { get; private set; }		// The base of the animation name (e.g. "Moosh/")
		public string[] AnimCycles { get; private set; }	// The animation names to cycle through (e.g. "WalkLeft1", "WalkLeft2", etc).
		public byte CycleId { get; private set; }			// The animation ID of the texture (e.g. "1", "2", etc.), which cycles through the animation.
		public byte AnimSpeed { get; private set; }			// The number of frames that each animation takes place.
		public uint NextFrame { get; private set; }			// The frame # that the texture will next update.

		public Animate( DynamicObject actor, string baseName = "" ) {
			this.actor = actor;
			this.BaseName = baseName;
			this.AnimCycles = new string[0];
			this.AnimSpeed = 15;
			this.CycleId = 0;
			this.NextFrame = 0;
		}

		// The Animation Tick, which should be run during the object's RunTick cycle.
		public void RunAnimationTick(TimerGlobal timer) {

			// Only run if this frame will update the animation.
			if(!this.IsAnimationTick(timer)) { return; }

			// Update the Animation ID (and cycle it once it reaches the animation number limit).
			this.CycleId += 1;
			if(this.CycleId >= this.AnimCycles.Length) { this.CycleId = 0; }

			// Update the next frame that the animation will tick.
			this.NextFrame = timer.Frame + this.AnimSpeed;

			// Update the actor's sprite name according to the next animation cycle.
			this.actor.SetAnimationSprite(this.BaseName + this.AnimCycles[this.CycleId]);
		}

		// Returns TRUE if we're on an animation update cycle.
		public bool IsAnimationTick(TimerGlobal timer) {

			// If the animation is limited to 1 frame or less, it has no animations; therefore, no animation tick.
			if(this.AnimCycles.Length <= 1) { return false; }

			// Check if the Animation Tick has met the next frame required.
			return this.NextFrame <= timer.Frame;
		}

		// Sets a designated animation.
		// SetAnimation( "Moosh/Brown/Walk", AnimCycleMap.Cycle3, 15 );
		// cycleId is the number you want to start at (e.g. if you want to start at the second walk cycle instead of the first).
		public void SetAnimation( string baseName, string[] animCycles, byte animSpeed, byte cycleId = 0 ) {

			// Don't update the animation if you're using the same animation.
			if(this.BaseName == baseName && this.AnimCycles == animCycles) { return; }

			if(baseName != null) { this.BaseName = baseName; }

			this.AnimCycles = animCycles;
			this.AnimSpeed = animSpeed;
			this.CycleId = cycleId;
			this.NextFrame = Systems.timer.Frame + animSpeed;

			// Update the actor's sprite name according to the next animation cycle.
			this.actor.SetAnimationSprite(this.BaseName + this.AnimCycles[this.CycleId]);
		}

		// Disables Animation
		public void DisableAnimation() {
			if(this.AnimCycles.Length > 0) { this.AnimCycles = AnimCycleMap.NoAnimation; }
		}
	}
}
