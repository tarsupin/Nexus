using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

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
			this.title = "Exclaim Block";
			this.description = "Hit from beneath to activate.";
		}

		public override bool RunImpact(RoomScene room, GameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Get the SubType
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);

			// Transparent Exclaim Blocks only Collide From Below
			if(subType == (byte) ExclaimBlockSubType.Transparent) {
				if(dir != DirCardinal.Up) { return false; }
			}

			// If hitting an active or transparent exclaim block from below:
			if(dir == DirCardinal.Up && subType != (byte) ExclaimBlockSubType.Inactive) {

				// Swap to an Inactive Block
				room.tilemap.SetTileSubType(gridX, gridY, (byte)ExclaimBlockSubType.Inactive);

				Systems.sounds.shellThud.Play();
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		private void CreateTextures() {
			this.Texture = new string[3];
			this.Texture[(byte) ExclaimBlockSubType.Active] = "Exclaim/Active";
			this.Texture[(byte) ExclaimBlockSubType.Inactive] = "Exclaim/Inactive";
			this.Texture[(byte) ExclaimBlockSubType.Transparent] = "Exclaim/Transparent";
		}
		
		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
