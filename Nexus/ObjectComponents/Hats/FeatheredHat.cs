﻿using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Increases Jump Height and Duration
	public class FeatheredHat : Hat {

		public FeatheredHat() : base(HatRank.PowerHat) {
			this.subType = (byte)HatSubType.FeatheredHat;
			this.SpriteName = "Hat/FeatheredHat";
			this.subStr = "feather";
		}

		public override void UpdateCharacterStats(Character character) {
			CharacterStats stats = character.stats;
			stats.JumpDuration += 2;
			stats.JumpStrength += 1;
			stats.WallJumpDuration += 2;
			stats.WallJumpYStrength += 1;
		}
	}
}
