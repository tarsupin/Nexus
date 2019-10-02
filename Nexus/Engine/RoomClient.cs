using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Engine {
	class RoomClient {

		public Dictionary<uint, Player> players;        // Players in the simulation.

		public RoomClient() {
			this.players = new Dictionary<uint, Player>();
		}

		// Runs the next frame for the server room. Once done, this frame cannot be reversed. Penalizes laggy players.
		public void ProcessFrame() {

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
