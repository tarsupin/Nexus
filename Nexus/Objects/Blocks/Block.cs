using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Block : DynamicGameObject {

		public Block(LevelScene scene, byte subType, FVector pos, object[] paramList) : base(scene, subType, pos, paramList) {

		}
	}
}
