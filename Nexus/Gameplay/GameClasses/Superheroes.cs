
namespace Nexus.Gameplay {

	public class Superheroes : GameClass {

		public Superheroes() {

            // Game Details
            this.arenaType = ArenaType.TeamLevel;
            this.gameClassFlag = GameClassFlag.Superheroes;
            this.title = "Superheroes";
            this.description = "Traditional level, but everyone is a superhero.";

            // Players Allowed
            this.minPlayersAllowed = 4;
            this.maxPlayersAllowed = 4;

            // Game Behaviors
            this.teams = 1;
            this.pvp = false;

            // Respawns
            this.respawn = true;
            this.respawnFrames = 600;
            this.respawnInvincible = 120;
            this.respawnUntouchable = true;
            this.respawnType = RespawnType.Standard;

            // Timer Limits
            this.timeLimit = 300;
            this.playDelay = 300;

            // Arena Games
            this.arena = true;
            this.arenaAllowHorizontal = true;
            this.arenaAllowVertical = true;
            this.arenaAllowFields = true;
            this.arenaAllowRect = true;
        }
	}
}
