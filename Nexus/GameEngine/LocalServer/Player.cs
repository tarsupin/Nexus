/*
 * Player Terminology:
 * 
 *		Player: Any Player, regardless of type: Self, Bot, Playmate.
 *		Self: The active player, the one controlling the program directly.
 *		Bot: A simulated player; has pre-programmed inputs.
 *		Playmate: An active human player other than Self; one controlled through multiplayer on another system.
 */

using Nexus.Objects;

namespace Nexus.GameEngine {

	public class Player {

		// Player Data
		public byte playerId;			// The ID of the player. Assigned on generation.

		public bool isPlaceholder;		// True if the player is the one controlling the running program.
		public bool isSelf;				// True if the player is the one controlling the running program.
		public bool isBot;				// True if the player is a simulated bot.
		public bool isPlaymate;			// True if the player is a multiplayer player.

		public bool recordInputs;		// True if the player will maintain records of the inputs pressed this game.

		public PlayerInput input;		// Tracks the input specific to the player.
		public Character character;		// Reference to the Character the player controls.

		public Player( byte playerId ) {
			this.playerId = playerId;
			this.input = new PlayerInput(this);
		}

		public void AssignCharacter( Character character, bool isSelf ) {
			this.character = character;
			this.character.AssignPlayer(this);

			this.isPlaceholder = false;
			this.isSelf = isSelf;
			this.isPlaymate = !isSelf;
		}
	}

	public class PlayerPlaceholder : Player {
		public PlayerPlaceholder() : base(0) {
			this.isPlaceholder = true;
		}
	}
}
