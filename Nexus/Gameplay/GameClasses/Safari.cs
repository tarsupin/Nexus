﻿
namespace Nexus.Gameplay {

	public class Safari : GameClass {

		public Safari() {

            // Game Details
            this.arenaType = ArenaType.TeamLevel;
            this.gameClassFlag = GameClassFlag.Safari;
            this.title = "Safari";
            this.description = "Collect all of them gems while surviving against elemental hazards.";

            // Players Allowed
            this.minPlayersAllowed = 2;
            this.maxPlayersAllowed = 4;

            // Game Behaviors
            this.teams = 1;
            this.pvp = false;

            // Respawns
            this.respawn = true;
            this.respawnFrames = 300;
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
