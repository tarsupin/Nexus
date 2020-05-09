using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Plant : TileGameObject {

		public string[] Texture;

		public enum  PlantSubType {
			Plant,
			Metal,
		}

		public Plant() : base() {
			this.collides = true;
			this.CreateTextures();
			this.tileId = (byte)TileEnum.Plant;
			this.title = "Plant";
			this.description = "Stationary. Doesn't hurt the character, but can be destroyed.";
		}

		// Plant Impacts
		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			TileSolidImpact.RunImpact(actor, gridX, gridY, dir);

			// Plants die when hit by projectiles of sufficient damage.
			 if(actor is Projectile) {
				(actor as Projectile).Destroy(dir);
				this.DestroyFull(room, gridX, gridY);

				// TODO: Get gridID and determine subType. Otherwise, it always shows Plant here.
				DeathEmitter.Knockout(room, "Particles/Chomp/Plant", gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
				Systems.sounds.splat1.Play();
			}

			return true;
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) PlantSubType.Plant] = "Plant/Plant";
			this.Texture[(byte) PlantSubType.Metal] = "Plant/Metal";
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
