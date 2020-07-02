using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class DashingShoes : Shoes {

		public DashingShoes( Character character ) : base( character ) {
			this.subType = (byte) ShoeSubType.Dashing;
			this.IconTexture = "Shoes/Red";
			this.subStr = "dashing";
		}
	}
}
