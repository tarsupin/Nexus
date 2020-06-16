using System;

namespace Nexus.Engine {

	public static class Coords {

		// Maps X,Y Coordinate to Single Integer
		public static int MapToInt(int x, int y) {
			return x * 2048 + y;
		}

		// Converts Integer to X,Y Coordinate (Dynamic Size, No Collisions)
		public static (int x, int y) GetFromInt( int num ) {
			return (num / 2048, num % 2048);
		}

		// Test Coordinate Mapping
		public static void TestCoordIntMap(int lenX = 50, int lenY = 50) {
			for(int x = 0; x < lenX; x++) {
				for(int y = 0; y < lenY; y++) {
					int mapNum = Coords.MapToInt(x, y);
					(int x, int y) coords = Coords.GetFromInt(mapNum);
					if(coords.x != x || coords.y != y) {
						throw new Exception("Coordinate Conversion Broke at " + x.ToString() + ", " + y.ToString());
					}
				}
			}
		}
	}

	public static class CoordsDiag {

		// Maps X,Y Coordinate to Single Integer (Dynamic Size, No Collisions)
		public static int MapToInt( int x, int y ) {
			if(y == x) { return (int) Math.Pow(x + 1, 2); }
			int max = Math.Max(x, y);
			int diag = (int) Math.Pow(max, 2);
			return diag + 1 + (y < x ? y : x + max);
		}

		// Converts Integer to X,Y Coordinate (Dynamic Size, No Collisions)
		public static (int x, int y) GetFromInt( int num ) {
			int max = (int) Math.Floor(Math.Sqrt(num - 1));
			int diagHigh = (int) Math.Pow(max + 1, 2);
			if(diagHigh == num) { return (max, max); }
			int dist = diagHigh - num;
			if(dist <= max) { return (max - dist, max); } // Y is Max
			return (max, max - (dist - max)); // X is Max
		}

		// Test Coordinate Mapping
		public static void TestCoordIntMap(int lenX = 50, int lenY = 50) {
			for(int x = 0; x < lenX; x++) {
				for(int y = 0; y < lenY; y++) {
					int mapNum = Coords.MapToInt(x, y);
					(int x, int y) coords = Coords.GetFromInt(mapNum);
					if(coords.x != x || coords.y != y) {
						throw new Exception("Coordinate Conversion Broke at " + x.ToString() + ", " + y.ToString());
					}
				}
			}
		}
	}
}
