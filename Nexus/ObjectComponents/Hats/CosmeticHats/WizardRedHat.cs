
namespace Nexus.ObjectComponents {

	// Has no effect; is just for style, or to accomodate a Suit.
	public class WizardRedHat : Hat {

		public WizardRedHat() : base(HatRank.CosmeticHat) {
			this.subType = (byte)HatSubType.WizRed;
			this.SpriteName = "Hat/WizRedHat";
		}
	}
}
