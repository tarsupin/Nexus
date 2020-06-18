
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
			if(subType == (byte) HatSubType.WizBlue) { this.SpriteName = "Hat/WizBlue"; }
			if(subType == (byte) HatSubType.WizGreen) { this.SpriteName = "Hat/WizGreen"; }
			if(subType == (byte) HatSubType.WizRed) { this.SpriteName = "Hat/WizRed"; }
			if(subType == (byte) HatSubType.WizWhite) { this.SpriteName = "Hat/WizWhite"; }

			// Important Cosmetic Hats
			if(subType == (byte) HatSubType.PooHat) { this.SpriteName = "Hat/PooHat"; this.hatRank = HatRank.BaseHat; }

			// Miscellaneous Hats
			if(subType == (byte) HatSubType.BaseballHat) { this.SpriteName = "Hat/BaseballHat"; }
		}
	}
}
