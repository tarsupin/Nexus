using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class OrbItem : Item {

		public enum OrbSubType : byte {
			Magic,
		}

		public OrbItem(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.OrbItem].meta;
			this.ThrowStrength = 12;

			// Grip Points (When Held)
			this.gripLeft = -28;
			this.gripRight = 20;
			this.gripLift = -4;

			this.physics.SetGravity(FInt.Create(0.4));

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 2, -2, -2);
		}

		private void AssignSubType(byte subType) {
			this.SpriteName = "Orb/Magic";
		}
	}
}
