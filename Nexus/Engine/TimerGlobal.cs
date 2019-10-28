using System;

namespace Nexus.Engine {
	public class TimerGlobal {
		private bool paused;

		public uint Frame { get; protected set; }		// The current frame.
		public uint tick20;								// 20 per second, 1 per 3 frames, increments forever. Can use for global timing events.
		public byte tick20Modulus;                      // The current tick20 (tick % 20). Cycles through 20 per second, loops back to 0.
		public uint beat;								// 4 per second, 1 per 15 frames, increments forever. Can use for global timing events.
		public uint beatModulus;						// The current beat (beat % 4). Cycles through 4 per second, loops back to 0.

		public TimerGlobal() {
			this.ResetTimer();
		}

		// Returns TRUE if the current frame is a 'tick'
		public bool isTickFrame {
			get { return this.Frame % 3 == 0; }
		}

		public void ResetTimer() {
			this.Frame = 0;
			this.tick20 = 0;
			this.tick20Modulus = 0;
			this.beat = 0;
			this.beatModulus = 0;
		}

		public void RunTick() {
			this.Frame += 1;
			this.tick20 = (uint) Math.Floor((double)(this.Frame / 3));
			this.tick20Modulus = (byte)(this.tick20 % 20);
			this.beat = (uint) Math.Floor((double)(this.Frame / 15));
			this.beatModulus = (byte)(this.beat % 4);
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
