using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundSnow : Ground {

		public GroundSnow(Scene scene, byte subType, FVector pos, object[] paramList = null) : base(scene, subType, pos, paramList) {

		}

		public override void SetSubType( byte subType ) {
			this.Texture = "Snow/" + System.Enum.GetName(typeof(GroundSubTypes), subType);
		}
	}
}
