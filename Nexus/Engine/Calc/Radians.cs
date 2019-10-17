using System;

namespace Nexus.Engine {

	public static class Radians {
		
		// Convert between degrees and percent
		public static float ConvertToDegrees( float radian ) { return radian * 180 / (float) Math.PI; }
		public static float ConvertToPercent( float radian ) { return Spectrum.GetPercentFromValue( Radians.Wrap(radian), (0 - (float) Math.PI), (float) Math.PI) ; }

		// Shift degrees to valid ranges.
		public static float Wrap( float radian ) { return Spectrum.Wrap(radian, (float) Math.PI, (float) Math.PI); }
	
		// Normalize radians to valid range: 0 to 2pi
		public static float Normalize( float radian ) {
			radian %= (2 * (float) Math.PI);
			return radian >= 0 ? radian : radian + (2 * (float) Math.PI);
		}

		// Reverse / Invert Radians
		public static float Reverse( float radian ) { return Radians.Normalize(radian + (float) Math.PI); }
	
		// Find the radians between two coordinates
		public static float GetRadiansBetweenCoords( int x1, int y1, int x2, int y2 ) { return (float) Math.Atan2(y2 - y1, x2 - x1); }

		// Get the X, Y from a rotation
		public static float GetXFromRotation( float radian, float distance ) { return distance * (float) Math.Cos(radian); }
		public static float GetYFromRotation( float radian, float distance ) { return distance * (float) Math.Sin(radian); }

		// Get the X, Y offset from a rotation and coordinate offset.
		public static float GetXOffsetFromRotation( float radian, int xOffset, int yOffset ) {
			return xOffset * (float) Math.Cos(radian) + yOffset * (float) Math.Sin(radian);
		}

		public static float GetYOffsetFromRotation( float radian, int xOffset, int yOffset ) {
			return -yOffset * (float) Math.Cos(radian) + xOffset * (float) Math.Sin(radian);
		}

		// Rotate a Radian
		public static float Rotate( float radian, float rotate ) { return (radian + rotate) % ((float) Math.PI * 2); }

		// Rotates radian to targetRadian, taking the shortest distance. Lerp argument is amount to rotate by.
		// Note: lerp of 0.05 may be a good starting point.
		public static float RotateTo( float radian, float targetRadian, float lerp ) {

			// Return Target Radian if the value has been reached.
			if(Math.Abs(targetRadian - radian) <= lerp || Math.Abs(targetRadian - radian) >= ((float) Math.PI * 2 - lerp)) {
				return targetRadian;
			}

			// Clamp / Wrap Target Radian as needed.
			if(Math.Abs(targetRadian - radian) > (float) Math.PI) {
				if(targetRadian < radian) { targetRadian += (float) Math.PI * 2; }
				else { targetRadian -= (float) Math.PI * 2; }
			}

			// Return Radian Rotation
			if(targetRadian > radian) { radian += lerp; }
			else { radian -= lerp; }

			return radian;
		}
	}
}
