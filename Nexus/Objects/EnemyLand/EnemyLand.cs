using Nexus.Engine;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class EnemyLand : Enemy {

		protected FInt speed;

		public EnemyLand(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {

		}
		
		public override void CollideTileLeft(int posX) {
			base.CollideTileLeft(posX);
			this.WalkRight();
		}

		public override void CollideTileRight(int posX) {
			base.CollideTileRight(posX);
			this.WalkLeft();
		}

		public void WalkLeft() {
			if(this.physics.touch.toFloor > 2) { return; }
			this.SetDirection(false);
		}

		public void WalkRight() {
			if(this.physics.touch.toFloor > 2) { return; }
			this.SetDirection(true);
		}
	}
}
