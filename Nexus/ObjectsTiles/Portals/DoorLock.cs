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
			this.paramSets = new Params[1] { Params.ParamMap["Door"] };
		}

		protected override void CreateTextures() {
			this.Texture = new string[4];
			this.Texture[(byte)DoorSubType.Blue] = "Door/Locked/Blue";
			this.Texture[(byte)DoorSubType.Green] = "Door/Locked/Green";
			this.Texture[(byte)DoorSubType.Red] = "Door/Locked/Red";
			this.Texture[(byte)DoorSubType.Yellow] = "Door/Locked/Yellow";
		}
	}
}
