using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class NPC : TileGameObject {

		public string[] Texture;
		protected NPCSubType subType;

		public enum NPCSubType : byte {
			Guy = 0,
			Girl = 1,

			NinjaBlack = 2,
			NinjaBlue = 3,
			NinjaGreen = 4,
			NinjaRed = 5,
			NinjaWhite = 6,
			NinjaMaster = 7,

			WizardBlue = 8,
			WizardGreen = 9,
			WizardRed = 10,
			WizardWhite = 11,
		}

		public NPC() : base() {
			this.collides = true;
			this.CreateTextures();
			this.Meta = Systems.mapper.MetaList[MetaGroup.Interactives];
			this.tileId = (byte)TileEnum.NPC;
			this.title = "NPC";
			this.description = "Can 'speak' to the character and allow simple interactions.";
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Characters interact with NPC:
			if(actor is Character) {

				// TODO: SHOW INTERACTION PROMPT FOR NPC
				// TODO: Default message should be possible. No need to have a full menu. Just show a brief message.

				// TODO: Get Message Data. Hover it above the NPC
				// TODO: Allow cycling through it with Interaction. Maybe a more advanced option later.

				// byte subType = room.tilemap.GetMainSubType(gridX, gridY);

				Character character = (Character)actor;

				// The Character must be pressing the interaction key to open the chest.
				if(!character.input.isPressed(IKey.YButton)) { return false; }

			}

			return false;
		}

		private void CreateTextures() {
			this.Texture = new string[12];
			this.Texture[(byte)NPCSubType.Guy] = "NPC/Guy";
			this.Texture[(byte)NPCSubType.Girl] = "NPC/Girl";
			this.Texture[(byte)NPCSubType.NinjaBlack] = "NPC/BlackNinja";
			this.Texture[(byte)NPCSubType.NinjaBlue] = "NPC/BlueNinja";
			this.Texture[(byte)NPCSubType.NinjaGreen] = "NPC/GreenNinja";
			this.Texture[(byte)NPCSubType.NinjaRed] = "NPC/RedNinja";
			this.Texture[(byte)NPCSubType.NinjaWhite] = "NPC/WhiteNinja";
			this.Texture[(byte)NPCSubType.NinjaMaster] = "NPC/MasterNinja";
			this.Texture[(byte)NPCSubType.WizardBlue] = "NPC/BlueWizard";
			this.Texture[(byte)NPCSubType.WizardGreen] = "NPC/GreenWizard";
			this.Texture[(byte)NPCSubType.WizardRed] = "NPC/RedWizard";
			this.Texture[(byte)NPCSubType.WizardWhite] = "NPC/WhiteWizard";
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
