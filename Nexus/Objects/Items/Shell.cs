using Nexus.Engine;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Shell : Item {

		public enum ShellSubType : byte {
			Green,
			GreenWing,
			Heavy,
			Red,
		}

		public Shell(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.ThrowStrength = 14;
			this.KickStrength = 7;

			// Grip Points (When Held)
			this.gripLeft = -40;
			this.gripRight = 0;
			this.gripLift = -34;

			this.physics.SetGravity(FInt.Create(0.4));

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(30, 0, 0, 0);
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) ShellSubType.Green) {
				this.SpriteName = "Shell/Green/Side";
			} else if(subType == (byte) ShellSubType.GreenWing) {
				this.physics.SetGravity(FInt.Create(0.35));
				this.SpriteName = "Shell/GreenWing/Side";
			} else if(subType == (byte) ShellSubType.Heavy) {
				this.physics.SetGravity(FInt.Create(0.6));
				this.KickStrength = 5;
				this.SpriteName = "Shell/Heavy/Side";
			} else if(subType == (byte) ShellSubType.Red) {
				this.SpriteName = "Shell/Red/Side";
			}
		}
	}
}
