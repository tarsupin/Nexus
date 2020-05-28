
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WorldFuncButSave : WorldFuncBut {

		public WorldFuncButSave() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Small/Save";
			this.title = "Save";
			this.description = "Saves the level.";
		}

		public override void ActivateWorldFuncButton() {
			Systems.handler.worldContent.SaveWorld();
		}
	}
}
