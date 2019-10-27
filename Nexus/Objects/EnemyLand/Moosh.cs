using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Newtonsoft.Json.Linq;

namespace Nexus.Objects {

	public enum MooshSubType : byte {
		Brown,
		Purple,
		White,
	}

	public class Moosh : EnemyLand {

		public Moosh(LevelScene scene, byte subType, FVector pos, JObject paramList) : base(scene, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyLand];

			// Movement
			this.speed = FInt.Create(0.4);

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.7));
			this.physics.velocity.X = 0 - this.speed;

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(4, 4, -4);
		}

		public override void OnStateChange() {

			// White Moosh
			if(this.subType == (byte) MooshSubType.White) {
				if(this.State == ActorState.Wait) {
					this.SetSpriteName("Moosh/White/" + (this.FaceRight ? "Right" : "Left") + "1");
				}
				
				// States: MoveAir, MoveStandard
				else {
					this.SetSpriteName("Moosh/White/" + (this.FaceRight ? "Right" : "Left") + (this.State == ActorState.Motion ? "3" : "2"));
				}
			}
			
			// Brown & Purple Moosh
			else {

				if(this.State == ActorState.Move || this.State == ActorState.MotionEnd || this.State == ActorState.Wait) {
					if(this.FaceRight) { this.WalkRight(); } else { this.WalkLeft(); }
					this.animate.SetAnimation("Moosh/" + (this.subType == (byte) MooshSubType.Brown ? "Brown/" : "Purple/") + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 12);
				}

				// States: ReactionCharacter, ReactionStall, RestStunned
				else if(this.State == ActorState.Special) {
					string frameNum = this.State == ActorState.Special ? "3" : "1";
					this.SetSpriteName("Moosh/" + (this.subType == (byte) MooshSubType.Brown ? "Brown/" : "Purple/") + (this.FaceRight ? "Right" : "Left") + frameNum);
				}
			}
		}

		private void AssignSubType( byte subType ) {

			if(subType == (byte) MooshSubType.Brown) {
				this.animate = new Animate(this, "Moosh/Brown/");
				this.behavior = new ChargeBehavior(this, 3, 11);
				((ChargeBehavior) this.behavior).SetBehavePassives(120, 35, 30, 9);
				this.SetState(ActorState.Move);

			} else if(subType == (byte) MooshSubType.White) {
				this.speed = FInt.Create(0);
				this.behavior = new HopConstantBehavior(this, 8, 2);
				this.SetState(ActorState.Wait);

			} else if(subType == (byte) MooshSubType.Purple) {
				this.animate = new Animate(this, "Moosh/Purple/");
				this.behavior = new ChargeBehavior(this, 1, 9, 30, 15);
				((ChargeBehavior) this.behavior).SetBehavePassives(120, 35, 30, 11);
				this.SetState(ActorState.Move);
			}
		}

		public override bool GetJumpedOn( Character character, sbyte bounceStrength = 3 ) {
			return base.GetJumpedOn(character, 3);
		}
	}
}
