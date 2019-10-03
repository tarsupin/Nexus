using System.Collections.Generic;

/*
 * The RoomServer class must:
 *	
 *	1. Receive input from each human player in the room, each frame.
 *		- pressedIKeys[x]
 *		- releasedIKeys[x]
 *		
 *	2. Receive input from any bots in the room. (Loads all input for scene in one go)
 *		- simulatedIKeys
 *		
 *	3. Pass all input instructions to each client.
 */

namespace Nexus.Engine {
	class RoomServer {

		public Dictionary<uint, Player> players;        // Players in the simulation.

		public RoomServer() {
			this.players = new Dictionary<uint, Player>();
		}

		// Runs the next frame for the server room. Once done, this frame cannot be reversed. Penalizes laggy players.
		public void ReceiveInput() {

		}

		public void ReceiveIKeys(Player player, IKey[] pressedIKeys, IKey[] releasedIKeys) {
			this.players[player.playerId].input.ApplyActiveInput(pressedIKeys, releasedIKeys);
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
