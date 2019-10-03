using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

/*
 * The InputClient class must:
 *	
 *	1. Determine what input the player has pressed, both this frame and the last.
 *		- Keyboard
 *		- GamePad
 *		
 *	2. Translate that input into a Dictionary that tracks IKeyState:
 *		- iKeyTrack		: Tracks what state each IKey is in.
 *	
 *	3. Make the two iKey Dictionaries available to InputPlayer classes.
 */

/*
 * Steps:
 * 1. Player presses a key. Checks previous state, was < "On"
 *		- Turns IKeyState to "Pressed"
 * 2. Player holds key. Checks previous state, was >= "On"
 *		- Turns IKeyState to "On"
 * 3. Player releases key. Checks previous state, was >= "On"
 *		- Turns IKeyState to "Released"
 * 4. Player does nothing.
 *		- Turns IKeyState to "Off"
 */

namespace Nexus.Engine {

	public enum IKey {
		Up = 1,
		Down = 2,
		Left = 3,
		Right = 4,
		XButton = 5,
		YButton = 6,
		AButton = 7,
		BButton = 8,
		L1 = 9,
		R1 = 10,
		L2 = 11,
		R2 = 12,
		Select = 13,
		Start = 14,
		AxisLeftPress = 15,
		AxisRightPress = 16,
		Other = 17,
	};

	public enum IKeyState {
		Off = 0,
		Released = 1,
		On = 2,
		Pressed = 3,
	}

	public class InputClient {
		private readonly Systems systems;

		// Bind Keys (and Reverse Lookup)
		private Dictionary<Keys, IKey> keyMap;
		private Dictionary<Buttons, IKey> buttonMap;
		private Dictionary<IKey, Keys> revKeyMap;
		private Dictionary<IKey, Buttons> revButtonMap;

		// Track when IKey is toggled (Pressed / Released) each frame. This will be sent to the server.
		public IKey[] pressedIKeys;
		public IKey[] releasedIKeys;
		
		private byte pressedNum;            // Tracks the array position of pressedIKeys.
		private byte releasedNum;			// Tracks the array position of releasedIKeys.

		// Track Previous States of Keyboard and GamePad (to determine what was pressed vs. released)
		private KeyboardState curKeyState, prevKeyState;
		private GamePadState curPadState, prevPadState;

		public InputClient( Systems systems ) {
			this.systems = systems;

			this.pressedNum = 0;
			this.pressedIKeys = new IKey[8];
			this.releasedNum = 0;
			this.releasedIKeys = new IKey[8];

			this.keyMap = new Dictionary<Keys, IKey>();
			this.buttonMap = new Dictionary<Buttons, IKey>();
			this.revKeyMap = new Dictionary<IKey, Keys>();
			this.revButtonMap = new Dictionary<IKey, Buttons>();

			this.prevKeyState = Keyboard.GetState();
			this.prevPadState = GamePad.GetState(PlayerIndex.One);

			this.AssignDefaultKeyMap();
			this.AssignDefaultButtonMap();
		}

		public void Update() {

			// Reset Array Cursors/Positions
			this.pressedNum = 0;
			this.releasedNum = 0;

			// Save Current Input States
			this.curKeyState = Keyboard.GetState();
			this.curPadState = GamePad.GetState(PlayerIndex.One);

			this.ProcessIKeys();

			// Send Input to LocalServer (if any input needs to be sent)
			if(this.pressedNum > 0 && this.releasedNum > 0) {
				this.SendIKeysLocalRoom();
			}

			// Save Previous Input States
			this.prevKeyState = curKeyState;
			this.prevPadState = curPadState;
		}


		// TODO HIGH PRIORITY: ADAPT THIS AND SENDIKEYSTOSERVER() FOR NEW LOCAL SERVER ARCHITECTURE
		// TODO HIGH PRIORITY: ADAPT THIS AND SENDIKEYSTOSERVER() FOR NEW LOCAL SERVER ARCHITECTURE
		// TODO HIGH PRIORITY: ADAPT THIS AND SENDIKEYSTOSERVER() FOR NEW LOCAL SERVER ARCHITECTURE
		private void SendIKeysLocalRoom() {

			IKeyPacket packet = new IKeyPacket {
				instruction = ServerPacketIns.IKeys,
				frame = 10,
				data = new Dictionary<byte, Dictionary<byte, IKey[]>>() {
					{
						0, new Dictionary<byte, IKey[]> {
							{
								0, new IKey[] { IKey.L1 }
							},
							{
								1, new IKey[] { IKey.Down }
							}
						}
					}
				}
			};

			this.systems.roomClient.HandlePacket(packet);
		}

		private void SendIKeysToServer() {

			// If there are no updates, skip sending anything to server.
			if(this.pressedNum == 0 && this.releasedNum == 0) { return; }

			// Build IKey Packet
			ClientPacketIns packetInstruction;

			if(this.pressedNum > 0) {
				packetInstruction = this.releasedNum > 0 ? ClientPacketIns.IKeysBoth : ClientPacketIns.IKeysPressed;
			} else {
				packetInstruction = ClientPacketIns.IKeysReleased;
			}

			string packet = PacketFromClient.PacketIKeys(packetInstruction, this.pressedIKeys, this.pressedNum, this.releasedIKeys, this.releasedNum);

			// TODO CONSOLE: Remove
			Console.WriteLine("Packet Data" + packet + packetInstruction);

			// Send IKeys to Server

		}

