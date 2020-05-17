﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

// Items are considered objects that Characters can pick up.
// DROP is a guaranteed action.

namespace Nexus.Objects {

	public class Platform : DynamicObject {

		public static readonly FInt MaxFallVelocity = FInt.Create(5);

		public Platform(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			
			// TODO: MUST ADD META TO EACH PLATFORM CLASS - NOT THE BASE ONE
			//this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Plat].meta;
			
			this.AssignBoundsByAtlas();
			this.physics.SetGravity(FInt.Create(0));
		}

		public override void RunTick() {

			// Limit Velocity
			if(this.physics.velocity.Y > Platform.MaxFallVelocity) { this.physics.velocity.Y = Platform.MaxFallVelocity; }

			base.RunTick();
		}
	}
}
