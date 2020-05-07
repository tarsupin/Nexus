using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Chest : TileGameObject {

		protected string Texture;
		protected ChestSubType subType;

		public enum ChestSubType : byte {
			Closed = 0,
			Open = 1,
			Locked = 2,
		}

		public Chest() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Interactives];
			this.tileId = (byte)TileEnum.Chest;

			// Chest Contents
			// TODO: Update Chest Contents with Param system. Needs to work with tiles.
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			
			// Characters interact with Chest:
			if(actor is Character) {

				uint gridId = room.tilemap.GetGridID(gridX, gridY);
				byte[] tileData = room.tilemap.GetTileDataAtGridID(gridId);
				byte subType = tileData[1];

				// If the chest is open, no interactions are allowed:
				if(subType == (byte) ChestSubType.Open) { return false; }

				Character character = (Character) actor;

				// Make sure the character is fully within the chest tile.
				if(!CollideTile.IsWithinTile(character, gridX, gridY)) { return false; }

				// The Character must be pressing the interaction key to open the chest.
				if(!character.input.isPressed(IKey.YButton)) { return false; }

				// If the chest is locked, must have a key:
				if(subType == (byte) ChestSubType.Locked) {
					// TODO: Must have a key to open chest.
					return false;
				}

				this.OpenChest(room, gridId);
			}

			return false;
		}

		protected void OpenChest(RoomScene room, uint gridId) {

			// Open the Chest
			room.tilemap.tiles[gridId][1] = (byte)ChestSubType.Open;

			// Provide the Opened Chest Items
			// TODO: MUST PROVIDE OPENED CHEST ITEMS
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture, posX, posY);
		}
	}
}
