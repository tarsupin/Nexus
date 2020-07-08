using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class RangerHat : Hat {

		public RangerHat() : base(HatRank.PowerHat) {
			this.subType = (byte)HatSubType.RangerHat;
			this.SpriteName = "Hat/RangerHat";
			this.subStr = "ranger";
		}

		public override void UpdateCharacterStats(Character character) {
			// TODO: Undecided Power; was originally fast-cast, but that makes more sense as a sorcery hat or something. Could be double weapon dist (handheld, etc).
			System.Console.WriteLine("GRANT NEW POWER TO RANGER HAT");
		}
	}
}
