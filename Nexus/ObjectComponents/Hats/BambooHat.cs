﻿using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Grants Shell Master
	public class BambooHat : Hat {

		public BambooHat() : base(HatRank.PowerHat) {
			this.SpriteName = "Hat/BambooHat";
		}

		public override void UpdateCharacterStats(Character character) {
			character.stats.ShellMastery = true;
		}
	}
}
