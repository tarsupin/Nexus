﻿using Newtonsoft.Json.Linq;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class StaticGameObject : GameObject {

		// TODO: Sticky? Ungrippable, Damage, Friction, Conveyor
		// -- move all into a TouchEffect class that contains this; (Sticky, Ungrippable, Damage, Friction)
		// -- that would also allow it to be utilized in dynamic blocks

		public StaticGameObject(LevelScene scene, byte subType, FVector pos, JObject paramList = null) : base(scene, subType, pos, paramList) {

		}
	}
}
