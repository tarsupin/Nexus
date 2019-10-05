using Nexus.Engine;
using System.Collections.Generic;

/*
 * Packets always have the following conventions for the first two elements:
 *	
 * packet List<object>
 *  [0] (byte)		packetType			PacketType enum.
 *  [1] (byte)		instruction			PacketInstruction enum, which is specific to whichever PacketType you're using.
 * 
 * After these two values, the rest of the packet may be different.
 * 
 */

namespace Nexus.GameEngine {

	public enum PacketType : byte {
		Input = 1,					// Input sent from players.

		PlayerData = 10,			// Details about players (such as IDs, usernames, etc)
	}

	public enum InputPacketIns : byte {

		// ..., IKeyPressed, IKeyPressed, ...			// All remaining elements are IKeyPress elements (indicates which keys were released this frame).
		IKeysPressed = 1,

		// ..., IKeyReleased, IKeyReleased, ...			// All remaining elements are IKeyRelease (indicates which keys were released this frame).
		IKeysReleased = 2,

		// ..., IKeyPressCount, IKeyPressed x(repeats based on IKeyPressCount), IKeyReleased, IKeyReleased
		IKeysBoth = 3,

		// ..., Frame #, [PLAYER INPUT PACKET], [PLAYER INPUT PACKET]
		// [PLAYER INPUT PACKET]: Player ID, IKeyPressCount, IKeyPressed x(repeats based on IKeyPressCount), IKeyReleased, IKeyReleased
		ToClientIKeys = 4,
	}

	public class GamePackets {

		// Creates a Fake Input Packet sent from the Server
		// ..., Frame #, [PLAYER INPUT PACKET], [PLAYER INPUT PACKET]
		// [PLAYER INPUT PACKET]: Player ID, IKeyPressCount, IKeyPressed x(repeats based on IKeyPressCount), IKeyReleased, IKeyReleased
		public static List<object> FakeInputToClient(uint frame, byte playerId, IKey[] IKeysPressed, IKey[] IKeysReleased) {

			// Create Packet
			var Packet = new List<object>();
			Packet.Add((byte)PacketType.Input);
			Packet.Add((byte)InputPacketIns.ToClientIKeys);
			Packet.Add((uint)frame);

			byte pressedNum = (byte) IKeysPressed.Length;
			byte releasedNum = (byte) IKeysReleased.Length;

			// Create "Player Input Packet" that goes within "Input Packet" - identifies the input for that specific player.
			byte[] playerInput = new byte[pressedNum + releasedNum + 2];
			playerInput[0] = playerId;
			playerInput[1] = pressedNum;

			byte elementNum = 2;

			// Loop through each IKeysReleased Key, and add to the elements
			foreach(byte keyPressed in IKeysPressed) {
				playerInput[elementNum] = keyPressed;
				elementNum++;
			}

			// Loop through each IKeysReleased Key, and add to the elements
			foreach(byte keyReleased in IKeysReleased) {
				playerInput[elementNum] = keyReleased;
				elementNum++;
			}

			// Attach the Player Packet to the Input Packet, and return.
			Packet.Add(playerInput);

			return Packet;
		}

		// Creates an Input Packet to send to the Server
		public static List<object> InputToServer(IKey[] IKeysPressed, IKey[] IKeysReleased) {

			var Packet = new List<object>();
			Packet.Add((byte)PacketType.Input);

			if(IKeysPressed.Length > 0) {
				if(IKeysReleased.Length > 0) {

					// Create "IKeysBoth" Input Packet
					Packet.Add((byte) InputPacketIns.IKeysBoth);
					Packet.Add((byte) IKeysPressed.Length);
				} else {

					// Create "IKeysPressed" Input Packet
					Packet.Add((byte)InputPacketIns.IKeysPressed);
				}
			} else {

				// Create "IKeysReleased" Input Packet
				Packet.Add((byte)InputPacketIns.IKeysReleased);
			}

			// Loop through each IKeysReleased Key, and add to the elements
			foreach(byte keyPressed in IKeysPressed) {
				Packet.Add(keyPressed);
			}

			// Loop through each IKeysReleased Key, and add to the elements
			foreach(byte keyReleased in IKeysReleased) {
				Packet.Add(keyReleased);
			}

			return Packet;
		}
		
	}
}
