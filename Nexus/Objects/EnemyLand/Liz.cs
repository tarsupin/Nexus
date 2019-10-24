using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Newtonsoft.Json.Linq;

namespace Nexus.Objects {

	public enum LizSubType : byte {
		Liz,
	}

	public class Liz : EnemyLand {

		public Liz(LevelScene scene, byte subType, FVector pos, JObject paramList) : base(scene, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyLand];

			// Movement
			this.speed = FInt.Create(0.8);

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));
			this.physics.velocity.X = (FInt)(0 - this.speed);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(12, 4, -4);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) LizSubType.Liz) {
				this.animate = new Animate(this, "Liz/");
				this.behavior = new ChargeBehavior(this, 6, 0);
				((ChargeBehavior) this.behavior).SetBehavePassives(120, 30, 30, 10);
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.animate.SetAnimation("Liz/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 15);
		}
	}
}
