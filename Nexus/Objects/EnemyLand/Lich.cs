using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum LichSubType : byte {
		Lich = 0,
	}

	public class Lich : EnemyLand {

		public Lich(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Lich].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));

			// Sub-Types
			this.AssignSubType(subType);

			// Speed Handling
			this.speed = FInt.Create(0.8);
			this.physics.velocity.X = (FInt)(0 - this.speed);

			// Bounds
			this.AssignBoundsByAtlas(4, 6, -6, -2);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) LichSubType.Lich) {
				this.animate = new Animate(this, "Lich/");
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.physics.velocity.X = this.speed * (this.FaceRight ? 1 : -1);
			this.animate.SetAnimation("Lich/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 15);
		}
	}
}
