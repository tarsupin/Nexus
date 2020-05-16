using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum MooshSubType : byte {
		Brown,
		Purple,
		White,
	}

	public class Moosh : EnemyLand {

		public Moosh(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
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
				if(this.State == (byte) CommonState.Wait) {
					this.SetSpriteName("Moosh/White/" + (this.FaceRight ? "Right" : "Left") + "1");
				}
				
				// States: MoveAir, MoveStandard
				else {
					this.SetSpriteName("Moosh/White/" + (this.FaceRight ? "Right" : "Left") + (this.State == (byte) CommonState.Motion ? "3" : "2"));
				}
			}
			
			// Brown & Purple Moosh
			else {

				if(this.State == (byte) CommonState.Move || this.State == (byte) CommonState.MotionEnd || this.State == (byte) CommonState.Wait) {
					if(this.FaceRight) { this.WalkRight(); } else { this.WalkLeft(); }

					// TODO FIX: This apparently broke (this.animate was set to null, and subtype was set to 0 (brown). shouldn't have occurred; can't recreate bug)
					this.animate.SetAnimation("Moosh/" + (this.subType == (byte) MooshSubType.Brown ? "Brown/" : "Purple/") + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 12);
				}

				// States: ReactionCharacter, ReactionStall, RestStunned
				else if(this.State == (byte) CommonState.Special) {
					string frameNum = this.State == (byte) CommonState.Special ? "3" : "1";
					this.SetSpriteName("Moosh/" + (this.subType == (byte) MooshSubType.Brown ? "Brown/" : "Purple/") + (this.FaceRight ? "Right" : "Left") + frameNum);
				}
			}
		}

		private void AssignSubType( byte subType ) {

			if(subType == (byte) MooshSubType.Brown) {
				this.animate = new Animate(this, "Moosh/Brown/");
				this.behavior = new ChargeBehavior(this, 3, 11);
				((ChargeBehavior) this.behavior).SetBehavePassives(120, 35, 30, 9);
				this.SetState((byte) CommonState.Move);

			} else if(subType == (byte) MooshSubType.White) {
				this.speed = FInt.Create(0);
				this.behavior = new HopConstantBehavior(this, 8, 2);
				this.SetState((byte) CommonState.Wait);

			} else if(subType == (byte) MooshSubType.Purple) {
				this.animate = new Animate(this, "Moosh/Purple/");
				this.behavior = new ChargeBehavior(this, 1, 9, 30, 15);
				((ChargeBehavior) this.behavior).SetBehavePassives(120, 35, 30, 11);
				this.SetState((byte) CommonState.Move);
			}
		}

		public override bool GetJumpedOn( Character character, sbyte bounceStrength = 3 ) {
			return base.GetJumpedOn(character, 3);
		}
	}
}
