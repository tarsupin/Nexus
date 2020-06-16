
namespace Nexus.Gameplay {

	public class GameClass {

		// Game Details
		public ArenaType arenaType;
		public GameClassFlag gameClassFlag;
		public string title;
		public string description;
		
		// Players Allowed
		public byte minPlayersAllowed = 2;		// Number of players that MUST be in the game.
		public byte maxPlayersAllowed = 4;      // Maximum number of players allowed in the game.

		// Game Behaviors
		public byte teams = 0;                  // 2+ means there are teams that play against each other.
		public bool pvp = false;				// Players are capable of damaging each other.
		
		// Respawns
		public bool respawn = true;								// Whether or not respawns are allowed.
		public short respawnFrames = 300;						// How many frames before someone respawns.
		public short respawnInvincible = 60;					// How many frames is the player invulnerable for after respawn?
		public bool respawnUntouchable = false;					// If TRUE, cannot be affected until you move.
		public RespawnType respawnType = RespawnType.Standard;	// The type of respawn behavior.
		
		// Timer Limits
		public short timeLimit = 0;						// If set, game expires at this duration (in seconds).
		public short playDelay = 300;					// The number of frames that the game will wait before starting.
		
		// Arena Games
		public bool arena = false;						// Arena matches are played on arena fields.
		public bool arenaAllowHorizontal = false;		// Allow "Horizontal" Arenas
		public bool arenaAllowVertical = false;			// Allow "Vertical" Arenas
		public bool arenaAllowFields = false;			// Allow "Field" Arenas
		public bool arenaAllowRect = false;				// Allow "Rectangular" Arenas
	}
}
