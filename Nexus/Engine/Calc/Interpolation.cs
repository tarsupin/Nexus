
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
		
		// http://gizma.com/easing/
		// elapsed: from 0 to duration
		public static float EaseOut( float elapsed, float startValue, float change, float duration ) {
			elapsed /= duration;
			return -change * elapsed * (elapsed - 2) + startValue;
		}

		// elapsed: from 0 to duration
		public static float EaseInAndOut(float elapsed, float startValue, float change, float duration) {
			elapsed /= duration / 2;
			if(elapsed < 1) {
				return change / 2 * elapsed * elapsed + startValue;
			}
			elapsed--;
			return -change / 2 * (elapsed * (elapsed - 2) - 1) + startValue;
		}

		// Quadratic Bezier Interpolation with Smooth Ease
		// p0, p1, p2 are Start Point, Control Point, End Point
		// weight is a float between 0 and 1
		public static float QuadBezierEaseBothDir( float p0, float p1, float p2, float weight )  {
			weight = (float) Math.Sin((weight * Math.PI * 2) / 2 + 0.5);
			float k = 1 - weight;
			return (k* k * p0) + (2 * (1 - weight) * weight * p1) + (weight* weight * p2);
		}
		
		// Quadratic Bezier Interpolation
		// https://en.wikipedia.org/wiki/Bezier_curve
		// weight is a float between 0 and 1
		public static float QuadBezier( float startPoint, float midPoint, float endPoint, float weight) {
			float k = 1 - weight;
			return (k * k * startPoint) + (2 * (1 - weight) * weight * midPoint) + (weight * weight * endPoint);
		}

		// Get the speed needed to cover a distance over the time provided.
		public static float Speed( float distance, float time )  {
			return distance / time;
		}
	}
}
