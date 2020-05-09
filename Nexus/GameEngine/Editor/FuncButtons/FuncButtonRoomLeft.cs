
namespace Nexus.GameEngine {

	public class FuncButtonRoomLeft : FuncButton {

		public FuncButtonRoomLeft() : base() {
			this.keyChar = "";
			this.spriteName = "Icons/Left";
			this.title = "Switch Room Left";
			this.description = "Switches the active editing room to one room ID lower.";
		}
	}
}