		// Determine what IKeys were activated this frame.
		private void ProcessIKeys() {
			this.GetIKeyState(IKey.Up);
			this.GetIKeyState(IKey.Down);
			this.GetIKeyState(IKey.Left);
			this.GetIKeyState(IKey.Right);
			this.GetIKeyState(IKey.AButton);
			this.GetIKeyState(IKey.BButton);
			this.GetIKeyState(IKey.XButton);
			this.GetIKeyState(IKey.YButton);
			this.GetIKeyState(IKey.R1);
			this.GetIKeyState(IKey.R2);
			this.GetIKeyState(IKey.L1);
			this.GetIKeyState(IKey.L2);
			this.GetIKeyState(IKey.Start);
			this.GetIKeyState(IKey.Select);
			this.GetIKeyState(IKey.AxisLeftPress);
			this.GetIKeyState(IKey.AxisRightPress);
			this.GetIKeyState(IKey.Other);
		}

		private void GetIKeyState( IKey iKey ) {

			// Determine if the IKey was activated THIS frame.
			bool curDown = (this.curKeyState.IsKeyDown(this.revKeyMap[iKey]) || this.curPadState.IsButtonDown(this.revButtonMap[iKey]));

			// Determine if the IKey was activated LAST frame.
			bool lastDown = (this.prevKeyState.IsKeyDown(this.revKeyMap[iKey]) || this.prevPadState.IsButtonDown(this.revButtonMap[iKey]));

			// Assign the relevant key state to the given IKey
			if(curDown) {
				if(!lastDown) { this.AddPressedIKey(iKey); }
			} else if(lastDown) { this.AddReleasedIKey(iKey); }
		}

		private void AddPressedIKey(IKey iKey) {
			this.pressedIKeys[this.pressedNum] = iKey;
			if(this.pressedNum < 7) { this.pressedNum++; }
		}

		private void AddReleasedIKey(IKey iKey) {
			this.releasedIKeys[this.releasedNum] = iKey;
			if(this.releasedNum < 7) { this.releasedNum++; }
		}

		/***********************************
		****** Key Mapping & Tracking ******
		***********************************/

		// Default Key Mapping
		private void AssignDefaultKeyMap() {
			this.AssignKeyMap(Keys.W, IKey.Up);
			this.AssignKeyMap(Keys.A, IKey.Left);
			this.AssignKeyMap(Keys.S, IKey.Down);
			this.AssignKeyMap(Keys.D, IKey.Right);

			this.AssignKeyMap(Keys.Q, IKey.L1);
			this.AssignKeyMap(Keys.E, IKey.L2);
			this.AssignKeyMap(Keys.O, IKey.R1);
			this.AssignKeyMap(Keys.U, IKey.R2);

			this.AssignKeyMap(Keys.J, IKey.XButton);
			this.AssignKeyMap(Keys.K, IKey.AButton);
			this.AssignKeyMap(Keys.L, IKey.BButton);
			this.AssignKeyMap(Keys.I, IKey.YButton);

			this.AssignKeyMap(Keys.Enter, IKey.Start);
			this.AssignKeyMap(Keys.Back, IKey.Select);
			this.AssignKeyMap(Keys.Delete, IKey.Other);

			this.AssignKeyMap(Keys.OemOpenBrackets, IKey.AxisLeftPress);
			this.AssignKeyMap(Keys.OemCloseBrackets, IKey.AxisRightPress);
		}

		private void AssignKeyMap( Keys key, IKey iKey ) {
			this.revKeyMap.Add(iKey, key);
			this.keyMap.Add(key, iKey);
		}

		// Default Gamepad Mapping
		private void AssignDefaultButtonMap() {
			this.AssignButtonMap(Buttons.DPadUp, IKey.Up);
			this.AssignButtonMap(Buttons.DPadLeft, IKey.Left);
			this.AssignButtonMap(Buttons.DPadDown, IKey.Down);
			this.AssignButtonMap(Buttons.DPadRight, IKey.Right);

			this.AssignButtonMap(Buttons.LeftShoulder, IKey.L1);
			this.AssignButtonMap(Buttons.LeftTrigger, IKey.L2);
			this.AssignButtonMap(Buttons.RightShoulder, IKey.R1);
			this.AssignButtonMap(Buttons.RightTrigger, IKey.R2);

			this.AssignButtonMap(Buttons.X, IKey.XButton);
			this.AssignButtonMap(Buttons.A, IKey.AButton);
			this.AssignButtonMap(Buttons.B, IKey.BButton);
			this.AssignButtonMap(Buttons.Y, IKey.YButton);

			this.AssignButtonMap(Buttons.Start, IKey.Start);
			this.AssignButtonMap(Buttons.Back, IKey.Select);
			this.AssignButtonMap(Buttons.BigButton, IKey.Other);

			this.AssignButtonMap(Buttons.LeftThumbstickDown, IKey.AxisLeftPress);
			this.AssignButtonMap(Buttons.RightThumbstickDown, IKey.AxisRightPress);
		}

		private void AssignButtonMap(Buttons button, IKey iKey) {
			this.revButtonMap.Add(iKey, button);
			this.buttonMap.Add(button, iKey);
		}
	}
}
