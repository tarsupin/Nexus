using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class PlatformFall : Platform {

		public PlatformFall(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.PlatformFall].meta;
			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(0, 0, 0, 0);

			this.physics.SetGravity(FInt.Create(0));
		}

		public override void ActivatePlatform() {
			this.physics.SetGravity(FInt.Create(0.1));
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte)PlatformSubTypes.W2) {
				this.SpriteName = "Platform/Fall/W2";
			} else {
				this.SpriteName = "Platform/Fall/W1";
			}
		}
	}
}
