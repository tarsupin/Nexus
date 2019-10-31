using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Box : BlockTile {

		public string[] Texture;

		public enum BoxSubType {
			Brown = 0,
			Gray = 1,
		}

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsClassGameObjectRegistered((byte) TileGameObjectId.Box)) {
				new Box(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.Box, subTypeId);
		}

		public Box(RoomScene room) : base(room, TileGameObjectId.Box) {
			this.CreateTextures();
		}

		public override bool RunImpact(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			TileSolidImpact.RunImpact(actor, gridX, gridY, dir);

			if(actor is Character) {

				// Standard Character Tile Collisions
				TileCharBasicImpact.RunImpact((Character)actor, dir);
			}

			// Destroy Box
			if(dir == DirCardinal.Up) {
				this.BreakApart(gridX, gridY);
			}

			return true;
		}

		private void BreakApart(ushort gridX, ushort gridY) {

			// Damage Creatures Above (if applicable)
			uint enemyFoundId = CollideDetect.FindObjectsTouchingArea( this.room.objects[(byte)LoadOrder.Enemy], (uint) gridX * (byte) TilemapEnum.TileWidth + 16, (uint) gridY * (byte)TilemapEnum.TileHeight - 4, 16, 4 );

			if(enemyFoundId > 0) {
				Enemy enemy = (Enemy) this.room.objects[(byte)LoadOrder.Enemy][enemyFoundId];
				enemy.Die(DeathResult.Knockout);
			}

			// Destroy Box Tile
			this.room.tilemap.RemoveTileByGrid(gridX, gridY);

			// Display Particle Effect
			// TODO PARTICLES: Display particle effect for box being destroyed.
			//let particleSys = game.particles;
			//PEventExplode.activate(particleSys, AtlasGroup.Other, "Particles/WoodFrag", this.pos.x + this.img.width / 2, this.pos.y + this.img.height / 2);

			// TODO SOUND: Box Breaking Sound
			// game.audio.soundList.brickBreak.play();
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) BoxSubType.Brown] = "Box/Brown";
			this.Texture[(byte) BoxSubType.Gray] = "Box/Gray";
		}

		public override void Draw(byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
