using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class TNT : Item {

		public enum TNTSubType : byte {
			TNT
		}

		public TNT(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.TNT].meta;
			this.ThrowStrength = 14;

			// Grip Points (When Held)
			this.gripLeft = -35;
			this.gripRight = 25;
			this.gripLift = -8;

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(6, 2, -2, 0);
		}

		public override void ActivateItem() { }

		private void AssignSubType(byte subType) {
			if(subType == (byte) TNTSubType.TNT) {
				this.SpriteName = "Items/TNT";
			}
		}
	}
}
