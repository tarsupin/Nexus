using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Plant : Chomper {

		public string[] Texture;

		public enum  PlantSubType {
			Plant,
			Metal,
		}

		public Plant() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyFixed];
			this.KnockoutName = "Particles/Chomp/Plant";
			this.DamageSurvive = DamageStrength.InstantKill;
			this.CreateTextures();
			this.tileId = (byte)TileEnum.Plant;
			this.title = "Plant";
			this.description = "Stationary. Doesn't hurt the character, but can be destroyed.";
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
