using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;
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
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Shell].meta;
			this.ThrowStrength = 14;
			this.KickStrength = 7;

			// Grip Points (When Held)
			this.gripLeft = -31;
			this.gripRight = 21;
			this.gripLift = -8;

			this.physics.SetGravity(FInt.Create(0.4));

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(10, 2, -2, 0);
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

		public override void CollidePosDown(int posY) {
			this.physics.touch.TouchUp();
			this.physics.StopY();
			this.physics.MoveToPosY(posY);

			// Only stop the shell's X momentum if it was equal to released momentum during a throw.
			if(releasedMomentum > 0 && this.physics.velocity.X.RoundInt == releasedMomentum) {
				this.physics.StopX();
				releasedMomentum = 0;
			}
		}

		public override bool CollideObjDown(DynamicObject obj) {

			// Verify the object is moving Up. If not, don't collide.
			// This prevents certain false collisions, e.g. if both objects are moving in the same direction.
			if(this.physics.intend.Y >= 0) { return false; }

			this.physics.touch.TouchUp();
			this.physics.AlignDown(obj);
			this.physics.StopY();

			// Only stop the shell's X momentum if it was equal to released momentum during a throw.
			if(releasedMomentum > 0 && this.physics.velocity.X.RoundInt == releasedMomentum) {
				this.physics.StopX();
				releasedMomentum = 0;
			}

			return true;
		}
	}
}
