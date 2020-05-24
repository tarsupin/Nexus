
namespace Nexus.Gameplay {

	public class TreasureHunt : GameClass {

		public TreasureHunt() {

            // Game Details
            this.arenaType = ArenaType.SoloArena;
            this.gameClassFlag = GameClassFlag.TreasureHunt;
            this.title = "Treasure Hunt";
            this.description = "Collect the most gems to achieve victory.";

            // Players Allowed
            this.minPlayersAllowed = 2;
            this.maxPlayersAllowed = 4;

            // Game Behaviors
            this.teams = 0;
            this.pvp = false;

            // Respawns
            this.respawn = true;
            this.respawnFrames = 60;
            this.respawnInvincible = 120;
            this.respawnUntouchable = true;
            this.respawnType = RespawnType.Standard;

            // Timer Limits
            this.timeLimit = 150;
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
