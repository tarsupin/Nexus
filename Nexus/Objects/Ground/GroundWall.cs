using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundWall : Ground {

		public GroundWall(Scene scene, byte subType, FVector pos, object[] paramList = null) : base(scene, subType, pos, paramList) {
		}

		// TODO: There is also Gray Slabs; convert to new wall type?
		public override void SetSubType(byte subType) {
			this.Texture = "Slab/Brown/" + System.Enum.GetName(typeof(GroundSubTypes), subType);
		}
	}
}
