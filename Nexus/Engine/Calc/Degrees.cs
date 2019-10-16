using System;

namespace Nexus.Engine {

	public static class Degrees {

		// Conversions
		public static FInt ConvertToRadians( FInt degrees ) { return degrees * FInt.PI / 180; }
		public static FInt ConvertToPercent( FInt degrees ) { return Spectrum.GetPercentFromValue( Radians.Wrap( degrees ), FInt.Create(-180), FInt.Create(180) ); }

		// Shift degrees to valid ranges.
		public static FInt Wrap( FInt degrees ) { return Spectrum.Wrap(degrees, FInt.Create(-180), FInt.Create(180)); }
		public static FInt Normalize( FInt degrees ) { return Spectrum.Wrap(degrees, FInt.Create(0), FInt.Create(360)); }
	
		// Reverse the angle
		public static FInt Reverse( FInt degrees ) { return Degrees.Normalize(degrees + 180); }
		
		// Get X, Y coordinates from degree and angle
		public static FInt GetXFromRotation(FInt degrees, FInt distance) { return Radians.GetXFromRotation(Degrees.ConvertToRadians(degrees), distance); }
		public static FInt GetYFromRotation(FInt degrees, FInt distance) { return Radians.GetYFromRotation(Degrees.ConvertToRadians(degrees), distance); }

		// Get the shortest distance between two angles in -180 to 180 range.
		// If result is >= 0, rotation is counter-clockwise. If <= 0, rotation is clockwise.
		public static FInt ShortestAngle( FInt angle1, FInt angle2 ) {
			FInt difference = angle2 - angle1;
			FInt times = FInt.Create(Math.Floor(((double) difference - (-180)) / 360));
			return difference - (times * 360);
		}

		// Find the degrees between two coordinates
		public static FInt GetDegreesBetweenCoords( int x1, int y1, int x2, int y2 ) {
			return FInt.Atan2(FInt.Create(y2 - y1), FInt.Create(x2 - x1)) * 180 / FInt.PI;
		}

		// Rotates degree to targetDegree (between 0 and 360), taking the shortest distance. Lerp argument is amount to rotate by.
		// Note: Lerp of 0.5 may be good place to start.
		public static FInt RotateTo( FInt degree, FInt targetDegree, FInt lerp ) {
			FInt diff = targetDegree - degree + 360;

			// Return Target Degree if it's been matched.
			if(FInt.Abs(diff) <= lerp) { return targetDegree; }

			// Otherwise, return next step:
			if(diff > 0) { degree += lerp; } else { degree -= lerp; }

			return Degrees.Normalize(degree);
		}
	}
}
