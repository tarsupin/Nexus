using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	// TODO: Nudging is going to be complicated. Maybe hide the tile, create a particle effect.

	public class ExclaimBlock : BlockTile {

		public string[] Texture;

		public enum ExclaimBlockSubType : byte {
			Active = 0,
			Inactive = 1,
			Transparent = 2,
		}

		public ExclaimBlock() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.ExclaimBlock;
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Get the SubType
			uint gridId = room.tilemap.GetGridID(gridX, gridY);
			byte[] tileData = room.tilemap.GetTileDataAtGridID(gridId);
			byte subType = tileData[1];

			// Transparent Exclaim Blocks only Collide From Below
			if(subType == (byte) ExclaimBlockSubType.Transparent) {
				if(dir != DirCardinal.Up) { return false; }
			}

			// If hitting an active or transparent exclaim block from below:
			if(dir == DirCardinal.Up && subType != (byte) ExclaimBlockSubType.Inactive) {

				// Swap to an Inactive Block
				room.tilemap.tiles[gridId][1] = (byte)ExclaimBlockSubType.Inactive;

				Systems.sounds.shellThud.Play();
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) ExclaimBlockSubType.Active] = "Exclaim/Active";
			this.Texture[(byte) ExclaimBlockSubType.Inactive] = "Exclaim/Inactive";
			this.Texture[(byte) ExclaimBlockSubType.Transparent] = "Exclaim/Transparent";
		}
		
		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
