using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.ObjectComponents {

	// Impacts Between Two Dynamic Objects
	public static class Impact {

		// A Standard Impact just triggers the collision methods that are commonly overridden (collideLeft(), collideRight(), etc).
		public static bool StandardImpact( DynamicObject actor, DynamicObject obj, DirCardinal dir = DirCardinal.Center) {

			if(dir == DirCardinal.Center) {
				return false;
			}

			if(dir == DirCardinal.Down) {
				actor.CollideDown(obj);
				obj.CollideUp(actor);
				return true;
			}

			if(dir == DirCardinal.Right) {
				actor.CollideRight(obj);
				obj.CollideLeft(actor);
				return true;
			}

			if(dir == DirCardinal.Left) {
				actor.CollideLeft(obj);
				obj.CollideRight(actor);
				return true;
			}

			if(dir == DirCardinal.Up) {
				actor.CollideUp(obj);
				obj.CollideDown(actor);
				return true;
			}

			return false;
		}
	}
}
