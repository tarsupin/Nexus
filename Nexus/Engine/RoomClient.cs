using System.Collections.Generic;

namespace Nexus.Engine {

	public class RoomClient {

		private readonly Systems systems;

		public Dictionary<uint, Player> players;        // Players in the simulation.

		public RoomClient( Systems systems ) {
			this.systems = systems;
			this.players = new Dictionary<uint, Player>();
		}

		// Receive Packet
		public void HandlePacket(ServerPacket packet) {

			// Handle Input (IKeyPacket)
			if(packet.instruction == ServerPacketIns.IKeys) {
				this.HandleInputPacket((IKeyPacket) packet);
			}
		}

		// Handle Input Packet
		private void HandleInputPacket(IKeyPacket packet) {

			// Loop through each player, and handle data:
			foreach(KeyValuePair<byte, Dictionary<byte, IKey[]>> entry in packet.data) {

				// Send Input to Scene
				// entry.Key = playerId, entry.Value[0] = iKeysPressed, entry.Value[1] = iKeysReleased
				this.systems.scene.ReceivePlayerInput(packet.frame, entry.Key, entry.Value[0], entry.Value[1]);
			}
		}

		// Runs the next frame for the server room. Once done, this frame cannot be reversed. Penalizes laggy players.
		public void ProcessFrame( uint frame ) {

		}

		public void AddPlayer( Player player ) {
			this.players.Add(player.playerId, player);
		}

		public void RemovePlayer( Player player ) {
			this.players.Remove(player.playerId);
		}

		public void RemovePlayer( uint playerId ) {
			this.players.Remove(playerId);
		}
	}
}
