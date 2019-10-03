using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Collectable : ClassGameObject {

		protected string[] Texture;

		public Collectable(LevelScene scene, ClassGameObjectId classId) : base(scene, classId, AtlasGroup.Other) {

		}

		public override void Draw(byte subType, ushort posX, ushort posY) {
			this.atlas.Draw(this.Texture[subType], FVector.Create(posX, posY));
		}
	}
}
