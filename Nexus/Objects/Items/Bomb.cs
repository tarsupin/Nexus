using Nexus.Engine;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Bomb : Item {

		public enum BombSubType : byte {
			Bomb
		}

		public Bomb(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.ThrowStrength = 12;

			// Grip Points (When Held)
			this.gripLeft = -44;
			this.gripRight = 5;
			this.gripLift = -36;

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(10, 4, -4, 0);
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) BombSubType.Bomb) {
				this.SpriteName = "Item/Bomb1";
			}
		}
	}
}
