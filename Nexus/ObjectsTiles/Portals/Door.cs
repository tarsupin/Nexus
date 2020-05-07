using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Door : TileGameObject {

		protected string Texture;
		protected DoorSubType subType;

		public enum DoorSubType : byte {
			Normal = 0,
			BlueDoor = 1,
			GreenDoor = 2,
			RedDoor = 3,
			YellowDoor = 4,
		}

		public Door() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Door];
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Characters interact with Door:
			if(actor is Character) {

				// TODO: Handle Parameter behavior for Doors.
				// TODO: instructions.destRoom, instructions.destType.

				// TODO: SHOW INTERACTION PROMPT FOR Door
				// TODO: Default message should be possible. Describe the door being entered.
				// TODO: Get Message Data. Hover it above the Door

				//uint gridId = room.tilemap.GetGridID(gridX, gridY);
				//byte[] tileData = room.tilemap.GetTileDataAtGridID(gridId);
				//byte subType = tileData[1];

				Character character = (Character)actor;

				// The Character must be pressing the interaction key to open the chest.
				if(!character.input.isPressed(IKey.YButton)) { return false; }

			}

			return false;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture, posX, posY);
		}
	}
}
