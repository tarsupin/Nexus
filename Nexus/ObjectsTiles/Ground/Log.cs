using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Log : Ground {

		public Log() : base() {
			this.BuildTextures("Log/");
			this.tileId = (byte)TileEnum.Log;
		}
	}
}
