using System.Collections.Generic;

/*
 * The InputPlayer class must:
 *	
 *	1. Receive input from each human player in the room, each frame.
 *		- pressedIKeys[x]
 *		- releasedIKeys[x]
 *		
 *	2. Receive input from any bots in the room. (Loads all input for scene in one go)
 *		- simulatedIKeys
 *		
 *	3. Translate all inputs into player-specific Dictionaries that track IKeyState:
 *		- iKeyTrack		: Tracks what state each IKey is in.
 *	
 *	4. If logging the data, save as Simulated IKeys.
 *		- simulatedIKeys
 */

namespace Nexus.Engine {

	/*
	 * Player Terminology:
	 * 
	 *		Player: Any Player, regardless of type: Self, Bot, Playmate.
	 *		Self: The active player, the one controlling the program directly.
	 *		Bot: A simulated player; has pre-programmed inputs.
	 *		Playmate: An active human player other than Self; one controlled through multiplayer on another system.
	 */

	public enum PlayerType : byte {
		Self = 1,
		Bot = 2,
		Playmate = 3,
	}

	/*
	 * Input Logging Instructions:
	 * The server must log instructions by player and frame. This will allow it to run the full simulation of the game.
	 * 
	 * Dictionary<Frame, [IKey, IKey, ...]>		// Each time IKey is activated, toggle it's state between up or down.
	 */

	public enum InputLoggingRules : byte {
		NoTracking = 0,
		Debug = 1,
	}

	public class InputPlayer {

		public PlayerType playerType;

		// Track the key states of each IKey (On, Off, Released, Pressed).
		private Dictionary<IKey, IKeyState> iKeyTrack;

		// Track what keys were pressed or released last turn.
		private IKey[] pressedLast;
		private IKey[] releasedLast;

		private byte pressedNum;     // Array Cursor (for pressedLast)
		private byte releasedNum;    // Array Cursor (for releasedLast)

		// Logging
		public InputLoggingRules trackInputLog;				// If/how you want to track input logging.
		public Dictionary<uint, IKey[]> inputLog;           // Tracks IKeys on the exact frames they were pressed or released.

		public InputPlayer( PlayerType playerType ) {
			this.playerType = playerType;

			this.iKeyTrack = new Dictionary<IKey, IKeyState>();

			// Retain memory of last frame of Pressed and Released IKeys
			this.pressedNum = 0;
			this.releasedNum = 0;

			this.pressedLast = new IKey[8];
			this.releasedLast = new IKey[8];

			// TODO CONFIG: Change the bot setting.
			// If not a bot, assign debugging for now.
			this.trackInputLog = this.playerType == PlayerType.Bot ? InputLoggingRules.NoTracking : InputLoggingRules.Debug;
		}

		public void ApplyActiveInput( IKey[] pressedIKeys, IKey[] releasedIKeys ) {

			// It is possible for these to be processed multiple times in a single frame on laggy multiplayer.
			// Presumably, this solution should be tolerant against the issue. It will, however, punish lag rather than punish others.
			foreach( IKey iKey in pressedIKeys ) {
				this.iKeyTrack[iKey] = IKeyState.Pressed;
				this.pressedLast[this.pressedNum] = iKey;
				if(this.pressedNum < 7) { this.pressedNum++; }
			}

			foreach( IKey iKey in releasedIKeys ) {
				this.iKeyTrack[iKey] = IKeyState.Released;
				this.releasedLast[this.releasedNum] = iKey;
				if(this.releasedNum < 7) { this.releasedNum++; }
			}
		}

		// Simulated Bots have a different method of handling input.
		public void ApplySimulatedInput() {

		}

		public void ProcessInput() {

			// Any Pressed or Released IKeys from last frame must turn to On / Off respectively:
			for( byte i = 0; i < this.pressedNum; i++ ) {
				IKey iKey = this.pressedLast[this.pressedNum];

				// It is possible for the IKeyState to have changed (one-frame change); therefore, must confirm that last IKeyState is "Pressed"
				if(this.iKeyTrack[iKey] == IKeyState.Pressed) { this.iKeyTrack[iKey] = IKeyState.On; }
			}

			for( byte i = 0; i < this.releasedNum; i++ ) {
				IKey iKey = this.releasedLast[this.releasedNum];

				// It is possible for the IKeyState to have changed (one-frame change); therefore, must confirm that last IKeyState is "Released"
				if(this.iKeyTrack[iKey] == IKeyState.Released) { this.iKeyTrack[iKey] = IKeyState.Off; }
			}

			// Reset Frame Memory
			this.pressedNum = 0;
			this.releasedNum = 0;
		}

		/*******************************
		****** Key State Tracking ******
		*******************************/

		// Resets all IKey States to "Off"
		public void ResetIKeyStates() {
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
