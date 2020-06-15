using Nexus.Engine;
using Nexus.Objects;
using System.Collections.Generic;

/*
 * LocalServer Class Purpose:
 *	- Act as the gateway between client and server. Treat Single Player and Multiplayer the same.
 *		- Receive instructions (by packets) from Server and handle accordingly (such as to change GameRoom, add players, etc).
 *	- Exchange and interpret packets with the Multiplayer Server.
 *	- For Single Player, short-circuit the Multiplayer routing and convert packets directly.
 *	
 * LocalServer Mechanics:
 *	- Receive packets from Server. Interpret those packets, and route accordingly.
 *	- Pass instructions to the rest of the system.
 *	
 * Classes Connected to LocalServer:
 *  - InputClient: LocalServer receives direct input from client, and must process this packet, route accordingly.
 *  - GamePackets: LocalServer uses this class to convert packets between Client and Server (since the needs may be different).
 *  - [SERVER]: Instructions will be sent to LocalServer, then routed elsewhere.
 */

namespace Nexus.GameEngine {

	public class LocalServer {

		// LocalServer Data
		public bool online = false;
		public byte playerCount = 0;
		// server address, port, etc.

		// Game Data
		public Dictionary<byte, Player> players;        // Players in the room.

		public byte MyPlayerId { get; private set; }

		public LocalServer() {
			this.players = new Dictionary<byte, Player>();
			this.StartLocalGame();
		}

		// Returns the "Self" Player Instance
		public Player MyPlayer {
			get { return this.players[this.MyPlayerId]; }
		}

		public Character MyCharacter {
			get { return this.players[this.MyPlayerId].character; }
		}

		public void StartLocalGame() {
			this.RemoveAllPlayers();
			this.AddPlayer(1);
			this.MyPlayerId = 1;
		}

		/**********************
		****** Game Data ******
		***********************/

		// Add & Remove Players
		public void AddPlayer(byte playerId) { this.players.Add(playerId, new Player(playerId)); this.playerCount++; }
		public void RemoveAllPlayers() { this.players.Clear(); this.playerCount = 0; }
		public void RemovePlayer(Player player) { this.players.Remove(player.playerId); this.playerCount--; }
		public void RemovePlayer(byte playerId) { this.players.Remove(playerId); this.playerCount--; }

		/****************************
		****** Packet Handling ******
		****************************/

		public void ReceivePacket( List<object> packet ) {

			// Verify integrity of Packet. Reject any packets that don't match required parameters.
			// Otherwise, it may be possible to crash players by sending invalid packets.
			if(
				packet.Count < 3 ||						// Packet must be at least three elements.
				packet[0] is byte == false ||			// First packet is a (byte) PacketType.
				packet[1] is byte == false				// Second packet is an optional (byte), the instruction for that Packet Type. 
			) { return;  }

			this.InterpretPacket((byte)packet[0], (byte)packet[1], packet);
		}

		private void InterpretPacket(byte packetType, byte packetIns, List<object> packet) {
			
			switch(packetType) {

				// Input Packet
				case (byte) PacketType.Input:
					this.ReceiveInputPacket( packet );
					break;
			}
		}

		// Receive Input Packet from Server
		// ..., Frame #, [PLAYER INPUT PACKET], [PLAYER INPUT PACKET]
		// [PLAYER INPUT PACKET]: Player ID, IKeyPressCount, IKeyPressed x(repeats based on IKeyPressCount), IKeyReleased, IKeyReleased
		private void ReceiveInputPacket( List<object> packet ) {

			// TODO CLEANUP: Remove
			// Display Input on Console
			// System.Console.WriteLine("Input, Frame " + frame + ", Player " + playerId + ": " + iKeysPressed.ToString() + " & " + iKeysReleased.ToString());

			// Verify Integrity of Input Packet:
			if(packet.Count < 4) { return; }
			if(packet[2] is uint == false) { return; }

			uint frame = (uint) packet[2];

			// Loop through each player in the packet, and update their frames accordingly.
			byte len = (byte)packet.Count;

			for( byte i = 3; i < len; i++ ) {

				// Only read the packet if it is a byte array.
				if(packet[i] is byte[] == false) { continue; }

				byte[] playerPacket = (byte[]) packet[i];

				byte playerId = playerPacket[0];
				byte pressCount = playerPacket[1];
				byte packLen = (byte) playerPacket.Length;

				IKey[] iKeysPressed = new IKey[pressCount];
				IKey[] iKeysReleased = new IKey[packLen - pressCount - 2];

				// Loop through Key Presses and Releases.
				for( byte j = 2; j < packLen; j++ ) {

					// Identifies a Key Press
					if(j < pressCount + 2) {
						iKeysPressed[j - 2] = (IKey) playerPacket[j];
					}
					
					// Identifies a Key Release
					else {
						iKeysReleased[j - pressCount - 2] = (IKey)playerPacket[j];
					}
				}

				// Apply the Inputs to the Player's Key Tracking:
				this.players[playerId].input.ApplyInputs( frame, iKeysPressed, iKeysReleased );
			}
		}

		public void CreateInputPacket(IKey[] iKeysPressed, IKey[] iKeysReleased) {

			// If Single Player, short-circuit and Recieve Packet Locally:
			if(!this.online) {
				List<object> fakePacket = GamePackets.FakeInputToClient(0, this.MyPlayerId, iKeysPressed, iKeysReleased);
				this.ReceiveInputPacket(fakePacket);
				return;
			}

			// Prepare Packet
			List<object> packet = GamePackets.InputToServer(iKeysPressed, iKeysReleased);

			// Send to Server
			// TODO HIGH PRIORITY: Send Packet to Server
		}
	}
}
