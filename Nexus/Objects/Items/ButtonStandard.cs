using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class ButtonStandard : Item {

		public ButtonStandard(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.ThrowStrength = 14;

			// Grip Points (When Held)
			this.gripLeft = -45;
			this.gripRight = 3;
			this.gripLift = -38;

			this.AssignSubType(subType);
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) ButtonSubTypes.BR) {
				this.SpriteName = "Button/Standard/BR";
			}
		}
	}
}
