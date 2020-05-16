using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.ObjectComponents {

	public static class Impact {

		// A Standard Impact just triggers the collision methods that are commonly overridden (collideLeft(), collideRight(), etc).
		public static bool StandardImpact( DynamicObject actor, DynamicObject obj, DirCardinal dir = DirCardinal.Center) {

			if(dir == DirCardinal.Center) {
				return false;
			}

			if(dir == DirCardinal.Down) {
				actor.physics.touch.TouchDown();
				obj.physics.touch.TouchUp();
				Impact.CollideDown(actor, obj);
				Impact.CollideUp(obj, actor);
				return true;
			}

			if(dir == DirCardinal.Right) {
				actor.physics.touch.TouchRight();
				obj.physics.touch.TouchLeft();
				Impact.CollideRight(actor, obj);
				Impact.CollideLeft(obj, actor);
				return true;
			}

			if(dir == DirCardinal.Left) {
				actor.physics.touch.TouchLeft();
				obj.physics.touch.TouchRight();
				Impact.CollideLeft(actor, obj);
				Impact.CollideRight(obj, actor);
				return true;
			}

			if(dir == DirCardinal.Up) {
				actor.physics.touch.TouchUp();
				obj.physics.touch.TouchDown();
				Impact.CollideUp(actor, obj);
				Impact.CollideDown(obj, actor);
				return true;
			}

			return false;
		}

		public static bool CollideUp( DynamicObject actor, DynamicObject obj ) {

			// Verify the object is moving Up. If not, don't collide.
			// This prevents certain false collisions, e.g. if both objects are moving in the same direction.
			if(actor.physics.velocity.Y + actor.physics.extraMovement.Y >= 0) { return false; }

			CollideAffect.AlignDown(actor, obj);
			actor.physics.StopY();

			return true;
		}

		public static bool CollideDown( DynamicObject actor, DynamicObject obj ) {

			// Verify the object is moving Down. If not, don't collide.
			// This prevents certain false collisions, e.g. if both objects are moving in the same direction.
			if(actor.physics.velocity.Y + actor.physics.extraMovement.Y <= 0) { return false; }

			CollideAffect.AlignUp(actor, obj);
			actor.physics.StopY();

			return true;
		}

		public static bool CollideLeft( DynamicObject actor, DynamicObject obj ) {

			// Verify the object is moving Left. If not, don't collide.
			// This prevents certain false collisions, e.g. if both objects are moving in the same direction.
			if(actor.physics.velocity.X + actor.physics.extraMovement.X >= 0) { return false; }

			CollideAffect.AlignRight(actor, obj);
			actor.physics.StopX();

			return true;
		}

		public static bool CollideRight( DynamicObject actor, DynamicObject obj ) {

			// Verify the object is moving Right. If not, don't collide.
			// This prevents certain false collisions, e.g. if both objects are moving in the same direction.
			if(actor.physics.velocity.X + actor.physics.extraMovement.X <= 0) { return false; }

			CollideAffect.AlignLeft(actor, obj);
			actor.physics.StopX();

			return true;
		}
	}
}
