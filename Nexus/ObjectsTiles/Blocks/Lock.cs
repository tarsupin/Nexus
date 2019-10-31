using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Lock : BlockTile {

		public string Texture;

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsClassGameObjectRegistered((byte) TileGameObjectId.Lock)) {
				new Lock(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.Lock, subTypeId);
		}

		public Lock(RoomScene room) : base(room, TileGameObjectId.Lock) {
			this.Texture = "Lock/Lock";
		}

		public override bool RunImpact(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			TileSolidImpact.RunImpact(actor, gridX, gridY, dir);

			if(actor is Character) {

				// Standard Character Tile Collisions
				TileCharBasicImpact.RunImpact((Character)actor, dir);
			}

			return true;
		}

		public override void Draw(byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture, posX, posY);
		}
	}
}
