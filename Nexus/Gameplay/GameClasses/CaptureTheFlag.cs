﻿
namespace Nexus.Gameplay {

	public class CaptureTheFlag : GameClass {

		public CaptureTheFlag() {

            // Game Details
            this.arenaType = ArenaType.TeamArena;
            this.gameClassFlag = GameClassFlag.CaptureTheFlag;
            this.title = "Capture The Flag";
            this.description = "Capture the enemy's flag while defending your own.";

            // Players Allowed
            this.minPlayersAllowed = 4;
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
            this.timeLimit = 300;
            this.playDelay = 300;

            // Arena Games
            this.arena = true;
            this.arenaAllowHorizontal = true;
            this.arenaAllowVertical = false;
            this.arenaAllowFields = true;
            this.arenaAllowRect = false;
        }
	}
}
