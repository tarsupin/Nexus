﻿using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// TODO: Undecided Power; was originally fast-cast, but that makes more sense as a sorcery hat or something. Could be double weapon speed (handheld, etc).
	public class RangerHat : Hat {

		public RangerHat() : base(HatRank.PowerHat) {
			this.SpriteName = "Hat/RangerHat";
		}

		public override void UpdateCharacterStats(Character character) {
			System.Console.WriteLine("GRANT NEW POWER TO RANGER HAT");
		}
	}
}
