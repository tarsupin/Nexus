using Nexus.Engine;

namespace Nexus.GameEngine {

	public static class DrawTracker {

		public static int drawFrame = 0;
		public static short gridX = 0;
		public static short gridY = 0;

		public static bool AttemptDraw( short gridX, short gridY ) {
			int curFrame = Systems.timer.Frame;

			// If the last draw occurred within the last 100ms on this tile, return early to prevent repeat draws:
			if(DrawTracker.drawFrame > curFrame - 100 && DrawTracker.gridX == gridX && DrawTracker.gridY == gridY) {
				DrawTracker.drawFrame = curFrame;
				return false;
			}

			// Update Last Draw
			DrawTracker.drawFrame = curFrame;
			DrawTracker.gridX = gridX;
			DrawTracker.gridY = gridY;

			return true;
		}
	}
}
