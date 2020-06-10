
namespace Nexus.ObjectComponents {

	// Increases Jump Height and Duration
	public class CosmeticHat : Hat {

		public byte subType;

		public CosmeticHat( byte subType ) : base (HatRank.CosmeticHat ) {
			this.ApplySubTypes(subType);
			this.subStr = "";
		}

		private void ApplySubTypes(byte subType) {

			// Wizard Hats
			if(subType == (byte) HatSubType.WizBlue) { this.SpriteName = "Hat/WizBlueHat"; }
			if(subType == (byte) HatSubType.WizGreen) { this.SpriteName = "Hat/WizGreenHat"; }
			if(subType == (byte) HatSubType.WizRed) { this.SpriteName = "Hat/WizRedHat"; }
			if(subType == (byte) HatSubType.WizWhite) { this.SpriteName = "Hat/WizWhiteHat"; }

			// Mage Hats
			if(subType == (byte) HatSubType.MageBlack) { this.SpriteName = "Hat/MageBlackHat"; }
			if(subType == (byte) HatSubType.MageBlue) { this.SpriteName = "Hat/MageBlueHat"; }
			if(subType == (byte) HatSubType.MageGreen) { this.SpriteName = "Hat/MageGreenHat"; }
			if(subType == (byte) HatSubType.MageRed) { this.SpriteName = "Hat/MageRedHat"; }
			if(subType == (byte) HatSubType.MageWhite) { this.SpriteName = "Hat/MageWhiteHat"; }

			// Important Cosmetic Hats
			if(subType == (byte) HatSubType.PooHat) { this.SpriteName = "Hat/PooHat"; }

			// Miscellaneous Hats
			if(subType == (byte) HatSubType.BaseballHat) { this.SpriteName = "Hat/BaseballHat"; }
		}
	}
}
