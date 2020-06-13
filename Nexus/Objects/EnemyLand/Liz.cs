using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum LizSubType : byte {
		Liz = 0,
	}

	public class Liz : EnemyLand {

		public Liz(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Liz].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));

			// Sub-Types
			this.AssignSubType(subType);

			// Speed Handling
			this.speed = FInt.Create(0.8);
			this.physics.velocity.X = (FInt)(0 - this.speed);

			// Bounds
			this.AssignBoundsByAtlas(12, 4, -4);
		}

		public override void OnStateChange() {
			if(this.State == (byte)CommonState.Move || this.State == (byte)CommonState.MotionEnd) {
				this.animate.SetAnimation("Liz/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 12);
			} else {
				this.SetSpriteName("Liz/" + (this.FaceRight ? "Right" : "Left") + (this.State == (byte)CommonState.SpecialWait ? "1" : "3"));
			}
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) LizSubType.Liz) {
				this.animate = new Animate(this, "Liz/");
				this.behavior = new ChargeBehavior(this, 6, 0, true);
				((ChargeBehavior) this.behavior).SetBehavePassives(120, 30, 30, 3);
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.physics.velocity.X = this.speed * (this.FaceRight ? 1 : -1);
			this.animate.SetAnimation("Liz/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 15);
		}
	}
}
