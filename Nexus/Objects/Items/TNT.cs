﻿using Nexus.Engine;
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
			this.gripLeft = -44;
			this.gripRight = 5;
			this.gripLift = -34;

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(4, 2, -2, 0);
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) TNTSubType.TNT) {
				this.SpriteName = "Items/TNT";
			}
		}
	}
}
