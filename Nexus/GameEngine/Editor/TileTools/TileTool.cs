using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class EditorPlaceholder {
		public byte tileId;
		public byte subType;
	}

	public class TileTool {

		public List<EditorPlaceholder[]> placeholders;
		public int index = 0;
		public int subIndex = 0;

		public TileTool() {
			this.placeholders = new List<EditorPlaceholder[]>();
		}
	}
}
