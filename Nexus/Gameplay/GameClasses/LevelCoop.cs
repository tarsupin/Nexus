﻿
namespace Nexus.Gameplay {

	public class LevelCoop : GameClass {

		public LevelCoop() {

            // Game Details
            this.arenaType = ArenaType.TeamLevel;
            this.gameClassFlag = GameClassFlag.LevelCoop;
            this.title = "Cooperative Level";
            this.description = "Traditional level playthrough, with cooperative partners.";

            // Players Allowed
            this.minPlayersAllowed = 2;
            this.maxPlayersAllowed = 4;

            // Game Behaviors
            this.teams = 1;
            this.pvp = false;

            // Respawns
            this.respawn = true;
            this.respawnFrames = 30;
            this.respawnInvincible = 30;
            this.respawnUntouchable = false;
            this.respawnType = RespawnType.Standard;

            // Timer Limits
            this.timeLimit = 600;
            this.playDelay = 300;

            // Arena Games
            this.arena = false;
            this.arenaAllowHorizontal = false;
            this.arenaAllowVertical = false;
            this.arenaAllowFields = false;
            this.arenaAllowRect = false;
        }
	}
}
