using Nexus.Engine;
using System.Collections.Generic;

// The InputPlayer class must log the inputs for each frame (of relevance) for the player.

namespace Nexus.GameEngine {

	public class PlayerInput {

		private Player player;

		// Track IKeys on the exact frames they were pressed or released.
		// byte[0] is the # of IKeyPressed elements that follow byte 0.
		// byte[1+] are IKeyPressed (until the byte[0] count is matched), and all remaining elements are IKeyReleased.
		public Dictionary<int, byte[]> inputLog;

		// Tracks the current status of each IKey.
		private Dictionary<IKey, IKeyState> iKeyTrack;

		public PlayerInput( Player player ) {
			this.player = player;
			this.inputLog = new Dictionary<int, byte[]>();
			this.iKeyTrack = new Dictionary<IKey, IKeyState>();
			this.ResetIKeyStates();
		}

		// Key Detection
		public bool isDown( IKey iKey ) { return this.iKeyTrack[iKey] == IKeyState.On || this.iKeyTrack[iKey] == IKeyState.Pressed; }
		public bool isPressed( IKey iKey ) { return this.iKeyTrack[iKey] == IKeyState.Pressed; }
		public bool isReleased( IKey iKey) { return this.iKeyTrack[iKey] == IKeyState.Released; }

		/***************************
		****** Key Processing ******
		***************************/

		public void ApplyInputs(int frame, IKey[] iKeysPressed, IKey[] iKeysReleased) {

			byte pressedNum = (byte)iKeysPressed.Length;
			byte releasedNum = (byte)iKeysReleased.Length;

			byte[] log = new byte[pressedNum + releasedNum + 1];

			log[0] = pressedNum;
			byte elementNum = 1;

			// Loop through each pressed IKey, and add it to the log.
			foreach(byte key in iKeysPressed) {
				log[elementNum] = key;
				elementNum++;
			}

			// Loop through each released IKey, and add it to the log.
			foreach(byte key in iKeysReleased) {
				log[elementNum] = key;
				elementNum++;
			}

			inputLog[frame] = log;
		}

		/*******************************
		****** Key State Tracking ******
		*******************************/

		// Update all Key States for Assigned Frame
		public void UpdateKeyStates(int frame) {

			// Loop through existing keys and update accordingly:
			for( byte i = 1; i < this.iKeyTrack.Count; i++ ) {
				IKeyState keyState = this.iKeyTrack[(IKey) i];

				if(keyState == IKeyState.Pressed) {
					this.iKeyTrack[(IKey) i] = IKeyState.On;
				} else if(keyState == IKeyState.Released) {
					this.iKeyTrack[(IKey) i] = IKeyState.Off;
				}
			}

			// Handle changes affected by this frame:
			if(inputLog.TryGetValue(frame, out byte[] kElements)) {

				// If there is no record for this frame, skip processing any new input.
				if(kElements.Length == 0) { return; }

				byte pressedNum = kElements[0];

				for( byte i = 1; i < kElements.Length; i++ ) {

					// IKeyPress is registered first:
					if(i < pressedNum + 1) {
						iKeyTrack[(IKey)kElements[i]] = IKeyState.Pressed;
					}

					// IKeyRelease is listed next:
					else {
						iKeyTrack[(IKey)kElements[i]] = IKeyState.Released;
					}
				}
			}

			// Remove this entry from the input logs (isn't needed anymore) unless the player is keeping the logs.
			if(!this.player.recordInputs) {
				this.inputLog.Remove(frame);
			}
		}

		// Resets all IKey States to "Off"
		private void ResetIKeyStates() {
			this.iKeyTrack.Add(IKey.Up, IKeyState.Off);
			this.iKeyTrack.Add(IKey.Down, IKeyState.Off);
			this.iKeyTrack.Add(IKey.Left, IKeyState.Off);
			this.iKeyTrack.Add(IKey.Right, IKeyState.Off);
			this.iKeyTrack.Add(IKey.AButton, IKeyState.Off);
			this.iKeyTrack.Add(IKey.XButton, IKeyState.Off);
			this.iKeyTrack.Add(IKey.BButton, IKeyState.Off);
			this.iKeyTrack.Add(IKey.YButton, IKeyState.Off);
			this.iKeyTrack.Add(IKey.Start, IKeyState.Off);
			this.iKeyTrack.Add(IKey.Select, IKeyState.Off);
			this.iKeyTrack.Add(IKey.Other, IKeyState.Off);
			this.iKeyTrack.Add(IKey.AxisLeftPress, IKeyState.Off);
			this.iKeyTrack.Add(IKey.AxisRightPress, IKeyState.Off);
			this.iKeyTrack.Add(IKey.R1, IKeyState.Off);
			this.iKeyTrack.Add(IKey.R2, IKeyState.Off);
			this.iKeyTrack.Add(IKey.L1, IKeyState.Off);
			this.iKeyTrack.Add(IKey.L2, IKeyState.Off);
		}
	}
}
