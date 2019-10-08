/*
 * Object Types are applied to Game Objects, covering types that do not fall under the "StatusTypes" category.
 */

namespace Nexus.Gameplay {

	public enum DamageStrength : byte {
		None = 0,                   // Does no damage.
		Trivial = 1,                // 
		Minor = 2,                  // 
		Standard = 3,               // Destroys most creatures, unless shielded.
		Major = 4,                  // 
		Lethal = 5,                 // Lethal to most enemies.
		Forced = 6,                 // Forces a death, despite other values.
	}

	public enum DeathResult : byte {
		Squish = 0,
		Knockout = 1,
		Special = 2,
	}

	public enum DeathCause : byte {
		Squish = 0,
		Power = 1,
		TNT = 2,
		Other = 3,
	}

	public enum TileSize : byte {
		Single,
		Wide,
		Tall,
		Large,
	}

	public enum ToggleType : byte {
		BR = 1,
		GY = 2,
	}
}
