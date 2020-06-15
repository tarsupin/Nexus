using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class PlatformDelay : Platform {

		private sbyte offset = 0;
		private int activationFrame = 0;
		private int releaseFrame = 0;

		public PlatformDelay(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.PlatformDelay].meta;
			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(0, 0, 0, 0);

			this.physics.SetGravity(FInt.Create(0));
		}

		public override void ActivatePlatform() {
			if(this.activationFrame > 0) { return; }
			this.activationFrame = (int)Systems.timer.Frame;
			this.releaseFrame = (int)(Systems.timer.Frame + 60);
		}

		public override void RunTick() {

			// Check if the delay platform is shaking.
			if(this.activationFrame > 0) {
				if(this.releaseFrame > Systems.timer.Frame) {
					float weight = (float)((Systems.timer.frame60Modulus) * 0.08f);
					this.offset = (sbyte)Math.Round(Interpolation.EaseBothDir(-1.8f, 1.8f, weight));
				} else if(releaseFrame == Systems.timer.Frame) {
					this.physics.SetGravity(FInt.Create(0.1));
					if(this.offset != 0) { this.offset = 0; }
				}
			}

			base.RunTick();
		}

		public override void Draw(int camX, int camY) {
			this.Meta.Atlas.Draw(this.SpriteName, posX - camX + this.offset, posY - camY);
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte)HorizontalSubTypes.S) {
				this.SpriteName = "Platform/Delay/S";
			} else if(subType == (byte)HorizontalSubTypes.H1) {
				this.SpriteName = "Platform/Delay/S";
			} else if(subType == (byte)HorizontalSubTypes.H2) {
				this.SpriteName = "Platform/Delay/H2";
			} else if(subType == (byte)HorizontalSubTypes.H3) {
				this.SpriteName = "Platform/Delay/H3";
			}
		}
	}
}
