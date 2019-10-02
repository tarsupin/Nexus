using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Engine {

	public enum PlayerHost {
		Client = 1,				// The "current" player on the device.
		ClientSim = 2,			// A simulated player; possesses keystrokes in advance of game.
		Server = 3,				// A player that is being hosted through a multiplayer server.
	}

	public class Player {

		public byte playerId;           // The ID of the player. Assigned on generation.
		public PlayerHost pHost;		// Client, ClientSim (simulated player), Server, etc.
		public InputPlayer input;		// Tracks the input specific to the player.

		public Player( PlayerHost pHost, byte playerId ) {
			this.pHost = pHost;
			this.playerId = playerId;
			//this.input = new InputPlayer();
		}
	}
}
