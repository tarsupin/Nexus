using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// TODO: Undecided Power
	public class FedoraHat : Hat {

		public FedoraHat( Character character ) : base(character, HatRank.PowerHat) {
			this.SpriteName = "Hat/FedoraHat";
		}

		public override void UpdateCharacterStats() {
			System.Console.WriteLine("GRANT NEW POWER TO FEDORA");
		}
	}
}
