using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.ObjectComponents;
using Newtonsoft.Json.Linq;

namespace Nexus.Objects {

	public class Block : DynamicGameObject {

		public Block(LevelScene scene, byte subType, FVector pos, JObject paramList) : base(scene, subType, pos, paramList) {

		}
	}
}
