
using Nexus.Gameplay;

namespace Nexus.ObjectComponents {

	public class CharacterStatus {

		// Action Properties
		public Action action;	// Reference to the action class being used.
		public uint actionEnds;         // The frame when the action ends.
		public sbyte actionNum1;		// Generic Action Property
		public sbyte actionNum2;		// Generic Action Property
		public bool actionBool1;        // Generic Action Property
		public bool actionBool2;        // Generic Action Property

		// Movement Statuses
		public byte jumpsUsed;          // The number of jumps currently used.
		public uint nextSlide;          // The frame that the next slide is allowed (or after).
		public uint leaveWall;			// The frame # until leeway for a wall jump is no longer allowed.
		public DirCardinal grabDir;		// The direction a wall has been grabbed.

		public CharacterStatus() {
			this.ResetCharacterStatus();
		}

		public void ResetCharacterStatus() {
			this.jumpsUsed = 0;
			this.nextSlide = 0;
			this.leaveWall = 0;
			this.grabDir = DirCardinal.Center;
		}
	}
}
