using Nexus.Engine;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class EnemyFlight : Enemy {

		public EnemyFlight(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {

		}

		public override void RunTick() {
			base.RunTick();
			if(this.FaceRight) {
				if(this.physics.velocity.X <= 1) {
					this.SetDirection(false);
				}
			} else {
				if(this.physics.velocity.X >= 1) {
					this.SetDirection(true);
				}
			}
		}

		public override bool DamageByTNT() { return false; }
	}
}
