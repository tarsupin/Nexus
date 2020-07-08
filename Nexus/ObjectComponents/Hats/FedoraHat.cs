using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Fedora : Hat {

		public Fedora() : base(HatRank.PowerHat) {
			this.subType = (byte)HatSubType.Fedora;
			this.SpriteName = "Hat/Fedora";
			this.subStr = "fedora";
		}

		public override void UpdateCharacterStats(Character character) {
			// TODO: Undecided Power
			System.Console.WriteLine("GRANT NEW POWER TO FEDORA");
		}
	}
}
