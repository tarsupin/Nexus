using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.ObjectComponents {

	public class TileImpact {

		private readonly TileGameObject refTile;

		public TileImpact(TileGameObject refTile ) {
			this.refTile = refTile;
		}

		// A Standard Impact just triggers the collision methods that are commonly overridden (collideLeft(), collideRight(), etc).
		public bool StandardCollision(DynamicGameObject actor, DirCardinal dir = DirCardinal.Center) {

			if(dir == DirCardinal.Center) {
				return false;
			}

			//if(dir == DirCardinal.Down) {
			//	actor.physics.touch.TouchUp();
			//	actor.impact.CollideUp(this.refTile);
			//	return true;
			//}

			//if(dir == DirCardinal.Right) {
			//	actor.physics.touch.TouchLeft();
			//	actor.impact.CollideLeft(this.refTile);
			//	return true;
			//}

			//if(dir == DirCardinal.Left) {
			//	actor.physics.touch.TouchRight();
			//	actor.impact.CollideRight(this.refTile);
			//	return true;
			//}

			//if(dir == DirCardinal.Up) {
			//	actor.physics.touch.TouchDown();
			//	actor.impact.CollideDown(this.refTile);
			//	return true;
			//}

			return false;
		}
	}
}
