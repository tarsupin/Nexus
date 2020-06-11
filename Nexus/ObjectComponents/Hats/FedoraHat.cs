using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// TODO: Undecided Power
	public class Fedora : Hat {

		public Fedora() : base(HatRank.PowerHat) {
			this.SpriteName = "Hat/Fedora";
			this.subStr = "fedora";
		}

		public override void UpdateCharacterStats(Character character) {
			System.Console.WriteLine("GRANT NEW POWER TO FEDORA");
		}
	}
}
