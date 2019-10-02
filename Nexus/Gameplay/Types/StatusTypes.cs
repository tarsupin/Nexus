/*
 * Status Types are applied to Game Objects.
 * 
 * Status Types include flags that are set separately from SubTypes, and which must be tracked by the level data (as a parameter).
 * For example, the type of Flight motion that a "Buzz" enemy takes is a Status, since it must be tracked.
 * 
 * Damage would NOT be considered a Status Type, since Damage does not need to be tracked as a separate flag.
 * ToggleType would NOT be considered a Status Type, since its identity is known by the object's SubType.
 */

namespace Nexus.Gameplay {

	public enum FlightMovement : byte {
		Axis = 1,
		Quadratic = 2,
		Circle = 3,
		To = 4,
		Track = 5,
		Chase = 6,
		Character = 7,
		Screen = 8,
	}
}
