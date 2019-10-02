﻿using System;

namespace Nexus.Engine {
	public class TimerGlobal {
		private bool paused;

		public uint frame;				// The current frame.
		public uint tick;               // The current tick. 20 ticks per second, 1 per 3 frames. Can use for global timing events.
		public byte beat;				// The current beat (tick % 20). Cycles through 20 beats per second.

		public TimerGlobal() {
			this.reset();
		}

		// Returns TRUE if the current frame is a 'tick'
		public bool isTickFrame {
			get { return this.frame % 3 == 0; }
		}

		public void reset() {
			this.frame = 0;
			this.tick = 0;
			this.beat = 0;
		}

		public void update() {
			this.frame += 1;
			this.tick = (uint) Math.Floor((double)(this.frame / 4));
			this.beat = (byte)(this.tick % 20);
		}

		public void pause() {
			if (this.paused == true) { return; }
			this.paused = true;
		}

		public void unpause() {
			if (this.paused == false) { return; }
			this.paused = false;
		}
	}
}
