using Nexus.Gameplay;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public class ChomperGrass : Chomper {

		public ChomperGrass() : base() {
			this.SpriteName = "Chomper/Grass/Chomp";
			this.KnockoutName = "Particles/Chomp/Grass";
			this.tileId = (byte)TileEnum.ChomperGrass;
		}
	}
}
