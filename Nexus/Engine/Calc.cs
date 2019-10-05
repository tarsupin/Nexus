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
	}
}
