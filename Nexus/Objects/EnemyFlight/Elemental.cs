﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum ElementalSubType : byte {
		Air,
		Earth,
		Fire,
	}

	public class Elemental : EnemyFlight {

		public Elemental(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyFly];

			// Physics, Collisions, etc.
			this.physics = new Physics(this);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 4, -4, -12);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) ElementalSubType.Air) {
				this.SpriteName = "Elemental/Air/Left";
			} else if(subType == (byte) ElementalSubType.Earth) {
				this.SpriteName = "Elemental/Earth/Left";
			} else if(subType == (byte) ElementalSubType.Fire) {
				this.SpriteName = "Elemental/Fire/Left";
			}
		}

		public override bool DamageByTNT() { return false; }

		// TODO: Collide With Character (see Elemental)
		// TODO: Collide With Character (see Elemental)
	}
}
