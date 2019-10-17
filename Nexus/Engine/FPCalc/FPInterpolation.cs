
namespace Nexus.Engine {

	public static class FPInterpolation {

		// Get value between two numbers using weight factor (0 to 1);
		public static FInt Number( FInt val1, FInt val2, FInt weight )  {
			return (1 - weight) * val1 + weight * val2;
		}

		
		//// Calculate a smooth interpolation percent between the `min` and `max`
		//static getSmoothStepPercent( value: number, min: number, max: number ): number {
		//	value = (value - min) / (max - min);
		//	return value * value * (3 - 2 * value);
		//}
	
		//static getSmootherStepPercent( value: number, min: number, max: number ): number {
		//	value = Math.max(0, Math.min(1, (value - min) / (max - min)));
		//	return value * value * value * (value * (value * 6 - 15) + 10);
		//}
		
		// Ease a value back and forth between two values.
		public static FInt EaseBothDir( FInt val1, FInt val2, FInt weight )  {
			return val1 + FInt.Abs(FInt.Sin((weight + FInt.Create(0.75)) * FInt.PI * 2) / 2 + FInt.Create(0.5)) * (val2 - val1);
		}

		// Quadratic Bezier Interpolation with Smooth Ease
		public static FInt QuadBezierEaseBothDir( FInt p0, FInt p1, FInt p2, FInt weight )  {
			weight = FInt.Sin(weight * FInt.PI * 2) / 2 + FInt.Create(0.5);
			FInt k = 1 - weight;
			return (k* k * p0) + (2 * (1 - weight) * weight * p1) + (weight* weight * p2);
		}
		
		// Quadratic Bezier Interpolation
		// https://en.wikipedia.org/wiki/Bezier_curve
		// p0, p1, p2 are Start Point, Control Point, End Point
		public static FInt QuadBezier( FInt p0, FInt p1, FInt p2, FInt weight) {
			FInt k = 1 - weight;
			return (k * k * p0) + (2 * (1 - weight) * weight * p1) + (weight * weight * p2);
		}

		// Get the speed needed to cover a distance over the time provided.
		public static FInt Speed( FInt distance, FInt time )  {
			return distance / time;
		}
	}
}
