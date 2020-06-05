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
			this.title = "Color Toggle Tap";
			this.description = "Toggles color blocks, then disappears along with its neighbors.";
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(actor is Character) {

				// Get the SubType
				byte subType = room.tilemap.GetMainSubType(gridX, gridY);

				// Toggle the color-toggle that matches this tap type.
				room.ToggleColor(subType == (byte)BGTapSubType.TapBR);

				// Destroy This BGTap (and any nearby)
				this.RemoveTap(room, gridX, gridY, subType);
			}

			return false;
		}

		private void RemoveTap(RoomScene room, ushort gridX, ushort gridY, byte subType) {

			// Check if the tile exists:
			byte[] td = room.tilemap.GetTileDataAtGrid(gridX, gridY);
			if(td == null) { return; }

			// If the tile is a BGTap, destroy it, and then run DestroyNeighborTap on it.
			if(td[0] != this.tileId || td[1] != subType) { return; }

			// Destroy This BGTap
			room.tilemap.ClearMainLayer(gridX, gridY);

			// Destroy all Neighbor BGTaps of the same subtype:
			this.RemoveTap(room, (ushort)(gridX - 1), gridY, subType);
			this.RemoveTap(room, (ushort)(gridX + 1), gridY, subType);
			this.RemoveTap(room, gridX, (ushort)(gridY - 1), subType);
			this.RemoveTap(room, gridX, (ushort)(gridY + 1), subType);
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
