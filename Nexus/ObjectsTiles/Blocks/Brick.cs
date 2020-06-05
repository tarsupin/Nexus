using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

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

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			
			// Nudge Brick Upward
			if(dir == DirCardinal.Up) {

				if(actor is Character) {
					Character character = (Character) actor;
					
					// Only Spikey Hats destroy Bricks
					if(character.hat is SpikeyHat) {

						// Destroy Brick
						BlockTile.BreakApart(room, gridX, gridY);

						// Display Particle Effect
						byte subType = room.tilemap.GetMainSubType(gridX, gridY);

						ExplodeEmitter.BoxExplosion(room, "Particles/Brick" + (subType == (byte)BrickSubType.Gray ? "Gray" : ""), gridX * (byte)TilemapEnum.TileWidth + 24, gridY * (byte)TilemapEnum.TileHeight + 24);

						return base.RunImpact(room, actor, gridX, gridY, dir);
					}
				}
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
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
