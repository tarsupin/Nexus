using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class SpikeShoes : Shoes {

		public SpikeShoes( Character character ) : base( character ) {
			this.subType = (byte) ShoeSubType.Spike;
			this.IconTexture = "Shoes/Spike";
			this.subStr = "spike";
		}

		public override void UpdateCharacterStats(Character character) {
			character.stats.CanWallJump = true;
			character.stats.CanWallGrab = true;
		}
	}
}
