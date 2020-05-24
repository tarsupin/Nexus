﻿
namespace Nexus.Gameplay {

	public enum ArenaSize : byte {
		Undeclared = 0,                 // Any Size.

		Horizontal1 = 1,                // 29x18 in size. Covers 1 page.
		Horizontal1Plus = 2,            // 44x18 in size. Covers 1.5 pages.
		Horizontal2 = 3,                // 58x18 in size. Covers 2 pages. Regulation field size?
		Horizontal2Plus = 4,            // 73x18 in size. Covers 2.5 pages.
		Horizontal3 = 5,                // 87x18 in size. Covers 3 pages.

		Vertical2 = 11,                 // 29x36 in size. Covers 2 vertical pages.
		Vertical3 = 12,                 // 29x54 in size. Covers 3 vertical pages.
		Vertical4 = 13,                 // 29x72 in size. Covers 4 vertical pages.

		Field1 = 21,                    // 40x22 in size.
		Field2 = 22,                    // 50x24 in size.
		Field3 = 23,                    // 60x26 in size.
		Field4 = 24,                    // 70x28 in size.
		Field5 = 25,                    // 80x30 in size.

		Rect1 = 31,                     // 50x25 in size.
		Rect2 = 32,                     // 70x35 in size.
		Rect3 = 33,                     // 90x45 in size.
	}

	public enum ArenaType : byte {

		Level = 1,              // Traditional level, or similar.
		TeamLevel = 2,          // Traditional level, or similar. Players cooperate to finish.

		Battle = 3,             // Players battle each other. Free for all.
		TeamBattle = 4,         // Players battle each other on teams. Work with allies, defeat enemies.

		SoloArena = 5,          // Arena match, usually played on a smaller map.
		TeamArena = 6,          // Arena match, usually played on a smaller map. Team game.

		// TERRITORY			// World Map where you compete over territories. Persistent Game.
		// GM Modes				// GM plays against players.
		// Tournaments			// Tournament Games
	}

	public enum League : byte {
		Unrated = 0,
		Training = 1,           // Players with very low talent. Bottom 20%. Probably children.
		Bronze = 2,             // Players with minimal talent. 20-40%. Likely young or inexperienced.
		Silver = 3,             // Players with minor talent. 40-60%.
		Gold = 4,               // Players with moderate talent. 60-80%.
		Platinum = 5,           // Players with significant talent. 80-90%.
		Diamond = 6,            // Players with extraordinary talent. 90-95%.
		Master = 7,             // Players that are best in the league. 95-99%
		Grandmaster = 8,        // Players that are indisputable champions. 99-100%.
	}

	// Game Type Groups contain multiple game types. Users can indicate these as preferences for what rooms are created.
	public enum GamePreference : byte {
		Undeclared = 0,
		Coop = 1,               // Coop, Roles, Survival
		Versus = 2,             // Versus, Versus Roles
		Battle = 3,             // Battle, Battle Royale
		Team = 4,               // Team Battle, Boss Battle, CTF, Football
	}

	public enum RoomType : byte {
		Standard = 1,       // Standard Room that gets generated automatically.
		Managed = 2,        // A managed room; one that was specifically created by intention. Player-created and managed.
		Persistent = 3,     // A persistent room. One that intends to remain persistent for some time.
	}

	public enum PlayerKarma : sbyte {
		Awful = -4,
		VeryLow = -3,
		Low = -2,
		Reduced = -1,
		None = 0,
		Known = 1,
		High = 2,
		VeryHigh = 3,
		Trusted = 4,
	}

	public enum PlayerRank : sbyte {
		Banned = -10,
		TemporarilyBanned = -5,
		Distrusted = -2,
		Unregistered = 0,
		Guest = 1,
		User = 2,
		PaidUser = 3,
		VIP = 4,
		Moderator = 6,
		Creator = 7,
		Admin = 8,
		Superuser = 10,
	}

	public enum RespawnType : byte {
		Standard = 0,           // The standard respawn; whichever location is appropriate for the game.
		Ghost = 1,              // Respawn as a ghost. Cannot interact, but may be visible.
		Spectator = 2,          // Respawn as a spectator. No interaction.
		NoRespawn = 3,          // Don't respawn.
	}
}
