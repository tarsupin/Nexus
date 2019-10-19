using System;

namespace Nexus.Engine {
	public class TimerGlobal {
		private bool paused;

		public uint frame;				// The current frame.
		public uint triFrame;			// 20 per second, 1 per 3 frames, increments forever. Can use for global timing events.
		public byte beat;				// The current beat (tick % 20). Cycles through 20 beats per second, loops back to 0.

		public TimerGlobal() {
			this.ResetTimer();
		}

		// Returns TRUE if the current frame is a 'tick'
		public bool isTickFrame {
			get { return this.frame % 3 == 0; }
		}

		public void ResetTimer() {
			this.frame = 0;
			this.triFrame = 0;
			this.beat = 0;
		}

		public void RunTick() {
			this.frame += 1;
			this.triFrame = (uint) Math.Floor((double)(this.frame / 3));
			this.beat = (byte)(this.triFrame % 20);
		}

		public void Pause() {
			if (this.paused == true) { return; }
			this.paused = true;
		}

		public void Unpause() {
			if (this.paused == false) { return; }
			this.paused = false;
		}
	}
}
