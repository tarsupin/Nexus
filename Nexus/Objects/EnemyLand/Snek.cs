using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum SnekSubType : byte {
		Snek = 0,
		Wurm = 1,
	}

	public class Snek : EnemyLand {

		public Snek(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Snek].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));

			// Sub-Types
			this.AssignSubType(subType);

			// Speed Handling
			this.speed = FInt.Create((this.subType == (byte)SnekSubType.Wurm) ? 0.3 : 0.8);

			this.physics.velocity.X = (FInt)(0 - this.speed);

			// Bounds
			this.AssignBoundsByAtlas(10, 4, -4);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) SnekSubType.Snek) {
				this.animate = new Animate(this, "Snek/");
				this.OnDirectionChange();
			} else if(subType == (byte) SnekSubType.Wurm) {
				this.animate = new Animate(this, "Wurm/");
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.physics.velocity.X = this.speed * (this.FaceRight ? 1 : -1);
			this.animate.SetAnimation((this.subType == (byte) SnekSubType.Snek ? "Snek/" : "Wurm/") + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 12);
		}
	}
}
