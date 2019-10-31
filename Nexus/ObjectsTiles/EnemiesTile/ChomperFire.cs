using Newtonsoft.Json.Linq;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class ChomperFire : TileGameObject {

		public string[] Texture;

		public enum  ChomperSubType {
			Fire,
		}

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsClassGameObjectRegistered((byte) TileGameObjectId.ChomperFire)) {
				new ChomperFire(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.ChomperFire, subTypeId);
		}

		public ChomperFire(RoomScene room) : base(room, TileGameObjectId.ChomperFire, AtlasGroup.Tiles) {
			this.collides = true;
			this.CreateTextures();
		}

		public override void UpdateParams(JObject paramList) {
			// JToken token = paramList["attGrav"];
			// if(token != null) {
			//	int a = (int) paramList.GetValue("attGrav");
			//	System.Console.WriteLine("GRAV: " + a.ToString());
			// }
		}

		// TODO HIGH PRIORITY: ChomperFire Impacts (projectiles, character, etc.)
		public override bool RunImpact(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			TileSolidImpact.RunImpact(actor, gridX, gridY, dir);

			// Characters Receive ChomperFire Damage
			if(actor is Character) {
				Character character = (Character) actor;
				character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
			}

			return true;
		}

		private void CreateTextures() {
			this.Texture = new string[1];
			this.Texture[(byte) ChomperSubType.Fire] = "Chomper/Fire/Chomp1";
		}

		public override void Draw(byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
