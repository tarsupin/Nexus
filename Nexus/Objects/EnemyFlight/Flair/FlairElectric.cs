using Nexus.Engine;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum FlairElectricSubType : byte { Normal };

	public class FlairElectric : Elemental {

		public FlairElectric(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.AssignSubType(subType);
		}

		private void AssignSubType( byte subType ) {
			this.SpriteName = "Flair/Electric/Left2";
		}
	}
}
