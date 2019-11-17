using Nexus.Gameplay;

namespace Nexus.Objects {

	public class LedgeGrass : Ledge {

		public LedgeGrass() : base() {
			this.BuildTextures("GrassLedge/");
			this.tileId = (byte)TileEnum.LedgeGrass;
		}
	}
}
