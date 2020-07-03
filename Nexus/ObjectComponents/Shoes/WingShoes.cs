using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class WingShoes : Shoes {

		public WingShoes( Character character ) : base( character ) {
			this.subType = (byte) ShoeSubType.Wing;
			this.IconTexture = "Shoes/Wing";
			this.subStr = "wing";
			this.duration = 11;
		}
	}
}
