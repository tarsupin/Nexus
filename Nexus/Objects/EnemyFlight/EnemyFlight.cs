using Nexus.Engine;
using Nexus.GameEngine;
using System;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class EnemyFlight : Enemy {

		public bool shellCollision = false;

		public EnemyFlight(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {

		}

		public override void RunTick() {
			base.RunTick();

			// Update Facing Direction based on speed
			float xVel = (float) this.physics.velocity.X.ToDouble();

			if(Math.Abs(xVel) >= 0.4f) {
				if(this.FaceRight) {
					if(xVel < 0) {
						this.SetDirection(false);
					}
				} else {
					if(xVel > 0) {
						this.SetDirection(true);
					}
				}
			}
		}

		public override bool DamageByTNT() { return false; }
	}
}
