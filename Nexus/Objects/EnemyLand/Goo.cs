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

		public string subTypeStr;

		public Goo(RoomScene room, byte subType, FVector pos, JObject paramList) : base(room, subType, pos, paramList) {
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
				this.subTypeStr = "Green";
			}
			
			// Orange Goo
			else if(subType == (byte) GooSubType.Orange) {
				this.animate = new Animate(this, "Goo/Orange/");
				this.speed = FInt.Create(1.5);
				this.subTypeStr = "Orange";
			}

			// Blue Goo
			else if(subType == (byte) GooSubType.Blue) {
				this.animate = new Animate(this, "Goo/Blue/");
				this.behavior = new HopConstantBehavior(this, 6, 0);
				this.subTypeStr = "Blue";
			}

			this.OnDirectionChange();
		}

		public override void OnDirectionChange() {
			if(this.subType != (byte) GooSubType.Blue) {
				this.physics.velocity.X = this.speed * (this.FaceRight ? 1 : -1);
				this.animate.SetAnimation("Goo/" + subTypeStr + (this.FaceRight ? "/Right" : "/Left"), AnimCycleMap.Cycle2, 15);
			}
		}
	}
}
