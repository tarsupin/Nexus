
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WorldFuncButHome : WorldFuncBut {

		public WorldFuncButHome() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Small/Home";
			this.title = "Home";
			this.description = "Switches to the 'home' room, where the level begins.";
		}

		public override void ActivateWorldFuncButton() {
			WorldEditorScene scene = (WorldEditorScene)Systems.scene;
			scene.SwitchZone(0);
		}
	}
}
