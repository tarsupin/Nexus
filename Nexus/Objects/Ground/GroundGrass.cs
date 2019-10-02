using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundGrass : Ground {

		public GroundGrass(Scene scene, byte subType, FVector pos, object[] paramList = null) : base(scene, subType, pos, paramList) {

		}

		public override void SetSubType( byte subType ) {
			this.Texture = "Grass/" + System.Enum.GetName(typeof(GroundSubTypes), subType);
		}
	}
}
