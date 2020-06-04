using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum BugSubType : byte {
		Bug,
	}

	public class Bug : EnemyLand {

		public Bug(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Bug].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));

			// Sub-Types
			this.AssignSubType(subType);

			// Speed Handling
			this.speed = FInt.Create(0.8);
			this.physics.velocity.X = (FInt)(0 - this.speed);

			// Bounds
			this.AssignBoundsByAtlas(4, 2, -2);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) BugSubType.Bug) {
				this.animate = new Animate(this, "Bug/");
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.physics.velocity.X = this.speed * (this.FaceRight ? 1 : -1);
			this.animate.SetAnimation("Bug/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle2, 15);
		}

		public override bool GetJumpedOn(Character character, sbyte bounceStrength = 3) {
			character.BounceUp(this, bounceStrength);
			return false;
		}
	}
}
