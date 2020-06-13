using System;

namespace Nexus.Engine {

	public static class FPTrigCalc {

		/*************************
		****** Trigonometry ******
		*************************/

		public static FInt GetDistance(FVector pos1, FVector pos2) {
			int dx = pos2.X.RoundInt - pos1.X.RoundInt;
			int dy = pos2.Y.RoundInt - pos1.Y.RoundInt;
			return FInt.Create(Math.Sqrt((dx * dx) + (dy * dy)));
		}
	}
}
