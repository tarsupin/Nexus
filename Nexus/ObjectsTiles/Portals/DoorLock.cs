using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DoorLock : Door {

		public DoorLock() : base() {
			this.tileId = (byte)TileEnum.DoorLock;
			this.title = "Locked Door";
			this.description = "A door that must be unlocked before use.";
			this.moveParamSet = Params.ParamMap["Door"];
		}

		public override bool InteractWithDoor(Character character, RoomScene room, short gridX, short gridY) {

			// The door is locked. Must have a key, or prevent entry.
			if(!character.trailKeys.HasKey) {
				room.PlaySound(Systems.sounds.click3, 1f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
				return false;
			}

			// Check if the door is locked.
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);

			// Find Destination Door. If none exist, then no reason to unlock it.
			(RoomScene destRoom, short gridX, short gridY) doorLink = Door.GetLinkingDoor(room, subType, gridX, gridY);

			if(doorLink.gridX == 0 && doorLink.gridY == 0) {
				room.PlaySound(Systems.sounds.collectDisable, 0.4f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
				return false;
			}

			// Unlock Door
			Door.UnlockDoor(character, room, subType, gridX, gridY, true);

			return true;
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
