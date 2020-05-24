﻿
namespace Nexus.Gameplay {

	public class LevelRace : GameClass {

		public LevelRace() {

            // Game Details
            this.arenaType = ArenaType.Level;
            this.gameClassFlag = GameClassFlag.LevelRace;
            this.title = "Race";
            this.description = "Race through a level. You have competitors, but they can't hurt you.";

            // Players Allowed
            this.minPlayersAllowed = 2;
            this.maxPlayersAllowed = 4;

            // Game Behaviors
            this.teams = 0;
            this.pvp = false;

            // Respawns
            this.respawn = true;
            this.respawnFrames = 0;
            this.respawnInvincible = 90;
            this.respawnUntouchable = true;
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
