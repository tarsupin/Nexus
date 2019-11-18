using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class EditorPlaceholder {
		public byte tileId;
		public byte subType;
	}

	public class TileTool {

		private readonly EditorScene scene;
		public List<EditorPlaceholder[]> placeholders;
		public int index = 0;
		public int subIndex = 0;

		public TileTool( EditorScene scene ) {
			this.scene = scene;
			this.placeholders = new List<EditorPlaceholder[]>();
		}
	}
}
