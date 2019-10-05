using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public class EnemyLand : Enemy {

		public EnemyLand(LevelScene scene, byte subType, FVector pos, object[] paramList) : base(scene, subType, pos, paramList) {

		}

		public void WalkLeft() {
			//if(!this.physics.touching.floor) { return; }
			//this.status.faceRight = false;
			//this.physics.velocity.x = -this.speed;
			//if(this.animation) { this.animation.setAnimation("MoveLeft"); }
		}

		public void WalkRight() {
			//if(!this.physics.touching.floor) { return; }
			//this.status.faceRight = true;
			//this.physics.velocity.x = this.speed;
			//if(this.animation) { this.animation.setAnimation("MoveRight"); }
		}
	}
}
