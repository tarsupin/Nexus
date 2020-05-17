using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum SnekSubType : byte {
		Snek,
		Wurm,
	}

	public class Snek : EnemyLand {

		public Snek(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Snek].meta;

			// Movement
			this.speed = FInt.Create(0.8);

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));
			this.physics.velocity.X = (FInt)(0 - this.speed);

			this.AssignSubType(subType);
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
			this.animate.SetAnimation((this.subType == (byte) SnekSubType.Snek ? "Snek/" : "Wurm/") + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 15);
		}
	}
}
