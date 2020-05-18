﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class SpringFixed : SpringTile {

		public enum SpringFixedSubType : byte {
			Fixed = 0,
		}

		public SpringFixed() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Block];
			this.title = "Fixed Spring";
			this.description = "A spring to bounce things on.";
			this.CreateTextures();
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		private void CreateTextures() {
			this.Texture = new string[1];
			this.Texture[(byte)SpringFixedSubType.Fixed] = "Spring/Up";
		}
	}
}
