using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Spike : BlockTile {

		public string[] Texture;

		public enum  SpikeSubType {
			Basic = 0,
			Lethal = 1,
		}

		public Spike() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.Spike;
			this.title = "Spike Block";
			this.description = "Damages the character.";
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Characters Receive Spike Damage
			if(actor is Character) {
				(actor as Character).wounds.ReceiveWoundDamage(DamageStrength.Standard);
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) SpikeSubType.Basic] = "Spike/Basic";
			this.Texture[(byte) SpikeSubType.Lethal] = "Spike/Lethal";
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
