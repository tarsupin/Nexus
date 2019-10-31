using Nexus.Engine;
using Nexus.GameEngine;
using Newtonsoft.Json.Linq;

namespace Nexus.Objects {

	public class Block : DynamicGameObject {

		public Block(RoomScene room, byte subType, FVector pos, JObject paramList) : base(room, subType, pos, paramList) {

		}
	}
}
