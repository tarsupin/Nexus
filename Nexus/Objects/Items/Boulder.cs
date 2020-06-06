using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Boulder : Item {

		public enum BoulderSubType : byte {
			Boulder
		}

		public Boulder(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Boulder].meta;
			this.ThrowStrength = 10;

			// Grip Points (When Held)
			this.gripLeft = -35;
			this.gripRight = 25;
			this.gripLift = -8;

			this.physics.SetGravity(FInt.Create(0.6));

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(4, 2, -2, 0);
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) BoulderSubType.Boulder) {
				this.SpriteName = "Items/Boulder";
			}
		}
	}
}
