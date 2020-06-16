using System;

namespace Nexus.Engine {

	public static class Snap {

		// Snap: Returns the canvas' value to the nearest interval; e.g. 552 -> 550
		public static int ToCeil(short interval, int value ) { return interval * (int) Math.Ceiling( (double) (value / interval) ); }
		public static int ToFloor(short interval, int value ) { return interval * (int) Math.Floor( (double) (value / interval) ); }
		public static int ToRound(short interval, int value ) { return interval * (int) Math.Round( (double) (value / interval) ); }
		
		// Grid: Returns the X or Y of the grid cell; e.g. 552 -> 55
		public static int GridCeil(short interval, int value ) { return (int) Math.Ceiling( (double) (value / interval) ); }
		public static int GridFloor(short interval, int value ) { return (int) Math.Floor( (double) (value / interval) ); }
		public static int GridRound(short interval, int value ) { return (int) Math.Round( (double) (value / interval) ); }
		
		// World: Returns world X,Y based on a grid cell; e.g. gridX of 5 => worldX of 250
		public static int GridToPos(short interval, int value ) { return (int) Math.Floor( (double) (value * interval) ); }
	}
}
