using Nexus.Engine;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum FlairMagicSubType : byte { Normal };

	public class FlairMagic : Elemental {

		public FlairMagic(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.AssignSubType(subType);
		}

		private void AssignSubType( byte subType ) {
			this.SpriteName = "Flair/Magic/Left2";
		}
	}
}
