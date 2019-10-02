using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DecorVeg : Decor {

		public static void Generate() {

		}

		public DecorVeg(Scene scene, byte subType, FVector pos, object[] paramList = null) : base(scene, subType, pos, paramList) {

		}

		public override void SetSubType( byte subType ) {
			this.Texture = "Grass/" + System.Enum.GetName(typeof(GroundSubTypes), subType);
		}
	}
}
