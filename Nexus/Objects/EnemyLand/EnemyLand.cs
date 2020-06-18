using Nexus.Engine;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class EnemyLand : Enemy {

		protected FInt speed;

		public EnemyLand(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {

		}
		
		public override bool CollideObjLeft(GameObject obj) {
			if(!base.CollideObjLeft(obj)) { return false; }
			this.WalkRight();
			return true;
		}

		public override bool CollideObjRight(GameObject obj) {
			if(!base.CollideObjRight(obj)) { return false; }
			this.WalkLeft();
			return true;
		}
		
		public override void CollidePosLeft(int posX) {
			base.CollidePosLeft(posX);
			this.WalkRight();
		}

		public override void CollidePosRight(int posX) {
			base.CollidePosRight(posX);
			this.WalkLeft();
		}

		public void WalkLeft() {
			if(!this.physics.touch.toFloor) { return; }
			this.SetDirection(false);
		}

		public void WalkRight() {
			if(!this.physics.touch.toFloor) { return; }
			this.SetDirection(true);
		}
	}
}
