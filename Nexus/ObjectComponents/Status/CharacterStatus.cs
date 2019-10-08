using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class CharacterStatus {

		// References
		private Character character;



		public CharacterStatus( Character character ) {
			this.character = character;
			this.ResetCharacterStatus();
		}

		public void ResetCharacterStatus() {

		}
	}
}
