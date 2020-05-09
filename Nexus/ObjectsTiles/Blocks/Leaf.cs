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

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			
			// Destroy Leaf
			if(dir == DirCardinal.Up) {
				this.BreakApart(room, gridX, gridY);
			} else if(dir == DirCardinal.Down) {

				// TODO: Add a delay to the leaf at this gridId.
				this.BreakApart(room, gridX, gridY);
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		private void BreakApart(RoomScene room, ushort gridX, ushort gridY) {

			// Damage Creatures Above (if applicable)
			uint enemyFoundId = CollideDetect.FindObjectsTouchingArea( room.objects[(byte)LoadOrder.Enemy], (uint) gridX * (byte) TilemapEnum.TileWidth + 16, (uint) gridY * (byte)TilemapEnum.TileHeight - 4, 16, 4 );

			if(enemyFoundId > 0) {
				Enemy enemy = (Enemy) room.objects[(byte)LoadOrder.Enemy][enemyFoundId];
				enemy.Die(DeathResult.Knockout);
			}

			// Destroy Leaf Tile
			room.tilemap.RemoveTileByGrid(gridX, gridY);

			// Display Particle Effect
			ExplodeEmitter.BoxExplosion(room, "Particles/Leaf", gridX * (byte)TilemapEnum.TileWidth + 24, gridY * (byte)TilemapEnum.TileHeight + 24);

			// Leaf Breaking Sound
			Systems.sounds.thudTap.Play();
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
