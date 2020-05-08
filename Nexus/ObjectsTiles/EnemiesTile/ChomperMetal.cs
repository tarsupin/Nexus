using Nexus.Gameplay;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public class ChomperMetal : Chomper {

		public ChomperMetal() : base() {
			this.SpriteName = "Chomper/Metal/Chomp";
			this.KnockoutName = "Particles/Chomp/Metal";
			this.tileId = (byte)TileEnum.ChomperMetal;
		}
	}
}
