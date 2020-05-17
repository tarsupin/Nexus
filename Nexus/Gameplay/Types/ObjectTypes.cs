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

	public enum ToggleColor: byte {
		Blue = 1,
		Red = 2,
		Green = 3,
		Yellow = 4,
	}

	public enum ButtonSubTypes : byte {
		BR,
		BRDown,
		BROff,
		BROffDown,
		GY,
		GYDown,
		GYOff,
		GYOffDown,
	}

	public enum FlightChaseAxis : byte {
		Both = 0,
		Horizontal = 1,
		Vertical = 2,
	}

	/*
	 * Status Types are applied to Game Objects.
	 * 
	 * Status Types include flags that are set separately from SubTypes, and which must be tracked by the level data (as a parameter).
	 * For example, the type of Flight motion that a "Buzz" enemy takes is a Status, since it must be tracked.
	 * 
	 * Damage would NOT be considered a Status Type, since Damage does not need to be tracked as a separate flag.
	 * ToggleType would NOT be considered a Status Type, since its identity is known by the object's SubType.
	 */

	public enum FlightMovement : byte {
		None = 0,
		Axis = 1,
		Quadratic = 2,
		Circle = 3,
		To = 4,
		Track = 5,
		Chase = 6,
		Character = 7,
		Screen = 8,
	}

	public enum DoorExitType : byte {
		ToSameColor = 0,
		ToOpenDoor = 1,
		ToCheckpoint = 2,
	}
}
