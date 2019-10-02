using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class LedgeGrass : Ground {

		public LedgeGrass(Scene scene, byte subType, FVector pos, object[] paramList = null) : base(scene, subType, pos, paramList) {
			System.Console.WriteLine("LedgeGrass Created");
		}

		public override void SetSubType(byte subType) {
			this.Texture = "GrassLedge/" + System.Enum.GetName(typeof(GroundSubTypes), subType);
		}
	}
}
