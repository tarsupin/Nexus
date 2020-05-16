using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Lock : BlockTile {

		public string Texture;

		public Lock() : base() {
			this.Texture = "Lock/Lock";
			this.tileId = (byte)TileEnum.Lock;
			this.title = "Lock";
			this.description = "A key will remove it, along with all neighboring locks.";
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(actor is Character) {
				// TODO: Special Lock Behavior, Unlock it if Character has a key.
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture, posX, posY);
		}
	}
}
