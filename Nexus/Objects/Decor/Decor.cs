using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Decor : ClassGameObject {

		public string[] DecorTexture;

		public Decor(LevelScene scene, ClassGameObjectId classId) : base(scene, classId, AtlasGroup.Blocks) {

		}
	}
}
