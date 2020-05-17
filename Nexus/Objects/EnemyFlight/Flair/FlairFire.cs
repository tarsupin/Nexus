﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum FlairFireSubType : byte { Normal };

	public class FlairFire : Elemental {

		public FlairFire(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.FlairFire].meta;
			this.AssignSubType(subType);
		}

		private void AssignSubType( byte subType ) {
			this.SpriteName = "Flair/Fire/Left2";
		}
	}
}
