using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Newtonsoft.Json.Linq;

namespace Nexus.Objects {

	public enum GooSubType : byte {
		Green,
		Orange,
		Blue,
	}

	public class Goo : EnemyLand {

		public Goo(LevelScene scene, byte subType, FVector pos, JObject paramList) : base(scene, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyLand];

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(4, 2, -2);
		}

		private void AssignSubType( byte subType ) {

			// Green Goo
			if(subType == (byte)GooSubType.Green) {
				this.animate = new Animate(this, "Goo/Green/");
				this.speed = FInt.Create(0.8);
			}
			
			// Orange Goo
			else if(subType == (byte) GooSubType.Orange) {
				this.animate = new Animate(this, "Goo/Orange/");
				this.speed = FInt.Create(1.5);
			}

			// Blue Goo
			else if(subType == (byte) GooSubType.Blue) {
				this.animate = new Animate(this, "Goo/Blue/");
				this.behavior = new HopConstantBehavior(this, 6, 0);
			}

			this.OnDirectionChange();
		}

		public override void OnDirectionChange() {
			if(this.subType != (byte) GooSubType.Blue) {
				this.physics.velocity.X = this.speed * (this.FaceRight ? 1 : -1);
				this.animate.SetAnimation("Goo/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle2, 15);
			}
		}
	}
}
