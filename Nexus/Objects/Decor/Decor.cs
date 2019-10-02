using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Decor : ClassGameObject {

		public Decor(Scene scene, byte subType, FVector pos, object[] paramList = null) : base(scene, subType, pos, paramList) {
			this.Meta = scene.mapper.MetaList[MetaGroup.Ground];
		}
	}
}
