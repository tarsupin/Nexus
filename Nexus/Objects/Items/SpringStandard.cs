using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class SpringStandard : Item {

		public enum  SpringStandardSubType : byte {
			Norm,
		}

		public SpringStandard(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.SpringStandard].meta;
			this.ThrowStrength = 14;

			// Grip Points (When Held)
			this.gripLeft = -35;
			this.gripRight = 25;
			this.gripLift = -8;

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 2, -2, 0);
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) SpringStandardSubType.Norm) {
				this.SpriteName = "Spring/Up";
			}
		}
	}
}
