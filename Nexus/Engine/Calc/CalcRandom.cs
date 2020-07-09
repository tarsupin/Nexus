using System;

namespace Nexus.Engine {

	public static class CalcRandom {

		public static Random Rand = new Random();

		public static int IntBetween( int min, int max ) {
			return (int) Rand.Next(min, max);
		}

		public static float FloatBetween( float min, float max ) {
			return (float) Rand.NextDouble() * (max - min) + min;
		}

		public static short ShortBetween(short min, short max) {
			return (short)Rand.Next(min, max);
		}

		public static bool RandomBool() {
			return (float) Rand.NextDouble() > 0.5f ? true : false;
		}
	}
}
