using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DoorLock : Door {
		
		public DoorLock() : base() {
			// TODO: Add Door Locked Behavior
			this.tileId = (byte)TileEnum.DoorLock;
			this.title = "Locked Door";
			this.description = "A door that must be unlocked before use.";
			this.moveParamSet = Params.ParamMap["Door"];
		}

		protected override void CreateTextures() {
			this.Texture = new string[4];
			this.Texture[(byte)DoorSubType.Blue] = "Door/Lock/Blue";
			this.Texture[(byte)DoorSubType.Green] = "Door/Lock/Green";
			this.Texture[(byte)DoorSubType.Red] = "Door/Lock/Red";
			this.Texture[(byte)DoorSubType.Yellow] = "Door/Lock/Yellow";
		}
	}
}
