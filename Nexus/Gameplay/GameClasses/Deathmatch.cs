﻿
namespace Nexus.Gameplay {

	public class Deathmatch : GameClass {

		public Deathmatch() {

            // Game Details
            this.arenaType = ArenaType.Battle;
            this.gameClassFlag = GameClassFlag.Deathmatch;
            this.title = "Deathmatch";
            this.description = "Score points by defeating your enemies.";

            // Players Allowed
            this.minPlayersAllowed = 2;
            this.maxPlayersAllowed = 16;

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
