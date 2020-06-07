using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Leaf : BlockTile {

		public string[] Texture;

		public enum LeafSubType : byte {
			Basic = 0,
			Reform = 1,
		}

		public Leaf() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.Leaf;

			// Helper Texts
			this.titles = new string[2];
			this.titles[(byte)LeafSubType.Basic] = "Leaf Block";
			this.titles[(byte)LeafSubType.Reform] = "Reforming Leaf Block";

			this.descriptions = new string[2];
			this.descriptions[(byte)LeafSubType.Basic] = "Shatters a short duration after standing on it.";
			this.descriptions[(byte)LeafSubType.Reform] = "Shatters a short duration after standing on it, but reforms seconds later.";
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// byte subType = room.tilemap.GetMainSubType(gridX, gridY);

			// Destroy Leaf
			if(dir == DirCardinal.Up) {
				BlockTile.BreakApart(room, gridX, gridY);
				ExplodeEmitter.BoxExplosion(room, "Particles/Leaf", gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth, gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.HalfHeight);
				Systems.sounds.thudTap.Play();

			} else if(dir == DirCardinal.Down) {

				// TODO: Add a delay to the leaf at this gridId.
				BlockTile.BreakApart(room, gridX, gridY);
				ExplodeEmitter.BoxExplosion(room, "Particles/Leaf", gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth, gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.HalfHeight);
				Systems.sounds.thudTap.Play();
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) LeafSubType.Basic] = "Leaf/Basic";
			this.Texture[(byte) LeafSubType.Reform] = "Leaf/Reform";
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
