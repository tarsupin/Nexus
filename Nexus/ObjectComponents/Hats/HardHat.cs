﻿using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Protects you against falling projectiles.
	public class HardHat : Hat {

		public HardHat( Character character ) : base(character, HatRank.PowerHat) {
			this.SpriteName = "Hat/HardHat";
		}

		public override void UpdateCharacterStats() {
			this.character.stats.SafeVsDamageAbove = true;
		}
	}
}
