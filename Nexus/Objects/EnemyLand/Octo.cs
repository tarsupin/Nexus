using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum OctoSubType : byte {
		Octo = 0,
	}

	public class Octo : EnemyLand {

		public Octo(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Octo].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));

			// Sub-Types
			this.AssignSubType(subType);

			// Speed Handling
			this.speed = FInt.Create(0.6);
			this.physics.velocity.X = (FInt)(0 - this.speed);

			// Bounds
			this.AssignBoundsByAtlas(4, 6, -4, -4);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) OctoSubType.Octo) {
				this.animate = new Animate(this, "Octo/");
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.physics.velocity.X = this.speed * (this.FaceRight ? 1 : -1);
			this.animate.SetAnimation("Octo/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 15);
		}
	}
}
