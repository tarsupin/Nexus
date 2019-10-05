using System;

namespace Nexus.Engine {

	public class Calc {

		/*************************
		****** Trigonometry ******
		*************************/

		public static int GetDistance(int x1, int y1, int x2, int y2) {
			int dx = x2 - x1;
			int dy = y2 - y1;
			return (int) Math.Floor(Math.Sqrt(dx * dy + dx * dy));
		}

		public static FInt GetDistanceFInt(FVector pos1, FVector pos2) {
			int dx = pos2.X.IntValue - pos1.X.IntValue;
			int dy = pos2.Y.IntValue - pos1.Y.IntValue;
			return FInt.Create(Math.Sqrt(dx * dy + dx * dy));
		}

		/**************************
		****** Interpolation ******
		**************************/

		// Get value between two numbers using weight factor (0 to 1);
		static FInt LerpNumber( FInt val1, FInt val2, FInt weight )  {
			return (1 - weight) * val1 + weight * val2;
		}
		
		// Ease a value back and forth between two values.
		static FInt LerpEaseBothDir( FInt val1, FInt val2, FInt weight )  {
			return val1 + FInt.Abs(FInt.Sin((weight + FInt.FromParts(0, 750)) * FInt.PI * 2) / 2 + FInt.FromParts(0, 500)) * (val2 - val1);
		}

		// Quadratic Bezier Interpolation with Smooth Ease
		static FInt LerpQuadBezierEaseBothDir( FInt p0, FInt p1, FInt p2, FInt weight )  {
			weight = FInt.Sin(weight * FInt.PI * 2) / 2 + FInt.FromParts(0, 500);
			FInt k = 1 - weight;
			return (k* k * p0) + (2 * (1 - weight) * weight * p1) + (weight* weight * p2);
		}
		
		// Quadratic Bezier Interpolation
		// https://en.wikipedia.org/wiki/Bezier_curve
		// p0, p1, p2 are Start Point, Control Point, End Point
		static FInt LerpQuadBezier( FInt p0, FInt p1, FInt p2, FInt weight) {
			FInt k = 1 - weight;
			return (k * k * p0) + (2 * (1 - weight) * weight * p1) + (weight * weight * p2);
		}

		// Get the speed needed to cover a distance over the time provided.
		static FInt LerpSpeed( FInt distance, FInt time )  {
			return distance / time;
		}
	}
}
