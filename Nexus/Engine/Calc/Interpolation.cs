
using System;

namespace Nexus.Engine {

	public static class Interpolation {

		// Get value between two numbers using weight factor (0 to 1);
		public static float Number( float val1, float val2, float weight )  {
			return (1 - weight) * val1 + weight * val2;
		}
		
		// Ease a value back and forth between two values.
		public static float EaseBothDir( float val1, float val2, float weight )  {
			return (float) (val1 + Math.Abs(Math.Sin((weight + 0.75) * Math.PI * 2) / 2 + 0.5) * (val2 - val1));
		}

		// Quadratic Bezier Interpolation with Smooth Ease
		public static float QuadBezierEaseBothDir( float p0, float p1, float p2, float weight )  {
			weight = (float) Math.Sin((weight * Math.PI * 2) / 2 + 0.5);
			float k = 1 - weight;
			return (k* k * p0) + (2 * (1 - weight) * weight * p1) + (weight* weight * p2);
		}
		
		// Quadratic Bezier Interpolation
		// https://en.wikipedia.org/wiki/Bezier_curve
		// p0, p1, p2 are Start Point, Control Point, End Point
		public static float QuadBezier( float p0, float p1, float p2, float weight) {
			float k = 1 - weight;
			return (k * k * p0) + (2 * (1 - weight) * weight * p1) + (weight * weight * p2);
		}

		// Get the speed needed to cover a distance over the time provided.
		public static float Speed( float distance, float time )  {
			return distance / time;
		}
	}
}
