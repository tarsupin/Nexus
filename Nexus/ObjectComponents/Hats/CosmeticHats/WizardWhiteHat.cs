﻿
namespace Nexus.ObjectComponents {

	// Has no effect; is just for style, or to accomodate a Suit.
	public class WizardWhiteHat : Hat {

		public WizardWhiteHat() : base(HatRank.CosmeticHat) {
			this.subType = (byte)HatSubType.WizWhite;
			this.SpriteName = "Hat/WizWhiteHat";
		}
	}
}
