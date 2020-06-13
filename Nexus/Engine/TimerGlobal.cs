using System;
using System.Diagnostics;

namespace Nexus.Engine {

	public class TimerGlobal {
		private bool paused;

		public Stopwatch stopwatch;						// A global stopwatch reference.

		public uint Frame { get; protected set; }       // The current frame.

		public byte frame60Modulus;						// Cycles through 60 per second, then loops back to 0.
		public byte frame16Modulus;						// Cycles through 16 frames, then loops back to 0.

		public uint tick20;								// 20 per second, 1 per 3 frames, increments forever. Can use for global timing events.
		public byte tick20Modulus;						// The current tick20 (tick % 20). Cycles through 20 per second, loops back to 0.
		protected byte tickModTrack;					// The last tick modulus. Updates when the tick modulus changes (every 3 frames).
		protected bool tickFrame;						// TRUE if this frame had a new tick occur (tick modulus changed).

		public uint beat;								// 4 per second, 1 per 15 frames, increments forever. Can use for global timing events.
		public byte beat16Modulus;						// A measure of a 4-second beat cycle (beat % 16). Cycles through 4 per second, until 16, then loops back to 0.
		public byte beat4Modulus;						// The current beat (beat % 4). Cycles through 4 per second, loops back to 0.
		protected byte beatModTrack;					// The last beat modulus. Updates when the beat modulus changes (every 15 frames).
		protected bool beatFrame;						// TRUE if this frame had a new beat occur (beat modulus changed).

		public TimerGlobal() {
			this.ResetTimer();
		}

		// Returns TRUE if the current frame is a 'tick'
		public bool IsTickFrame {
			get { return this.tickFrame; }
		}

		public bool IsBeatFrame {
			get { return this.beatFrame; }
		}

		public void ResetTimer() {
			this.Frame = 0;
			this.frame60Modulus = 0;
			this.tick20 = 0;
			this.tick20Modulus = 0;
			this.beat = 0;
			this.beat16Modulus = 0;
			this.beat4Modulus = 0;
			this.beatModTrack = 0;
			this.beatFrame = false;
		}

		public void RunTick() {

			this.Frame += 1;
			this.frame60Modulus = (byte)(this.Frame % 60);
			this.frame16Modulus = (byte)(this.Frame % 16);
			this.tick20 = (uint) Math.Floor((double)(this.Frame / 3));
			this.tick20Modulus = (byte)(this.tick20 % 20);
			this.beat = (uint) Math.Floor((double)(this.Frame / 15));
			this.beat16Modulus = (byte)(this.beat % 16);
			this.beat4Modulus = (byte)(this.beat16Modulus % 4);

			// Track Tick Frames
			if(this.tickModTrack != this.tick20Modulus) {
				this.tickModTrack = this.tick20Modulus;
				this.tickFrame = true;
			} else {
				this.tickFrame = false;
			}

			// Track Beat Frames
			if(this.beatModTrack != this.beat4Modulus) {
				this.beatModTrack = this.beat4Modulus;
				this.beatFrame = true;
			} else {
				this.beatFrame = false;
			}
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
