using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Spike : BlockTile {

		public string[] Texture;

		public enum  SpikeSubType {
			Basic = 0,
			Lethal = 1,
		}

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte)TileEnum.Spike)) {
				new Spike(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte)TileEnum.Spike, subTypeId);
		}

		public Spike(RoomScene room) : base(room, TileEnum.Spike) {
			this.CreateTextures();
		}

		public override bool RunImpact(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Characters Receive Spike Damage
			if(actor is Character) {
				(actor as Character).wounds.ReceiveWoundDamage(DamageStrength.Standard);
			}

			return base.RunImpact(actor, gridX, gridY, dir);
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) SpikeSubType.Basic] = "Spike/Basic";
			this.Texture[(byte) SpikeSubType.Lethal] = "Spike/Lethal";
		}

		public override void Draw(byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
