using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class PlatformMove : Platform {

		public PlatformMove(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.PlatformMove].meta;
			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(0, 0, 0, 0);
		}

		//this.physics.setMaxVelocity(0, 5);

		private void AssignSubType(byte subType) {
			if(subType == (byte)HorizontalSubTypes.S) {
				this.SpriteName = "Platform/Move/S";
			} else if(subType == (byte)HorizontalSubTypes.H1) {
				this.SpriteName = "Platform/Move/S";
			} else if(subType == (byte)HorizontalSubTypes.H2) {
				this.SpriteName = "Platform/Move/H2";
			} else if(subType == (byte)HorizontalSubTypes.H3) {
				this.SpriteName = "Platform/Move/H3";
			}
		}
	}
}
