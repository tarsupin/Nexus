﻿using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class LeapMobility : PowerMobility {

		public LeapMobility( Character character ) : base( character ) {
			this.subType = (byte)PowerSubType.Leap;
			this.IconTexture = "Power/Leap";
			this.subStr = "leap";
		}
	}
}
