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
				actor.CollideObjDown(obj);
				obj.CollideObjUp(actor);
				return true;
			}

			if(dir == DirCardinal.Right) {
				actor.CollideObjRight(obj);
				obj.CollideObjLeft(actor);
				return true;
			}

			if(dir == DirCardinal.Left) {
				actor.CollideObjLeft(obj);
				obj.CollideObjRight(actor);
				return true;
			}

			if(dir == DirCardinal.Up) {
				actor.CollideObjUp(obj);
				obj.CollideObjDown(actor);
				return true;
			}

			return false;
		}
	}
}
