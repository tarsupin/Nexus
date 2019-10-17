using System;

namespace Nexus.Engine {

	public static class CalcRandom {

		public static Random Rand = new Random();

		public static float FloatBetween( float min, float max ) {
			return (float) Rand.NextDouble() * (max - min) + min;
		}
	}
}
