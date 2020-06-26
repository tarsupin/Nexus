
namespace Nexus.ObjectComponents {

	// Has no effect; is just for style, or to accomodate a Suit.
	public class WizardBlueHat : Hat {

		public WizardBlueHat() : base(HatRank.CosmeticHat) {
			this.subType = (byte)HatSubType.WizBlue;
			this.SpriteName = "Hat/WizBlueHat";
		}
	}
}
