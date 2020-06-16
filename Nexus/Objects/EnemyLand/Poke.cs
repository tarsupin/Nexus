using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum PokeSubType : byte {
		Poke = 0,
	}

	public class Poke : EnemyLand {

		public Poke(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Poke].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));

			// Sub-Types
			this.AssignSubType(subType);

			// Speed Handling
			this.speed = FInt.Create(0.8);
			this.physics.velocity.X = (FInt)(0 - this.speed);

			// Bounds
			this.AssignBoundsByAtlas(2, 8, -8, -2);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) PokeSubType.Poke) {
				this.animate = new Animate(this, "Poke/");
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.physics.velocity.X = this.speed * (this.FaceRight ? 1 : -1);
			this.animate.SetAnimation("Poke/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 15);
		}
	}
}
