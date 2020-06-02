﻿using System;

namespace Nexus.Engine {

	public static class TrigCalc {

		/*************************
		****** Trigonometry ******
		*************************/

		public static int GetDistance(int x1, int y1, int x2, int y2) {
			return (int) Math.Floor(Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)));
		}

		public static FInt GetDistanceFInt(FVector pos1, FVector pos2) {
			int dx = pos2.X.RoundInt - pos1.X.RoundInt;
			int dy = pos2.Y.RoundInt - pos1.Y.RoundInt;
			return FInt.Create(Math.Sqrt((dx * dx) + (dy * dy)));
		}
	}
}
