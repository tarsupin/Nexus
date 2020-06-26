
namespace Nexus.ObjectComponents {

	// Has no effect; is just for style, or to accomodate a Suit.
	public class WizardGreenHat : Hat {

		public WizardGreenHat() : base(HatRank.CosmeticHat) {
			this.subType = (byte)HatSubType.WizGreen;
			this.SpriteName = "Hat/WizGreenHat";
		}
	}
}
