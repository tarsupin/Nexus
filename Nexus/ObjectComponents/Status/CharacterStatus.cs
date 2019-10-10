using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class CharacterStatus {

		// References
		private Character character;

		// Action Properties
		public ActionCharacter action;	// Reference to the action class being used.
		public uint actionEnds;         // The frame when the action ends.
		public sbyte actionNum1;		// Generic Action Property
		public sbyte actionNum2;		// Generic Action Property
		public bool actionBool1;        // Generic Action Property
		public bool actionBool2;        // Generic Action Property

		// Statuses
		public byte jumpsUsed;			// The number of jumps currently used.

		public CharacterStatus( Character character ) {
			this.character = character;
			this.ResetCharacterStatus();
		}

		public void ResetCharacterStatus() {

		}
	}
}
