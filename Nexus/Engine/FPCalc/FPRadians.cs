using System;

namespace Nexus.Engine {

	public static class FPRadians {
		
		// Convert between degrees and percent
		public static FInt ConvertToDegrees( FInt radian ) { return radian * 180 / FInt.PI; }
		public static FInt ConvertToPercent( FInt radian ) { return FPSpectrum.GetPercentFromValue( FPRadians.Wrap(radian), (0 - FInt.PI), FInt.PI) ; }

		// Shift degrees to valid ranges.
		public static FInt Wrap( FInt radian ) { return FPSpectrum.Wrap(radian, FInt.PI, FInt.PI); }
	
		// Normalize radians to valid range: 0 to 2pi
		public static FInt Normalize( FInt radian ) {
			radian %= (2 * FInt.PI);
			return radian >= 0 ? radian : radian + (2 * FInt.PI);
		}

		// Reverse / Invert Radians
		public static FInt Reverse( FInt radian ) { return FPRadians.Normalize(radian + FInt.PI); }
	
		// Find the radians between two coordinates
		public static FInt GetRadiansBetweenCoords( int x1, int y1, int x2, int y2 ) { return FInt.Atan2(FInt.Create(y2 - y1), FInt.Create(x2 - x1)); }

		// Get the X, Y from a rotation
		public static FInt GetXFromRotation(FInt radian, FInt distance) { return distance * FInt.Cos(radian); }
		public static FInt GetYFromRotation(FInt radian, FInt distance) { return distance * FInt.Sin(radian); }

		// Get the X, Y offset from a rotation and coordinate offset.
		public static FInt GetXOffsetFromRotation( FInt radian, int xOffset, int yOffset ) {
			return xOffset * FInt.Cos(radian) + yOffset * FInt.Sin(radian);
		}

		public static FInt GetYOffsetFromRotation( FInt radian, int xOffset, int yOffset ) {
			return -yOffset * FInt.Cos(radian) + xOffset * FInt.Sin(radian);
		}

		// Rotate a Radian
		public static FInt Rotate( FInt radian, FInt rotate ) { return (radian + rotate) % (FInt.PI * 2); }

		// Rotates radian to targetRadian, taking the shortest distance. Lerp argument is amount to rotate by.
		// Note: lerp of 0.05 may be a good starting point.
		public static FInt RotateTo( FInt radian, FInt targetRadian, FInt lerp ) {

			// Return Target Radian if the value has been reached.
			if(FInt.Abs(targetRadian - radian) <= lerp || FInt.Abs(targetRadian - radian) >= (FInt.PI * 2 - lerp)) {
				return targetRadian;
			}

			// Clamp / Wrap Target Radian as needed.
			if(FInt.Abs(targetRadian - radian) > FInt.PI) {
				if(targetRadian < radian) { targetRadian += FInt.PI * 2; }
				else { targetRadian -= FInt.PI * 2; }
			}

			// Return Radian Rotation
			if(targetRadian > radian) { radian += lerp; }
			else { radian -= lerp; }

			return radian;
		}
	}
}
