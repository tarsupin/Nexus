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

			// Helper Texts
			this.titles = new string[2];
			this.titles[(byte)SpikeSubType.Basic] = "Spike Block";
			this.titles[(byte)SpikeSubType.Lethal] = "Lethal Spike Block";

			this.descriptions = new string[2];
			this.descriptions[(byte)SpikeSubType.Basic] = "Wounds a character that touches it.";
			this.descriptions[(byte)SpikeSubType.Lethal] = "Kills a character that touches it.";
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Characters Receive Spike Damage
			if(actor is Character) {

				// Lethal Spikes will force-kill, even if you're currently invincible.
				byte subType = room.tilemap.GetMainSubType(gridX, gridY);
				(actor as Character).wounds.ReceiveWoundDamage(subType == (byte)SpikeSubType.Lethal ? DamageStrength.Forced : DamageStrength.Standard);
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
