using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class PlatformDip : Platform {

		private int dipWeight = 0;
		private int yStart = 0;

		public PlatformDip(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.PlatformDip].meta;
			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(0, 0, 0, 0);

			this.yStart = this.posY;
			this.physics.SetGravity(FInt.Create(0));
		}

		public override void RunTick() {

			// If there is momentum releasing on the dip platform.
			if(this.dipWeight <= 0) {

				// If the platform is moving (not stationed at its original position):
				if(this.physics.gravity != 0) {

					// If the platform is below the starting height:
					if(this.posY > this.yStart) {
						this.physics.SetGravity(FInt.Create(-0.1));
					}
					
					// If the platform is above (or at) the starting height.
					else {
						this.physics.SetGravity(FInt.Create(0));
						this.physics.velocity.Y = FInt.Create(0);
						if(this.posY < this.yStart) {
							this.physics.MoveToPosY(this.yStart);
							return;
						}
					}
				}
			}

			// If there is no current momentum on the dip platform, begin resetting the weight.
			else {
				this.dipWeight -= 3;
				this.physics.SetGravity(FInt.Create(0.05));
			}

			base.RunTick();
		}

		public override void ActivatePlatform() {
			if(this.dipWeight < 60) {
				if(this.dipWeight < 12) { this.dipWeight = 12; }
				this.dipWeight += 3;
			}
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte)PlatformSubTypes.W2) {
				this.SpriteName = "Platform/Dip/W2";
			} else {
				this.SpriteName = "Platform/Dip/W1";
			}
		}
	}
}
