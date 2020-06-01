using System;

namespace Nexus.Engine {

	public static class Coords {

		// Maps X,Y Coordinate to Single Integer (Dynamic Size, No Collisions)
		public static uint MapToInt( uint x, uint y ) {
			if(y == x) { return (uint) Math.Pow(x + 1, 2); }
			uint max = Math.Max(x, y);
			uint diag = (uint) Math.Pow(max, 2);
			return diag + 1 + (y < x ? y : x + max);
		}

		// Converts Integer to X,Y Coordinate (Dynamic Size, No Collisions)
		public static (uint x, uint y) GetFromInt( uint num ) {
			uint max = (uint) Math.Floor(Math.Sqrt(num - 1));
			uint diagHigh = (uint) Math.Pow(max + 1, 2);
			if(diagHigh == num) { return (max, max); }
			uint dist = diagHigh - num;
			if(dist <= max) { return (max - dist, max); } // Y is Max
			return (max, max - (dist - max)); // X is Max
		}

		// Test Coordinate Mapping
		public static void TestCoordIntMap(uint lenX = 50, uint lenY = 50) {
			for(uint x = 0; x < lenX; x++) {
				for(uint y = 0; y < lenY; y++) {
					uint mapNum = Coords.MapToInt(x, y);
					(uint x, uint y) coords = Coords.GetFromInt(mapNum);
					if(coords.x != x || coords.y != y) {
						throw new Exception("Coordinate Conversion Broke at " + x.ToString() + ", " + y.ToString());
					}
				}
			}
		}
	}
}
