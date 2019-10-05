using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Decor : ClassGameObject {

		public string[] Texture;

		public Decor(LevelScene scene, ClassGameObjectId classId) : base(scene, classId, AtlasGroup.Tiles) {

		}

		public override void Draw(byte subType, ushort posX, ushort posY) {
			this.atlas.Draw(this.Texture[subType], FVector.Create(posX, posY));
		}
	}
}
