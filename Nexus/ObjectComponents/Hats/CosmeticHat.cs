
namespace Nexus.ObjectComponents {

	// Increases Jump Height and Duration
	public class CosmeticHat : Hat {

		public CosmeticHat( byte subType ) : base (HatRank.CosmeticHat ) {
			this.ApplySubTypes(subType);
			this.subStr = "";
		}

		private void ApplySubTypes(byte subType) {

			// Wizard Hats
			if(subType == (byte) HatSubType.WizBlue) {
				this.subType = (byte)HatSubType.WizBlue;
				this.SpriteName = "Hat/WizBlue";
			}
			
			else if(subType == (byte) HatSubType.WizGreen) {
				this.subType = (byte)HatSubType.WizGreen;
				this.SpriteName = "Hat/WizGreen";
			}
			
			else if(subType == (byte) HatSubType.WizRed) {
				this.subType = (byte)HatSubType.WizRed;
				this.SpriteName = "Hat/WizRed";
			}
			
			else if(subType == (byte) HatSubType.WizWhite) {
				this.subType = (byte)HatSubType.WizWhite;
				this.SpriteName = "Hat/WizWhite";
			}

			// Important Cosmetic Hats
			else if(subType == (byte) HatSubType.PooHat) {
				this.subType = (byte)HatSubType.PooHat;
				this.SpriteName = "Hat/PooHat"; this.hatRank = HatRank.BaseHat;
			}

			// Miscellaneous Hats
			else if(subType == (byte) HatSubType.BaseballHat) {
				this.subType = (byte)HatSubType.BaseballHat;
				this.SpriteName = "Hat/BaseballHat";
			}
		}
	}
}
