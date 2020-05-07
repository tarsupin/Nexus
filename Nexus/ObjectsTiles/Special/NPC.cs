using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class NPC : TileGameObject {

		protected string Texture;
		protected NPCSubType subType;

		public enum NPCSubType : byte {
			NPCGuy = 0,
			NPCGirl = 5,

			NPCNinjaBlack = 10,
			NPCNinjaBlue = 11,
			NPCNinjaGreen = 12,
			NPCNinjaRed = 13,
			NPCNinjaWhite = 14,
			NPCNinjaMaster = 15,

			NPCWizardBlue = 20,
			NPCWizardGreen = 21,
			NPCWizardRed = 22,
			NPCWizardWhite = 23,
		}

		public NPC() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Interactives];
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Characters interact with NPC:
			if(actor is Character) {

				// TODO: SHOW INTERACTION PROMPT FOR NPC
				// TODO: Default message should be possible. No need to have a full menu. Just show a brief message.

				// TODO: Get Message Data. Hover it above the NPC
				// TODO: Allow cycling through it with Interaction. Maybe a more advanced option later.

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
