using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class ButtonStandard : Item {

		public ButtonStandard(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.ButtonStandard].meta;
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
			} else if(subType == (byte) ButtonSubTypes.BRDown) {
				this.SpriteName = "Button/Standard/BRDown";
			} else if(subType == (byte) ButtonSubTypes.BROff) {
				this.SpriteName = "Button/Standard/BROff";
			} else if(subType == (byte) ButtonSubTypes.BROffDown) {
				this.SpriteName = "Button/Standard/BROffDown";
			} else if(subType == (byte) ButtonSubTypes.GY) {
				this.SpriteName = "Button/Standard/GY";
			} else if(subType == (byte) ButtonSubTypes.GYDown) {
				this.SpriteName = "Button/Standard/GYDown";
			} else if(subType == (byte) ButtonSubTypes.GYOff) {
				this.SpriteName = "Button/Standard/GYOff";
			} else if(subType == (byte) ButtonSubTypes.GYOffDown) {
				this.SpriteName = "Button/Standard/GYOffDown";
			}
		}
	}
}
