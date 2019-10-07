﻿using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.ObjectComponents {

	public class Collision {

		private readonly DynamicGameObject refObject;
		private readonly Physics physics;
		private readonly Touch touch;

		public Collision( DynamicGameObject refObject ) {
			this.refObject = refObject;
			this.physics = refObject.physics;
			this.touch = this.physics.touch;
		}

		// A Standard Collision just triggers the collision methods that are commonly overridden (collideLeft(), collideRight(), etc).
		public void StandardCollision(DynamicGameObject obj, DynamicGameObject obj2, DirCardinal dir) {

			if(dir == DirCardinal.Down) {
				this.touch.TouchDown();
				obj2.physics.touch.TouchUp();
				this.CollideDown( obj2 );
				obj2.collision.CollideUp( obj );
				return;
			}

			if(dir == DirCardinal.Right) {
				this.touch.TouchRight();
				obj2.physics.touch.TouchLeft();
				this.CollideRight( obj2 );
				obj2.collision.CollideLeft( obj );
				return;
			}

			if(dir == DirCardinal.Left) {
				this.touch.TouchLeft();
				obj2.physics.touch.TouchRight();
				this.CollideLeft( obj2 );
				obj2.collision.CollideRight( obj );
				return;
			}

			if(dir == DirCardinal.Up) {
				this.touch.TouchUp();
				obj2.physics.touch.TouchDown();
				this.CollideUp( obj2 );
				obj2.collision.CollideDown( obj );
				return;
			}
		}

		public bool CollideUp(GameObject obj2) {

			// Verify the object is moving Up. If not, don't collide.
			// This prevents certain false collisions, e.g. if both objects are moving in the same direction.
			if(this.physics.velocity.Y.IntValue + this.physics.extraMovement.Y.IntValue <= 0) { return false; }

			CollideAffect.AlignVertical(this.refObject, obj2, -1);
			this.physics.StopY();

			return true;
		}

		public bool CollideDown( GameObject obj2 ) {

			// Verify the object is moving Down. If not, don't collide.
			// This prevents certain false collisions, e.g. if both objects are moving in the same direction.
			if(this.physics.velocity.Y.IntValue + this.physics.extraMovement.Y.IntValue >= 0) { return false; }

			CollideAffect.AlignVertical(this.refObject, obj2, 1);
			this.physics.StopY();

			return true;
		}

		public bool CollideLeft( GameObject obj2 ) {

			// Verify the object is moving Left. If not, don't collide.
			// This prevents certain false collisions, e.g. if both objects are moving in the same direction.
			if(this.physics.velocity.X.IntValue + this.physics.extraMovement.X.IntValue <= 0) { return false; }

			CollideAffect.AlignHorizontal(this.refObject, obj2, -1);
			this.physics.StopX();

			return true;
		}

		public bool CollideRight( GameObject obj2 ) {

			// Verify the object is moving Right. If not, don't collide.
			// This prevents certain false collisions, e.g. if both objects are moving in the same direction.
			if(this.physics.velocity.X.IntValue + this.physics.extraMovement.X.IntValue >= 0) { return false; }

			CollideAffect.AlignHorizontal(this.refObject, obj2, 1);
			this.physics.StopX();

			return true;
		}
	}
}
