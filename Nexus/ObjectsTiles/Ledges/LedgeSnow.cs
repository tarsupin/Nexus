using Nexus.Gameplay;

namespace Nexus.Objects {

	public class LedgeSnow : Ledge {

		public LedgeSnow() : base() {
			this.BuildTextures("SnowLedge/");
			this.tileId = (byte)TileEnum.LedgeSnow;
		}
	}
}
