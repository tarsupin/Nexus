﻿using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// Has no effect; is just for style, or to accomodate a Suit.
	public class CosmeticHat : Hat {

		public CosmeticHat( Character character, string hatTexture ) : base(character, HatRank.PowerHat) {

		}
	}
}
