using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PuffBlockMini : PuffBlock {

		public PuffBlockMini() : base() {
			this.tileId = (byte) TileEnum.PuffBlockMini;
			this.Texture = "Puff/Mini";
			this.title = "Mini-Puff Block";
			this.description = "Touching it causes the character to burst in the designated direction.";
			this.puffDuration = -6;
		}
	}
}
