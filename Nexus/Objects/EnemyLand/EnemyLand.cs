using Nexus.Engine;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class EnemyLand : Enemy {

		protected FInt speed;

		public EnemyLand(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {

		}

		public void WalkLeft() {
			if(!this.physics.touch.toFloor) { return; }
			this.SetDirection(false);
			this.physics.velocity.X = this.speed.Inverse;
		}

		public void WalkRight() {
			if(!this.physics.touch.toFloor) { return; }
			this.SetDirection(true);
			this.physics.velocity.X = this.speed;
		}
	}
}
