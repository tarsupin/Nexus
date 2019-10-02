using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Ground : StaticGameObject {

		public Ground(Scene scene, byte subType, FVector pos, object[] paramList = null) : base(scene, subType, pos, paramList) {
			this.Meta = scene.maps.MetaList[MetaGroup.Ground];
		}
	}
}
