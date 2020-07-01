
namespace Nexus.Gameplay {

	public static class GameplayTypes {
		public static string[] DiffName = new string[] { "", "Easy", "Normal", "Hard", "Expert", "Master" };
		public static string[] HardcoreName = new string[] { "Softcore", "Mediumcore", "Hardcore", "Punishing", "Brutal", "Nightmare", "Hell" };

		public static string[] HardcoreDesc = new string[] {
			"Infinite continues. No penalties for death.",
			"Some penalties for death. Nothing too severe.",
			"Lose all progress on death. Restart at beginning of world.",
			"On death you lose all progress and cannot replay the world for 48 hours.",
			"On death you lose all progress and cannot replay the world for two weeks.",
			"Cannot play other worlds until completion or death. On death your account is deactivated for 48 hours.",
			"Cannot play other worlds until completion or death. On death your account is deactivated for two weeks.",
		};
	}

	public enum DifficultyMode : short {
		NotApplicable = 0,		// Casual Gamers should be able to play through with a high success rate.
		Easy = 1,				// Casual Gamers should be able to play through with a high success rate.
		Normal = 2,				// Casual Gamers should feel a challenge, but nothing too overwhelming.
		Hard = 3,				// Casual Gamers may have a hard time beating these levels, but could do it with practice.
		Expert = 4,				// Built for more experienced players with high skill levels.
		Master = 5,				// Built for genuine experts at the game. Very difficult.
	}
	
	public enum HardcoreMode : short {
		SoftCore = 0,		// Infinite Continues. No penalties for death.
		MediumCore = 1,		// Some penalties for death.
		HardCore = 2,		// Lose progress on death. Restart at beginning of world.
		Punishing = 3,		// Lose all progress on death. Cannot retry world for 48 hours.
		Brutal = 4,			// Lose all progress on death. Cannot retry world for 2 weeks.
		Nightmare = 5,      // Lose all progress on death and account is deactivated for 48 hours. Cannot play alternative levels until completed.
		Hell = 6,			// Lose all progress on death and account is deactivated for two weeks. Cannot play alternative levels until completed.
	}
}
