using System;

namespace Nexus.Engine {

	public static class Degrees {

		// Conversions
		public static float ConvertToPercent(float degrees) { return Spectrum.GetPercentFromValue(Radians.Wrap(degrees), -180f, 180f); }
		public static float ConvertToRadians(float degrees) { return degrees * (float) Math.PI / 180f; }

		// Shift degrees to valid ranges.
		public static float Wrap( float degrees ) { return Spectrum.Wrap(degrees, -180f, 180f); }
		public static float Normalize( float degrees ) { return Spectrum.Wrap(degrees, 0f, 360f); }
	
		// Reverse the angle
		public static float Reverse( float degrees ) { return Degrees.Normalize(degrees + 180f); }
		
		// Get X, Y coordinates from degree and angle
		public static float GetXFromRotation(float degrees, float distance) { return Radians.GetXFromRotation(Degrees.ConvertToRadians(degrees), distance); }
		public static float GetYFromRotation(float degrees, float distance) { return Radians.GetYFromRotation(Degrees.ConvertToRadians(degrees), distance); }

		// Get the shortest distance between two angles in -180 to 180 range.
		// If result is >= 0, rotation is counter-clockwise. If <= 0, rotation is clockwise.
		public static float ShortestAngle( float angle1, float angle2 ) {
			float difference = angle2 - angle1;
			float times = (int) Math.Floor((difference - (-180f)) / 360f);
			return difference - (times * 360f);
		}

		// Find the degrees between two coordinates
		public static float GetDegreesBetweenCoords( int x1, int y1, int x2, int y2 ) {
			return (float) Math.Atan2(y2 - y1, x2 - x1) * 180 / (float) Math.PI;
		}

		// Rotates degree to targetDegree (between 0 and 360), taking the shortest distance. Lerp argument is amount to rotate by.
		// Note: Lerp of 0.5 may be good place to start.
		public static float RotateTo( float degree, float targetDegree, float lerp ) {
			float diff = targetDegree - degree + 360f;

			// Return Target Degree if it's been matched.
			if(Math.Abs(diff) <= lerp) { return targetDegree; }

			// Otherwise, return next step:
			if(diff > 0f) { degree += lerp; } else { degree -= lerp; }

			return Degrees.Normalize(degree);
		}
	}
}
