﻿using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class RedWizard : Suit {

		public RedWizard() : base(SuitRank.PowerSuit, "RedWizard", HatMap.WizRed) {

		}

		public override void UpdateCharacterStats(Character character) {
			CharacterStats stats = character.stats;
			stats.CanFastCast = true;
			stats.JumpMaxTimes++;
		}
	}
}
