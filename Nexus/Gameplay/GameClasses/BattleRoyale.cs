
namespace Nexus.Gameplay {

	public class BattleRoyale : GameClass {

		public BattleRoyale() {

			// Game Details
			this.arenaType = ArenaType.Battle;
			this.gameClassFlag = GameClassFlag.BattleRoyale;
			this.title = "Battle Royale";
			this.description = "Survive the longest. Defeat your enemies for supplies.";

			// Players Allowed
			this.minPlayersAllowed = 4;
			this.maxPlayersAllowed = 16;

			// Game Behaviors
			this.teams = 0;
			this.pvp = true;

			// Respawns
			this.respawn = false;
			this.respawnFrames = 0;
			this.respawnInvincible = 0;
			this.respawnUntouchable = false;
			this.respawnType = RespawnType.Spectator;

			// Timer Limits
			this.timeLimit = 150;
			this.playDelay = 300;

			// Arena Games
			this.arena = true;
			this.arenaAllowHorizontal = false;
			this.arenaAllowVertical = false;
			this.arenaAllowFields = true;
			this.arenaAllowRect = true;
		}
	}
}
