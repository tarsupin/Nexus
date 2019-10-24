using Newtonsoft.Json.Linq;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Chomper : TileGameObject {

		public string[] Texture;
		public float rotation;

		public enum  ChomperSubType {
			Standard,
			Grass,
			Metal,
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.Chomper)) {
				new Chomper(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.Chomper, subTypeId);
		}

		public Chomper(LevelScene scene) : base(scene, TileGameObjectId.Chomper, AtlasGroup.Tiles) {
			this.collides = true;
			this.CreateTextures();
		}

		public override void UpdateParams(JObject paramList) {
			if(paramList["face"] != null) {
				byte face = (byte) paramList.GetValue("face");

				if(face == (byte) DirCardinal.Right) {

				}
			}
		}

		// TODO HIGH PRIORITY: Chomper Impacts (projectiles, character, etc.)
		public override bool RunImpact(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			TileSolidImpact.RunImpact(actor, gridX, gridY, dir);

			// Characters Receive Chomper Damage
			if(actor is Character) {
				Character character = (Character) actor;
				character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
			}

			return true;
		}

		private void CreateTextures() {
			this.Texture = new string[3];
			this.Texture[(byte) ChomperSubType.Standard] = "Chomper/Standard/Chomp1";
			this.Texture[(byte) ChomperSubType.Grass] = "Chomper/Grass/Chomp1";
			this.Texture[(byte) ChomperSubType.Metal] = "Chomper/Metal/Chomp1";
		}

		public override void Draw(byte subType, int posX, int posY) {
			this.atlas.DrawFaceDown(this.Texture[subType], posX, posY);
		}
	}
}
