using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

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

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {

			if(actor is Projectile) {
				return TileProjectileImpact.RunImpact((Projectile)actor, gridX, gridY, dir);
			}

			// Slam-Down Action can break boxes.
			if(actor is Character && ((Character)actor).status.action is SlamAction) {
				this.DestroyBox(room, gridX, gridY);
				return true;
			}

			// Destroy Box
			if(dir == DirCardinal.Up) {
				BlockTile.DamageAbove(room, gridX, gridY);
				this.DestroyBox(room, gridX, gridY);
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		public void DestroyBox(RoomScene room, short gridX, short gridY) {

			// Display Particle Effect
			ExplodeEmitter.BoxExplosion(room, "Particles/WoodFrag", gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth, gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.HalfHeight);
			Systems.sounds.brickBreak.Play();

			// Destroy Brick Tile
			room.tilemap.SetMainTile(gridX, gridY, 0, 0);
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
