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
				this.SpriteName = "Button/BR";
			} else if(subType == (byte) ButtonSubTypes.BRDown) {
				this.SpriteName = "Button/BRDown";
			} else if(subType == (byte) ButtonSubTypes.BROff) {
				this.SpriteName = "Button/BROff";
			} else if(subType == (byte) ButtonSubTypes.BROffDown) {
				this.SpriteName = "Button/BROffDown";
			} else if(subType == (byte) ButtonSubTypes.GY) {
				this.SpriteName = "Button/GY";
			} else if(subType == (byte) ButtonSubTypes.GYDown) {
				this.SpriteName = "Button/GYDown";
			} else if(subType == (byte) ButtonSubTypes.GYOff) {
				this.SpriteName = "Button/GYOff";
			} else if(subType == (byte) ButtonSubTypes.GYOffDown) {
				this.SpriteName = "Button/GYOffDown";
			}
		}
	}
}
