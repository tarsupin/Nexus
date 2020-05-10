using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	// TODO: Nudging is going to be complicated. Maybe hide the tile, create a particle effect.

	public class Brick : BlockTile {

		public string[] Texture;

		public enum BrickSubType : byte {
			Brown = 0,
			Gray = 1,
		}

		public Brick() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.Brick;
			this.title = "Brick";
			this.description = "Nudged from underneath. Can be destroyed with Spikey Hat.";
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			
			// Nudge Brick Upward
			if(dir == DirCardinal.Up) {

				if(actor is Character) {
					Character character = (Character) actor;
					
					// Only Spikey Hats destroy Bricks
					if(character.hat is SpikeyHat) {
						this.BreakApart(room, gridX, gridY);
						return base.RunImpact(room, actor, gridX, gridY, dir);
					}
				}
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

			// Destroy Brick Tile
			room.tilemap.RemoveTile(gridX, gridY);

			// Display Particle Effect
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);

			ExplodeEmitter.BoxExplosion(room, "Particles/Brick" + (subType == (byte) BrickSubType.Gray ? "Gray" : ""), gridX * (byte)TilemapEnum.TileWidth + 24, gridY * (byte)TilemapEnum.TileHeight + 24);

			// Brick Breaking Sound
			Systems.sounds.brickBreak.Play();
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) BrickSubType.Brown] = "Brick/Brown";
			this.Texture[(byte) BrickSubType.Gray] = "Brick/Gray";
		}
		
		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
