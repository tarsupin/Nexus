using Nexus.Engine;
using Nexus.GameEngine;
using Newtonsoft.Json.Linq;

namespace Nexus.Objects {

	public class EnemyLand : Enemy {

		protected FInt speed;

		public EnemyLand(LevelScene scene, byte subType, FVector pos, JObject paramList) : base(scene, subType, pos, paramList) {

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
