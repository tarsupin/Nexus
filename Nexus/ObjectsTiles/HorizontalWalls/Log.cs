using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Log : HorizontalWall {

		public Log() : base() {
			this.BuildTextures("Log/");
			this.tileId = (byte)TileEnum.Log;
			this.title = "Log";
			this.description = "Hold `control` while clicking and dragging the mouse to Auto-Tile";
		}
	}
}
