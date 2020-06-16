
namespace Nexus.Engine {

	// Add a ColorToggles class for every RoomScene. It will track the BR (Blue-Red) switches and timers, and the GY (Green-Yellow) switches and timers.
	public class ColorToggles {

		public bool toggleBR { get; protected set; }
		public bool toggleGY { get; protected set; }
		public int timerBREnd { get; protected set; }
		public int timerGYEnd { get; protected set; }

		public ColorToggles() {
			this.ResetColorToggles();
		}

		// Toggle Colored Toggles. Used to be toggleWorldObjects()
		public void ToggleColor(bool isBR, bool addTime = false) {

			// Color Toggle
			if(isBR) { this.toggleBR = !this.toggleBR; } else { this.toggleGY = !this.toggleGY; }

			// Add Time to Timer: 10 seconds added to current Color Toggle Timer.
			if(addTime) {
				if(isBR) { this.timerBREnd = Systems.timer.Frame + 600; } else { this.timerGYEnd = Systems.timer.Frame + 600; }
			}

			Systems.sounds.toggle.Play();
		}

		public void RunColorTimers() {
			int frame = Systems.timer.Frame;

			// BR Timer
			if(this.timerBREnd > frame) {

				// Run the BR Timer, but only if it's more relevant than the GY Timer
				if(this.timerGYEnd < frame || this.timerBREnd < this.timerGYEnd) {
					this.RunTimerCountdown((short)(this.timerBREnd - frame));
					return;
				}
			}

			// Run the GY Timer
			if(this.timerGYEnd > frame) {
				this.RunTimerCountdown((short)(this.timerGYEnd - frame));
			}
		}

		private void RunTimerCountdown(short timeRemaining) {
			if(timeRemaining <= 60) {
				if(timeRemaining % 10 == 0) {
					if(timeRemaining % 20 == 0) {
						Systems.sounds.timer1.Play(1f, 0, 0);
					} else {
						Systems.sounds.timer2.Play(1f, 0, 0);
					}
				}
			} else if(timeRemaining <= 180) {
				if(timeRemaining % 15 == 0) {
					if(timeRemaining % 30 == 0) {
						Systems.sounds.timer1.Play(0.8f, 0, 0);
					} else {
						Systems.sounds.timer2.Play(0.8f, 0, 0);
					}
				}
			} else if(Systems.timer.IsBeatFrame) {
				byte beat4Mod = Systems.timer.beat4Modulus;
				if(beat4Mod == 0) {
					Systems.sounds.timer1.Play(0.6f, 0, 0);
				} else if(beat4Mod == 2) {
					Systems.sounds.timer2.Play(0.6f, 0, 0);
				}
			}
		}

		public void ResetColorToggles() {
			toggleBR = true;
			toggleGY = true;
			timerBREnd = 0;
			timerGYEnd = 0;
		}
	}
}
