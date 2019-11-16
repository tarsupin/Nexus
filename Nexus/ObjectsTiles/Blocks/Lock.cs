using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Lock : BlockTile {

		public string Texture;

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.Lock)) {
				new Lock(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.Lock, subTypeId);
		}

		public Lock(RoomScene room) : base(room, TileEnum.Lock) {
			this.Texture = "Lock/Lock";
		}

		public override bool RunImpact(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(actor is Character) {
				// TODO: Special Lock Behavior, Unlock it if Character has a key.
			}

			return base.RunImpact(actor, gridX, gridY, dir);
		}

		public override void Draw(byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture, posX, posY);
		}
	}
}
