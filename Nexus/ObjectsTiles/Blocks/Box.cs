using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Box : BlockTile {

		public string[] Texture;

		public enum BoxSubType : byte {
			Brown = 0,
			Gray = 1,
		}

		public Box() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.Box;
			this.title = "Box";
			this.description = "Destroyed when hit from beneath.";
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			
			// Destroy Box
			if(dir == DirCardinal.Up) {
				BlockTile.BreakApart(room, gridX, gridY);
				ExplodeEmitter.BoxExplosion(room, "Particles/WoodFrag", gridX * (byte)TilemapEnum.TileWidth + 24, gridY * (byte)TilemapEnum.TileHeight + 24);
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) BoxSubType.Brown] = "Box/Brown";
			this.Texture[(byte) BoxSubType.Gray] = "Box/Gray";
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
