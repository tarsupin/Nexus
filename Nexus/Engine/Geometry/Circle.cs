using System;

namespace Nexus.Engine {

	public static class Circle {

		// Measurements of Standard Circle Calculations
		public static double GetArea(int radius) { return Math.PI * radius * radius; }
		public static double GetCircumference(int radius) { return Math.PI * radius * 2; }
		
		// Get Circle Edge Positions
		public static int GetLeftEdge(int x, int radius) { return x - radius; }
		public static int GetRightEdge(int x, int radius) { return x + radius; }
		public static int GetTopEdge(int y, int radius) { return y - radius; }
		public static int GetBottomEdge(int y, int radius) { return y + radius; }
		
		// Get Point on Circle Circumference based on Radian
		public static float GetEdgePointByRadianX( int x, float radius, float radian ) { return (float) (x + (radius * Math.Cos(radian))); }
		public static float GetEdgePointByRadianY( int y, float radius, float radian ) { return (float) (y + (radius * Math.Sin(radian))); }

		// Get Point on Circle Circumference based on Degrees
		public static float GetEdgePointByDegreeX( int x, float radius, int degree ) { return Circle.GetEdgePointByRadianX(x, radius, Degrees.ConvertToRadians(degree)); }
		public static float GetEdgePointByDegreeY( int y, float radius, int degree) { return Circle.GetEdgePointByRadianX(y, radius, Degrees.ConvertToRadians(degree)); }

		// Check if Circle contains X, Y Coordinate
		public static bool DoesCircleContainPoint( int circleX, int circleY, int radius, int pointX, int pointY ) {
			float dx = (circleX - pointX) * (circleX - pointX);
			float dy = (circleY - pointY) * (circleY - pointY);
			return (dx + dy) <= (radius * radius);
		}
	}
}
