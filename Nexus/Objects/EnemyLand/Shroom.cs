using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum ShroomSubType : byte {
		Black,
		Purple,
		Red,
	}

	public class Shroom : EnemyLand {

		public Shroom(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Shroom].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.45));

			// Sub-Types
			this.AssignSubType(subType);

			// Speed Handling
			this.speed = FInt.Create(0);

			if(this.subType == (byte)ShroomSubType.Red) {
				this.speed = FInt.Create(0.4);
			}

			this.physics.velocity.X = (FInt)(0 - this.speed);

			// Bounds
			this.AssignBoundsByAtlas(4, 6, -6);
		}

		private void AssignSubType( byte subType ) {

			if(subType == (byte)ShroomSubType.Black) {
				this.behavior = new HopConstantBehavior(this, 8, 0);
				this.SetState((byte)CommonState.Wait);
			}
			
			else if(subType == (byte) ShroomSubType.Red) {
				this.OnDirectionChange();
			}
			
			else if(subType == (byte) ShroomSubType.Purple) {
				this.behavior = new ChargeBehavior(this, 0, 13, false, 144, 64);
				((ChargeBehavior)this.behavior).SetBehavePassives(45, 25, 30, 7);
				this.SetState((byte)CommonState.Wait);
			}
		}

		public override void OnStateChange() {

			// Shrooms
			if(this.subType == (byte)ShroomSubType.Black) {
				if(this.State == (byte)CommonState.Wait) {
					this.SetSpriteName("Shroom/Black/" + (this.FaceRight ? "Right1" : "Left1"));
				}

				// States: MoveAir, MoveStandard
				else if(this.State == (byte)CommonState.Move) {
					this.SetSpriteName("Shroom/Black/" + (this.FaceRight ? "Right3" : "Left3"));
				}
			}

			else if(this.subType == (byte)ShroomSubType.Purple) {
				this.SetSpriteName("Shroom/Purple/" + (this.FaceRight ? "Right" : "Left") + (this.State == (byte)CommonState.SpecialWait ? "1" : "3"));
			}
		}

		public override void OnDirectionChange() {
			if(this.subType == (byte)ShroomSubType.Red) {
				this.physics.velocity.X = this.speed * (this.FaceRight ? 1 : -1);
				this.animate = new Animate(this, "Shroom/Red/");
				this.animate.SetAnimation("Shroom/Red/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 12);
			}
		}

		public override bool GetJumpedOn(Character character, sbyte bounceStrength = 6) {
			character.BounceUp(this, bounceStrength);
			return this.ReceiveWound(); // TODO: Some Shrooms should be protected above; cannot be damaged by jumping on them.
		}
	}
}
