using System;

namespace Nexus.Engine {

	public static class TrigCalc {

		/*************************
		****** Trigonometry ******
		*************************/

		public static int GetDistance(int x1, int y1, int x2, int y2) {
			return (int) Math.Floor(Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)));
		}
	}
}
