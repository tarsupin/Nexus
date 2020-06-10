using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// TODO: Undecided Power
	public class FedoraHat : Hat {

		public FedoraHat() : base(HatRank.PowerHat) {
			this.SpriteName = "Hat/FedoraHat";
			this.subStr = "fedora";
		}

		public override void UpdateCharacterStats(Character character) {
			System.Console.WriteLine("GRANT NEW POWER TO FEDORA");
		}
	}
}
