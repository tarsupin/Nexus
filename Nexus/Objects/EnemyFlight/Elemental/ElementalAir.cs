using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class ElementalAir : Elemental {

		public enum ElementalAirSubType : byte {
			Left,
			Right,
		}

		public ElementalAir(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.ElementalAir].meta;
			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 4, -4, -12);
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte)ElementalAirSubType.Left) {
				this.SpriteName = "Elemental/Air/Left";
				this.FaceRight = false;
			} else {
				this.SpriteName = "Elemental/Air/Right";
				this.FaceRight = true;
			}
		}
	}
}
