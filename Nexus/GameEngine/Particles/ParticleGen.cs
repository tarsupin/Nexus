using Microsoft.Xna.Framework;
using Nexus.Engine;
using System;

namespace Nexus.GameEngine {

	public class ParticleGen {

		short[] x = new short[140] { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36, 39, 42, 45, 48, 51, 54, 57, 60, 63, 66, 69, 72, 75, 78, 81, 84, 87, 90, 93, 96, 99, 102, 105, 108, 111, 114, 117, 120, 123, 126, 129, 132, 135, 138, 141, 144, 147, 150, 153, 156, 159, 162, 165, 168, 171, 174, 177, 180, 183, 186, 189, 192, 195, 198, 201, 204, 207, 210, 213, 216, 219, 222, 225, 228, 231, 234, 237, 240, 243, 246, 249, 252, 255, 258, 261, 264, 267, 270, 273, 276, 279, 282, 285, 288, 291, 294, 297, 300, 303, 306, 309, 312, 315, 318, 321, 324, 327, 330, 333, 336, 339, 342, 345, 348, 351, 354, 357, 360, 363, 366, 369, 372, 375, 378, 381, 384, 387, 390, 393, 396, 399, 402, 405, 408, 411, 414, 417, 420, };
		short[] y = new short[140] { -10, -20, -28, -37, -45, -52, -60, -66, -72, -78, -82, -87, -91, -94, -98, -100, -102, -104, -104, -105, -105, -104, -104, -102, -100, -98, -94, -91, -87, -82, -78, -72, -66, -60, -52, -46, -38, -32, -24, -18, -10, -4, 4, 10, 18, 24, 32, 38, 46, 52, 60, 66, 74, 80, 88, 94, 102, 108, 116, 122, 130, 136, 144, 150, 158, 164, 172, 178, 186, 192, 200, 206, 214, 220, 228, 234, 242, 248, 256, 262, 270, 276, 284, 290, 298, 304, 312, 318, 326, 332, 340, 346, 354, 360, 368, 374, 382, 388, 396, 402, 410, 416, 424, 430, 438, 444, 452, 458, 466, 472, 480, 486, 494, 500, 508, 514, 522, 528, 536, 542, 550, 556, 564, 570, 578, 584, 592, 598, 606, 612, 620, 626, 634, 640, 648, 654, 662, 668, 676, 682, };

		public ParticleGen() {

		}

		public static void GenGravityBurstRandom() {

		}

		public static void GenGravityBurst( float velX, float velY, float maxVelY = 7, float gravity = 0.499f, int numberSteps = 140 ) {

			string xStr = "short[] x = new short[" + numberSteps + "] {";
			string yStr = "short[] y = new short[" + numberSteps + "] {";

			Vector2 pos = new Vector2(0, 0);

			for( int i = 0; i < numberSteps; i++ ) {

				// Update Position by Velocity and Gravity
				pos.X += velX;
				pos.Y += velY;
				velY = Math.Min(maxVelY, velY + gravity);

				xStr += Math.Round(pos.X).ToString() + ", ";
				yStr += Math.Round(pos.Y).ToString() + ", ";
			}

			xStr += "};";
			yStr += "};";

			// Output the Generator in Text
			System.Console.WriteLine(xStr);
			System.Console.WriteLine(yStr);
		}

		// In Degrees: Angle of Starting Degree, Distance from Start, Rotation Lerp, Speed that Spiral Grows
		public static Vector2 SpiralStep( float startingDegree, float startDist, float rotationLerp, float growLerp, int step = 0 ) {

			float curDegree = Degrees.Normalize(startingDegree + (rotationLerp * step));
			float curDist = startDist + (growLerp * step);

			// Get Coordinates
			float x = Degrees.GetXFromRotation(curDegree, curDist);
			float y = Degrees.GetYFromRotation(curDegree, curDist);

			return new Vector2(x, y);
		}

		public static Vector2 CurveMotion( float xStart, float xMid, float xEnd, float yStart, float yMid, float yEnd, float weight ) {

			// Get Coordinates
			float x = Interpolation.QuadBezier(xStart, xMid, xEnd, weight);
			float y = Interpolation.QuadBezier(yStart, yMid, yEnd, weight);

			return new Vector2(x, y);
		}

		// fadeStart refers to the weight point when it will begins to transition alpha to more faded.
		public static float AlphaByFadeWeight(float alphaStart, float alphaEnd, float fadeStart, float weight) {
			if(fadeStart >= weight) { return alphaStart; }
			float stepMult = 1 / fadeStart;
			float actualWeight = (weight - fadeStart) * stepMult;
			return Interpolation.Number(alphaStart, alphaEnd, actualWeight);
		}

	}
}
