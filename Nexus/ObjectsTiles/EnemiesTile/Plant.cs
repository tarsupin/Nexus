using Newtonsoft.Json.Linq;
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
			if(!room.IsClassGameObjectRegistered((byte) TileGameObjectId.Plant)) {
				new Plant(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.Plant, subTypeId);
		}

		public Plant(RoomScene room) : base(room, TileGameObjectId.Plant, AtlasGroup.Tiles) {
			this.collides = true;
			this.CreateTextures();
		}

		// TODO HIGH PRIORITY: Plant Impacts (projectiles, etc.)
		public override bool RunImpact(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			TileSolidImpact.RunImpact(actor, gridX, gridY, dir);
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
