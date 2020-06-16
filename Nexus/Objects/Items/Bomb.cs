using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Bomb : Item {

		public enum BombSubType : byte {
			Bomb
		}

		public Bomb(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Bomb].meta;
			this.ThrowStrength = 12;

			// Grip Points (When Held)
			this.gripLeft = -35;
			this.gripRight = 25;
			this.gripLift = -8;

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(4, 4, -4, 0);
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) BombSubType.Bomb) {
				this.SpriteName = "Items/Bomb1";
			}
		}

		public override void ActivateItem() { }
	}
}
