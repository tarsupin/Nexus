﻿
namespace Nexus.Gameplay {

	public class Dodgeball : GameClass {

		public Dodgeball() {

            // Game Details
            this.arenaType = ArenaType.TeamArena;
            this.gameClassFlag = GameClassFlag.Dodgeball;
            this.title = "Dodgeball";
            this.description = "Dodge ball projectiles while knocking out your opponents.";

            // Players Allowed
            this.minPlayersAllowed = 6;
            this.maxPlayersAllowed = 16;

            // Game Behaviors
            this.teams = 2;
            this.pvp = true;

            // Respawns
            this.respawn = true;
            this.respawnFrames = 300;
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
