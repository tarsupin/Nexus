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

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(actor is Character) {

				// Get the SubType
				byte subType = room.tilemap.GetMainSubType(gridX, gridY);

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
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);

			if(subType == (byte) TileEnum.BGTap) {
				room.tilemap.ClearMainLayer(gridX, gridY);
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
