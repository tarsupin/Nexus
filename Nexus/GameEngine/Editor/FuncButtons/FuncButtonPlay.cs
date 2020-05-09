
namespace Nexus.GameEngine {

	public class FuncButtonPlay : FuncButton {

		public FuncButtonPlay() : base() {
			this.keyChar = "p";
			this.spriteName = "Icons/Play";
			this.title = "Play";
			this.description = "Saves the level, then initiates a playthrough.";
		}
	}
}
