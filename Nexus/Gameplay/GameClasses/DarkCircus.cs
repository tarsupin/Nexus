
namespace Nexus.Gameplay {

	public class DarkCircus : GameClass {

		public DarkCircus() {

            // Game Details
            this.arenaType = ArenaType.Battle;
            this.gameClassFlag = GameClassFlag.DarkCircus;
            this.title = "Dark Circus";
            this.description = "Collect the most gems while surviving battle to achieve victory.";

            // Players Allowed
            this.minPlayersAllowed = 3;
            this.maxPlayersAllowed = 4;

            // Game Behaviors
            this.teams = 0;
            this.pvp = true;

            // Respawns
            this.respawn = true;
            this.respawnFrames = 300;
            this.respawnInvincible = 120;
            this.respawnUntouchable = true;
            this.respawnType = RespawnType.Standard;

            // Timer Limits
            this.timeLimit = 180;
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
