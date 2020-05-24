
namespace Nexus.Gameplay {

	public class GhostTown : GameClass {

		public GhostTown() {

            // Game Details
            this.arenaType = ArenaType.TeamBattle;
            this.gameClassFlag = GameClassFlag.GhostTown;
            this.title = "Ghost Town";
            this.description = "Collect gems while surviving team battle and environmental hazards.";

            // Players Allowed
            this.minPlayersAllowed = 6;
            this.maxPlayersAllowed = 12;

            // Game Behaviors
            this.teams = 2;
            this.pvp = true;

            // Respawns
            this.respawn = true;
            this.respawnFrames = 600;
            this.respawnInvincible = 60;
            this.respawnUntouchable = true;
            this.respawnType = RespawnType.Standard;

            // Timer Limits
            this.timeLimit = 240;
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
