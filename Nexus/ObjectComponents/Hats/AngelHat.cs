﻿using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Grants Double Jump
	public class AngelHat : Hat {

		public AngelHat() : base(HatRank.PowerHat) {
			this.SpriteName = "Hat/AngelHat";
		}

		public override void UpdateCharacterStats(Character character) {
			character.stats.JumpMaxTimes += 1;
		}
	}
}
