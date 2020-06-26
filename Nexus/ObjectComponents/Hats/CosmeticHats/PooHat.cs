
namespace Nexus.ObjectComponents {

	// Has no effect; is just for style, or to accomodate a Suit.
	public class PooHat : Hat {

		public PooHat() : base(HatRank.CosmeticHat) {
			this.subType = (byte)HatSubType.PooHat;
			this.SpriteName = "Hat/PooHat";
		}
	}
}
