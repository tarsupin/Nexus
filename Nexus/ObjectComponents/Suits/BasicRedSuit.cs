
namespace Nexus.ObjectComponents {

	public class BasicRedSuit : Suit {

		public BasicRedSuit() : base(SuitRank.BaseSuit, "BasicChar") {
			this.subType = (byte) SuitSubType.RedBasic;
			this.baseStr = "basic";
			this.subStr = "red";
		}
	}
}
