
namespace Nexus.Gameplay {

	public enum DifficultyMode : short {
		Easy = 0,		// Casual Gamers should be able to play through with a high success rate.
		Normal = 1,		// Casual Gamers should feel a challenge, but nothing too overwhelming.
		Hard = 2,		// Casual Gamers may have a hard time beating these levels, but could do it with practice.
		Expert = 3,		// Built for more experienced players with high skill levels.
		Master = 4,		// Built for genuine experts at the game. Very difficult.
	}
	
	public enum HardcoreMode : short {
		SoftCore = 0,		// Infinite Continues. No penalties for death.
		MediumCore = 1,		// Some penalties for death.
		HardCore = 2,		// Lose progress on death. Restart at beginning of world.
		Punishing = 3,		// Lose all progress on death and cannot try again for 48 hours.
	}
}
