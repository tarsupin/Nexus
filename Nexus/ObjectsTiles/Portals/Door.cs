using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Door : TileGameObject {

		protected string[] Texture;
		protected DoorSubType subType;

		public enum DoorSubType : byte {
			Blue = 0,
			Green = 1,
			Red = 2,
			Yellow = 3,
			Open = 4,
		}

		public Door() : base() {
			this.CreateTextures();
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Door];
			this.tileId = (byte) TileEnum.Door;
			this.title = "Door";
			this.description = "Allows passage to another room or door.";
			this.paramSet =  Params.ParamMap["Door"];
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Characters interact with Door:
			if(actor is Character) {

				// TODO: Handle Parameter behavior for Doors.
				// TODO: instructions.destRoom, instructions.destType.

				// TODO: SHOW INTERACTION PROMPT FOR Door
				// TODO: Default message should be possible. Describe the door being entered.
				// TODO: Get Message Data. Hover it above the Door

				//byte subType = room.tilemap.GetMainSubType(gridX, gridY);

				Character character = (Character)actor;

				// The Character must be pressing the interaction key to open the chest.
				if(!character.input.isPressed(IKey.YButton)) { return false; }

			}

			return false;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}

		protected virtual void CreateTextures() {
			this.Texture = new string[5];
			this.Texture[(byte)DoorSubType.Open] = "Door/Standard/Open";
			this.Texture[(byte)DoorSubType.Blue] = "Door/Standard/Blue";
			this.Texture[(byte)DoorSubType.Green] = "Door/Standard/Green";
			this.Texture[(byte)DoorSubType.Red] = "Door/Standard/Red";
			this.Texture[(byte)DoorSubType.Yellow] = "Door/Standard/Yellow";
		}
	}
}
