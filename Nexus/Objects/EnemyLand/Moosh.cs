﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public enum MooshSubType : byte {
		Brown,
		Purple,
		White,
	}

	public class Moosh : EnemyLand {

		public Moosh(LevelScene scene, byte subType, FVector pos, object[] paramList) : base(scene, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyLand];

			// Movement
			this.speed = FInt.Create(0.4);

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.7));
			this.physics.velocity.X = (FInt)(0 - this.speed);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(4, 4, -4);
		}

		public override void OnStateChange() {

			// White Moosh
			if(this.subType == (byte) MooshSubType.White) {
				if(this.State == ActorState.RestStall) {
					this.SetSpriteName("Moosh/White/" + (this.FaceRight ? "Right" : "Left") + "1");
				}
				
				// States: MoveAir, MoveStandard
				else {
					this.SetSpriteName("Moosh/White/" + (this.FaceRight ? "Right" : "Left") + (this.State == ActorState.MoveAir ? "3" : "2"));
				}
			}
			
			// Brown & Purple Moosh
			else {

				if(this.State == ActorState.MoveStandard || this.State == ActorState.MoveLand || this.State == ActorState.BehaviorStandard) {
					if(this.FaceRight) { this.WalkRight(); } else { this.WalkLeft(); }
					this.animate.SetAnimation("Moosh/" + (this.subType == (byte) MooshSubType.Brown ? "Brown/" : "Purple/") + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 12);
				}

				// States: ReactionCharacter, ReactionStall, RestStunned
				else if(this.State == ActorState.ReactionCharacter) {
					string animSpeed = this.State == ActorState.ReactionCharacter ? "3" : "1";
					this.SetSpriteName("Moosh/" + (this.subType == (byte) MooshSubType.Brown ? "Brown/" : "Purple/") + (this.FaceRight ? "Right" : "Left") + animSpeed);
				}
			}
		}

		private void AssignSubType( byte subType ) {

			if(subType == (byte) MooshSubType.Brown) {
				this.animate = new Animate(this, "Moosh/Brown/");
				this.behavior = new ChargeBehavior(this, 3, 11);
				((ChargeBehavior) this.behavior).SetBehavePassives(120, 35, 30, 9);
				this.SetState(ActorState.MoveStandard);

			} else if(subType == (byte) MooshSubType.White) {
				this.speed = FInt.Create(0);
				this.behavior = new HopConstantBehavior(this);
				this.SetState(ActorState.RestStall);

			} else if(subType == (byte) MooshSubType.Purple) {
				this.animate = new Animate(this, "Moosh/Purple/");
				this.behavior = new ChargeBehavior(this, 1, 9, 30, 15);
				((ChargeBehavior) this.behavior).SetBehavePassives(120, 35, 30, 11);
				this.SetState(ActorState.MoveStandard);
			}
		}

		public override bool GetJumpedOn( Character character, sbyte bounceStrength = 3 ) {
			return base.GetJumpedOn(character, 3);
		}
	}
}
