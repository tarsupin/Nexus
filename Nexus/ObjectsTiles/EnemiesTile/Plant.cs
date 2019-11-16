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

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.Plant)) {
				new Plant(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.Plant, subTypeId);
		}

		public Plant(RoomScene room) : base(room, TileEnum.Plant, AtlasGroup.Tiles) {
			this.collides = true;
			this.CreateTextures();
		}

		// Plant Impacts
		public override bool RunImpact(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			TileSolidImpact.RunImpact(actor, gridX, gridY, dir);

			// Plants die when hit by projectiles of sufficient damage.
			 if(actor is Projectile) {
				(actor as Projectile).Destroy(dir);
				this.Destroy(gridX, gridY);

				// TODO: Get gridID and determine subType. Otherwise, it always shows Plant here.
				DeathEmitter.Knockout(this.room, "Particles/Chomp/Plant", gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
				Systems.sounds.splat1.Play();
			}

			return true;
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) PlantSubType.Plant] = "Plant/Plant";
			this.Texture[(byte) PlantSubType.Metal] = "Plant/Metal";
		}

		public override void Draw(byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
