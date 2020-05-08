using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class BGTap : BGTile {

		public string[] Texture;

		public enum BGTapSubType : byte {
			TapBR = 0,
			TapGY = 1,
		}

		public BGTap() : base() {
			this.tileId = (byte)TileEnum.BGTap;
			this.CreateTextures();
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(actor is Character) {

				// Get the SubType
				uint gridId = room.tilemap.GetGridID(gridX, gridY);
				byte[] tileData = room.tilemap.GetTileDataAtGridID(gridId);
				byte subType = tileData[1];

				// Toggle the color-toggle that matches this tap type.
				room.ToggleColor(subType == (byte)BGTapSubType.TapBR);

				// Destroy This BGTap
				this.DestroyMainLayer(room, gridX, gridY);

				// Destroy all Neighbor BGTaps detected:
				this.DestroyNeighborTap(room, gridX, gridY);
			}

			return false;
		}

		public void DestroyNeighborTap(RoomScene room, ushort gridX, ushort gridY) {
			uint gridId = room.tilemap.GetGridID(gridX, gridY);
			byte[] tileData = room.tilemap.GetTileDataAtGridID(gridId);
			
			if(tileData[0] == (byte) TileEnum.BGTap) {
				room.tilemap.ClearMainLayer(gridId);
			}
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte)BGTapSubType.TapBR] = "BGTile/TapBR";
			this.Texture[(byte)BGTapSubType.TapGY] = "BGTile/TapGY";
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
